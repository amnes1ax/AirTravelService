using AirTravelService.Service.Models.Ticket;
using AirTravelService.Service.Services.Domain;

namespace AirTravelService.Service.Services;

public class TicketBllService : ITicketBllService
{
    private readonly ITicketService _ticketService;

    public TicketBllService(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    public async Task CreateAsync(CreateTicketModel ticketModel, CancellationToken cancellationToken)
    {
        await _ticketService.CreateAsync(ticketModel, cancellationToken);
    }

    public async Task<TicketReference?> GetByIdAsync(Guid ticketId, CancellationToken cancellationToken)
    {
        return await _ticketService.GetByIdAsync(ticketId, cancellationToken);
    }

    public async Task<TicketView?> GetInfoViewByIdAsync(Guid ticketId, CancellationToken cancellationToken)
    {
        return await _ticketService.GetInfoViewByIdAsync(ticketId, cancellationToken);
    }

    public async Task<IEnumerable<TicketReportViewByPassenger?>> GetReportByPassengerAsync(Guid passengerId,
        DateTimeOffset startDateRange, DateTimeOffset endDateRange,
        CancellationToken cancellationToken)
    {
        return await _ticketService.GetReportByPassengerAsync(
            passengerId, startDateRange, endDateRange, cancellationToken);
    }

    public async Task<IEnumerable<TicketReference>> GetListAsync(CancellationToken cancellationToken)
    {
        return await _ticketService.GetListAsync(cancellationToken);
    }

    public async Task UpdateAsync(UpdateTicketModel ticketModel, CancellationToken cancellationToken)
    {
        await _ticketService.UpdateAsync(ticketModel, cancellationToken);
    }

    public async Task DeleteAsync(Guid ticketId, CancellationToken cancellationToken)
    {
        await _ticketService.DeleteAsync(ticketId, cancellationToken);
    }
}

public interface ITicketBllService
{
    Task CreateAsync(CreateTicketModel ticketModel, CancellationToken cancellationToken);
    Task<TicketReference?> GetByIdAsync(Guid ticketId, CancellationToken cancellationToken);
    Task<TicketView?> GetInfoViewByIdAsync(Guid ticketId, CancellationToken cancellationToken);

    Task<IEnumerable<TicketReportViewByPassenger?>> GetReportByPassengerAsync(Guid passengerId,
        DateTimeOffset startDateRange, DateTimeOffset endDateRange, CancellationToken cancellationToken);

    Task<IEnumerable<TicketReference>> GetListAsync(CancellationToken cancellationToken);
    Task UpdateAsync(UpdateTicketModel ticketModel, CancellationToken cancellationToken);
    Task DeleteAsync(Guid ticketId, CancellationToken cancellationToken);
}