using AirTravelService.DataAccess.Exceptions;
using AirTravelService.DataAccess.PostgresSql._shared;
using AirTravelService.DataAccess.PostgresSql.Repositories.Documents.Context;
using AirTravelService.DataAccess.Repositories;
using AirTravelService.DataAccess.Repositories.Exceptions;
using AirTravelService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirTravelService.DataAccess.PostgresSql.Repositories.Documents;

public class DocumentsRepository : PostgresSqlAggregateRootRepository<Document, DocumentsDbContext>,
    IDocumentsRepository
{
    private readonly DocumentsDbContext _context;

    public DocumentsRepository(DocumentsDbContext context) : base(context)
    {
        _context = context;
    }

    public override async Task SaveAsync(Document document)
    {
        try
        {
            await base.SaveAsync(document);
        }
        catch (AggregateAddException)
        {
            throw new DocumentFieldWithSameNameAlreadyExistException();
        }
    }

    public override Task<Document?> FindByIdAsync(Guid documentId, CancellationToken cancellationToken = default)
        => FindByIdAsyncFunc(_context, documentId, cancellationToken);

    private static readonly Func<DocumentsDbContext, Guid, CancellationToken, Task<Document?>> FindByIdAsyncFunc =
        EF.CompileAsyncQuery((DocumentsDbContext context, Guid aggregateRootId, CancellationToken cancellationToken) =>
            context.Documents
                .Include(d => d.Fields).AsSplitQuery()
                .FirstOrDefault(m => m.AggregateRootId == aggregateRootId));
}