using AirTravelService.Service.Models.Document;
using AirTravelService.Service.Services.Domain;

namespace AirTravelService.Service.Services;

public class DocumentBllService : IDocumentBllService
{
    private readonly IDocumentService _documentService;

    public DocumentBllService(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    public async Task CreateAsync(CreateDocumentModel documentModel, CancellationToken cancellationToken)
    {
        await _documentService.CreateAsync(documentModel, cancellationToken);
    }

    public async Task<DocumentReference?> GetByIdAsync(Guid documentId, CancellationToken cancellationToken)
    {
        return await _documentService.GetByIdAsync(documentId, cancellationToken);
    }

    public async Task<IEnumerable<DocumentReference>> GetListByPassengerIdAsync(Guid passengerId, CancellationToken cancellationToken)
    {
        return await _documentService.GetListByPassengerIdAsync(passengerId, cancellationToken);
    }

    public async Task UpdateAsync(UpdateDocumentModel documentModel, CancellationToken cancellationToken)
    {
        await _documentService.UpdateAsync(documentModel, cancellationToken);
    }

    public async Task DeleteAsync(Guid documentId, CancellationToken cancellationToken)
    {
        await _documentService.DeleteAsync(documentId, cancellationToken);
    }
}

public interface IDocumentBllService
{
    Task CreateAsync(CreateDocumentModel documentModel, CancellationToken cancellationToken);
    Task<DocumentReference?> GetByIdAsync(Guid documentId, CancellationToken cancellationToken);

    Task<IEnumerable<DocumentReference>> GetListByPassengerIdAsync(Guid passengerId,
        CancellationToken cancellationToken);

    Task UpdateAsync(UpdateDocumentModel documentModel, CancellationToken cancellationToken);
    Task DeleteAsync(Guid documentId, CancellationToken cancellationToken);
}