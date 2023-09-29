using AirTravelService.ReadModel;
using AirTravelService.ReadModel._shared;
using AirTravelService.Service.Models.Passenger;
using AirTravelService.Service.Services.Domain;

namespace AirTravelService.Service.Services;

public class PassengerBllService : IPassengerBllService
{
    private readonly IReadModelQueryProvider<TicketModelItem> _ticketModelQueryProvider;
    private readonly IReadModelQueryProvider<DocumentModelItem> _documentModelQueryProvider;
    private readonly IReadModelQueryExecutor _modelQueryExecutor;

    private readonly IPassengerService _passengerService;
    private readonly ITicketService _ticketService;
    private readonly IDocumentService _documentService;

    public PassengerBllService(IReadModelQueryProvider<TicketModelItem> ticketModelQueryProvider,
        IReadModelQueryProvider<DocumentModelItem> documentModelQueryProvider,
        IReadModelQueryExecutor modelQueryExecutor,
        IPassengerService passengerService, ITicketService ticketService, IDocumentService documentService)
    {
        _ticketModelQueryProvider = ticketModelQueryProvider;
        _documentModelQueryProvider = documentModelQueryProvider;
        _modelQueryExecutor = modelQueryExecutor;
        _passengerService = passengerService;
        _ticketService = ticketService;
        _documentService = documentService;
    }

    public async Task CreateAsync(CreatePassengerModel passengerModel, CancellationToken cancellationToken)
    {
        await _passengerService.CreateAsync(passengerModel, cancellationToken);
    }

    public async Task<PassengerReference?> GetByIdAsync(Guid passengerId, CancellationToken cancellationToken)
    {
        return await _passengerService.GetByIdAsync(passengerId, cancellationToken);
    }

    public async Task<PassengerReference?> GetByTicketIdAsync(Guid ticketId, CancellationToken cancellationToken)
    {
        return await _passengerService.GetByTicketIdAsync(ticketId, cancellationToken);
    }

    public async Task<IEnumerable<PassengerReference>> GetListAsync(CancellationToken cancellationToken)
    {
        return await _passengerService.GetListAsync(cancellationToken);
    }

    public async Task UpdateAsync(UpdatePassengerModel passengerModel, CancellationToken cancellationToken)
    {
        await _passengerService.UpdateAsync(passengerModel, cancellationToken);
    }

    public async Task DeleteAsync(Guid passengerId, CancellationToken cancellationToken)
    {
        var ticketIds = _modelQueryExecutor.ToListAsync(
            _ticketModelQueryProvider.Queryable.Where(x => x.PassengerId == passengerId)
                .Select(x => x.TicketId),
            cancellationToken);

        var documentIds = _modelQueryExecutor.ToListAsync(
            _documentModelQueryProvider.Queryable
                .Where(doc => doc.PassengerId == passengerId)
                .Select(x => x.DocumentId), cancellationToken);

        await Task.WhenAll(ticketIds, documentIds);

        await _ticketService.DeleteRangeAsync(ticketIds.Result, cancellationToken);
        await _documentService.DeleteRangeAsync(documentIds.Result, cancellationToken);
        await _passengerService.DeleteAsync(passengerId, cancellationToken);
    }
}

public interface IPassengerBllService
{
    Task CreateAsync(CreatePassengerModel passengerModel, CancellationToken cancellationToken);
    Task<PassengerReference?> GetByIdAsync(Guid passengerId, CancellationToken cancellationToken);
    Task<PassengerReference?> GetByTicketIdAsync(Guid ticketId, CancellationToken cancellationToken);
    Task<IEnumerable<PassengerReference>> GetListAsync(CancellationToken cancellationToken);
    Task UpdateAsync(UpdatePassengerModel passengerModel, CancellationToken cancellationToken);
    Task DeleteAsync(Guid passengerId, CancellationToken cancellationToken);
}