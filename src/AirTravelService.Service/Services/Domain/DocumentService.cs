using AirTravelService.DataAccess.Repositories;
using AirTravelService.DataAccess.Repositories.Exceptions;
using AirTravelService.Domain.Entities;
using AirTravelService.ReadModel;
using AirTravelService.ReadModel._shared;
using AirTravelService.Service.Exceptions.Documents;
using AirTravelService.Service.Exceptions.Passengers;
using AirTravelService.Service.Models;
using AirTravelService.Service.Models.Document;

namespace AirTravelService.Service.Services.Domain;

public class DocumentService : IDocumentService
{
    private readonly IDocumentsRepository _documentsRepository;
    private readonly IReadModelQueryExecutor _modelQueryExecutor;
    private readonly IReadModelQueryProvider<PassengerModelItem> _passengerModelQueryProvider;
    private readonly IReadModelQueryProvider<DocumentModelItem> _documentModelQueryProvider;
    private readonly IReadModelQueryProvider<DocumentFieldItem> _documentFieldItemModelQueryProvider;

    public DocumentService(IDocumentsRepository documentsRepository,
        IReadModelQueryProvider<DocumentModelItem> documentModelQueryProvider,
        IReadModelQueryProvider<DocumentFieldItem> documentFieldItemModelQueryProvider,
        IReadModelQueryExecutor modelQueryExecutor,
        IReadModelQueryProvider<PassengerModelItem> passengerModelQueryProvider)
    {
        _documentsRepository = documentsRepository;
        _documentModelQueryProvider = documentModelQueryProvider;
        _documentFieldItemModelQueryProvider = documentFieldItemModelQueryProvider;
        _modelQueryExecutor = modelQueryExecutor;
        _passengerModelQueryProvider = passengerModelQueryProvider;
    }

    public async Task CreateAsync(CreateDocumentModel documentModel, CancellationToken cancellationToken)
    {
        var document = await _documentsRepository.FindByIdAsync(documentModel.DocumentId, cancellationToken);
        if (document is not null)
        {
            ThrowIfNotIdempotent(documentModel, document);
            return;
        }

        var passenger = await _modelQueryExecutor.FirstOrDefaultAsync(_passengerModelQueryProvider.Queryable
            .Where(item => item.PassengerId == documentModel.PassengerId), cancellationToken);

        if (passenger is null)
            throw new PassengerNotFoundException($"Passenger with ID '{documentModel.PassengerId}' not found");

        document = new Document
        {
            AggregateRootId = documentModel.DocumentId,
            Type = documentModel.Type,
            PassengerId = documentModel.PassengerId,
            Fields = documentModel.Fields.Select(x => new DocumentField
            {
                Name = x.Name,
                Value = x.Value
            }).ToList(),
        };

        try
        {
            await _documentsRepository.SaveAsync(document);
        }
        catch (DocumentFieldWithSameNameAlreadyExistException)
        {
            throw new DocumentFieldNameNotUniqueException("Document fields names should be unique");
        }
    }

    public async Task<DocumentReference?> GetByIdAsync(Guid documentId, CancellationToken cancellationToken)
    {
        var document = await _modelQueryExecutor.FirstOrDefaultAsync(_documentModelQueryProvider.Queryable
            .Where(item => item.DocumentId == documentId), cancellationToken);

        if (document is null)
            throw new DocumentNotFoundException($"Document with ID '{documentId}' not found");

        var fields = await _modelQueryExecutor.ToListAsync(
            _documentFieldItemModelQueryProvider.Queryable.Where(f => f.DocumentId == documentId),
            cancellationToken);

        return new DocumentReference
        {
            DocumentId = document.DocumentId,
            Type = document.Type,
            Fields = fields.Select(f => new DocumentFieldReference
            {
                Name = f.Name,
                Value = f.Value
            }),
        };
    }

    public async Task<IEnumerable<DocumentReference>> GetListByPassengerIdAsync(Guid passengerId,
        CancellationToken cancellationToken)
    {
        var passenger = await _modelQueryExecutor.FirstOrDefaultAsync(_passengerModelQueryProvider.Queryable
            .Where(item => item.PassengerId == passengerId), cancellationToken);

        if (passenger is null)
            throw new PassengerNotFoundException($"Passenger with ID '{passengerId}' not found");

        var result = await _modelQueryExecutor.ToListAsync(
            from item in _documentModelQueryProvider.Queryable.Where(
                doc => doc.PassengerId == passengerId)
            let fields = _documentFieldItemModelQueryProvider.Queryable.Where(
                d => d.DocumentId == item.DocumentId).ToList()
            select new DocumentReference
            {
                DocumentId = item.DocumentId,
                Type = item.Type,
                Fields = fields.Select(f =>
                    new DocumentFieldReference
                    {
                        Name = f.Name,
                        Value = f.Value
                    }).ToArray(),
            }, cancellationToken);
        return result;
    }

    public async Task UpdateAsync(UpdateDocumentModel documentModel, CancellationToken cancellationToken)
    {
        var document = await _documentsRepository.FindByIdAsync(documentModel.DocumentId, cancellationToken);
        if (document is null)
            throw new DocumentNotFoundException($"Document with ID '{documentModel.DocumentId}' not found");

        if (documentModel.Fields is null || documentModel.Fields.Any() is false)
            throw new DocumentRequiredFieldsNotExistsException("Document must contain at least 1 field");
        document.Fields.Clear();

        document.Type = documentModel.Type;
        foreach (var field in documentModel.Fields)
        {
            document.Fields.Add(new DocumentField
            {
                Name = field.Name,
                Value = field.Value
            });
        }
    }

    public async Task DeleteAsync(Guid documentId, CancellationToken cancellationToken)
    {
        var document = await _documentsRepository.FindByIdAsync(documentId, cancellationToken);
        if (document is null)
            throw new DocumentNotFoundException($"Document with ID '{documentId}' not found");

        await _documentsRepository.DeleteAsync(document);
    }

    public async Task DeleteRangeAsync(List<Guid> documentIds, CancellationToken cancellationToken)
    {
        var documents = new List<Document>();
        foreach (var documentId in documentIds)
        {
            var document = await _documentsRepository.FindByIdAsync(documentId, cancellationToken);
            if (document is not null) documents.Add(document);
        }

        await _documentsRepository.DeleteRangeAsync(documents);
    }

    private static void ThrowIfNotIdempotent(CreateDocumentModel documentModel, Document document)
    {
        if (document.Type != documentModel.Type ||
            document.Fields.OrderBy(d => d.Name).Select(x =>
                new DocumentFieldReference
                {
                    Name = x.Name,
                    Value = x.Value
                }).SequenceEqual(documentModel.Fields.OrderBy(d => d.Name)) is false)

            throw new DocumentAlreadyExistsException(
                $"Document with same ID '{documentModel.DocumentId}' already exists");
    }
}

public interface IDocumentService
{
    Task CreateAsync(CreateDocumentModel documentModel, CancellationToken cancellationToken);
    Task<DocumentReference?> GetByIdAsync(Guid documentId, CancellationToken cancellationToken);

    Task<IEnumerable<DocumentReference>> GetListByPassengerIdAsync(Guid passengerId,
        CancellationToken cancellationToken);

    Task UpdateAsync(UpdateDocumentModel documentModel, CancellationToken cancellationToken);
    Task DeleteAsync(Guid documentId, CancellationToken cancellationToken);
    Task DeleteRangeAsync(List<Guid> documentIds, CancellationToken cancellationToken);
}