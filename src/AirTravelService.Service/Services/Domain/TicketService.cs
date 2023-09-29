using AirTravelService.DataAccess.Repositories;
using AirTravelService.Domain.Entities;
using AirTravelService.ReadModel;
using AirTravelService.ReadModel._shared;
using AirTravelService.Service.Exceptions.Passengers;
using AirTravelService.Service.Exceptions.Tickets;
using AirTravelService.Service.Models.Ticket;

namespace AirTravelService.Service.Services.Domain;

public class TicketService : ITicketService
{
    private readonly ITicketsRepository _ticketsRepository;
    private readonly IReadModelQueryProvider<TicketModelItem> _ticketModelQueryProvider;
    private readonly IReadModelQueryProvider<PassengerModelItem> _passengerModelQueryProvider;
    private readonly IReadModelQueryProvider<DocumentModelItem> _documentModelQueryProvider;
    private readonly IReadModelQueryProvider<DocumentFieldItem> _documentFieldItemModelQueryProvider;
    private readonly IReadModelQueryExecutor _modelQueryExecutor;

    public TicketService(ITicketsRepository ticketsRepository,
        IReadModelQueryExecutor modelQueryExecutor,
        IReadModelQueryProvider<TicketModelItem> ticketModelQueryProvider,
        IReadModelQueryProvider<PassengerModelItem> passengerModelQueryProvider,
        IReadModelQueryProvider<DocumentModelItem> documentModelQueryProvider,
        IReadModelQueryProvider<DocumentFieldItem> documentFieldItemModelQueryProvider)
    {
        _ticketsRepository = ticketsRepository;
        _ticketModelQueryProvider = ticketModelQueryProvider;
        _modelQueryExecutor = modelQueryExecutor;
        _passengerModelQueryProvider = passengerModelQueryProvider;
        _documentModelQueryProvider = documentModelQueryProvider;
        _documentFieldItemModelQueryProvider = documentFieldItemModelQueryProvider;
    }

    public async Task CreateAsync(CreateTicketModel ticketModel, CancellationToken cancellationToken)
    {
        var ticket = await _ticketsRepository.FindByIdAsync(ticketModel.TicketId, cancellationToken);
        if (ticket is not null)
        {
            ThrowIfNotIdempotent(ticketModel, ticket);
            return;
        }

        var passenger = await _modelQueryExecutor.FirstOrDefaultAsync(
            _passengerModelQueryProvider.Queryable.Where(item => item.PassengerId == ticketModel.PassengerId),
            cancellationToken);
        if (passenger is null)
            throw new PassengerNotFoundException($"Passenger with ID '{ticketModel.PassengerId}' not found");

        ticket = new Ticket
        {
            AggregateRootId = ticketModel.TicketId,
            OrderNumber = ticketModel.OrderNumber,
            ServiceProvider = ticketModel.ServiceProvider,
            DeparturePoint = ticketModel.DeparturePoint,
            DestinationPoint = ticketModel.DestinationPoint,
            DepartureDate = ticketModel.DepartureDate,
            ArrivalDate = ticketModel.ArrivalDate,
            RegistrationDate = ticketModel.RegistrationDate,
            PassengerId = ticketModel.PassengerId
        };

        await _ticketsRepository.SaveAsync(ticket);
    }

    public async Task<TicketReference?> GetByIdAsync(Guid ticketId, CancellationToken cancellationToken)
    {
        var ticket = await _modelQueryExecutor.FirstOrDefaultAsync(_ticketModelQueryProvider.Queryable
            .Where(item => item.TicketId == ticketId), cancellationToken);
        if (ticket is null)
        {
            throw new TicketNotFoundException($"Ticket with ID '{ticketId}' not found");
        }

        return new TicketReference
        {
            TicketId = ticket.TicketId,
            OrderNumber = ticket.OrderNumber,
            ServiceProvider = ticket.ServiceProvider,
            DeparturePoint = ticket.DeparturePoint,
            DestinationPoint = ticket.DestinationPoint,
            DepartureDate = ticket.DepartureDate,
            ArrivalDate = ticket.ArrivalDate,
            RegistrationDate = ticket.RegistrationDate
        };
    }

    public async Task<TicketView?> GetInfoViewByIdAsync(Guid ticketId, CancellationToken cancellationToken)
    {
        var ticket = await _modelQueryExecutor.FirstOrDefaultAsync(_ticketModelQueryProvider.Queryable
            .Where(item => item.TicketId == ticketId), cancellationToken);
        if (ticket is null)
            throw new TicketNotFoundException($"Ticket with ID '{ticketId}' not found");

        var passenger = await _modelQueryExecutor.FirstOrDefaultAsync(
            _passengerModelQueryProvider.Queryable.Where(item => item.PassengerId == ticket.PassengerId),
            cancellationToken);
        if (passenger is null)
            throw new PassengerNotFoundException($"Passenger with ID '{ticket.PassengerId}' not found");

        var documents = await _modelQueryExecutor.ToListAsync(
            from item in _documentModelQueryProvider.Queryable.Where(
                doc => doc.PassengerId == passenger.PassengerId)
            let fields = _documentFieldItemModelQueryProvider.Queryable.Where(
                d => d.DocumentId == item.DocumentId).ToList()
            select new DocumentView
            {
                Type = item.Type,
                Fields = fields.Select(f =>
                    new DocumentFieldView
                    {
                        Name = f.Name,
                        Value = f.Value
                    }).ToArray(),
            }, cancellationToken);

        return new TicketView
        {
            OrderNumber = ticket.OrderNumber,
            ServiceProvider = ticket.ServiceProvider,
            DeparturePoint = ticket.DeparturePoint,
            DestinationPoint = ticket.DestinationPoint,
            DepartureDate = ticket.DepartureDate,
            ArrivalDate = ticket.ArrivalDate,
            RegistrationDate = ticket.RegistrationDate,
            Passenger = new PassengerView
            {
                FirstName = passenger.FirstName,
                LastName = passenger.LastName,
                Patronymic = passenger.Patronymic,
                Documents = documents
            }
        };
    }

    public async Task<IEnumerable<TicketReportViewByPassenger?>> GetReportByPassengerAsync(Guid passengerId,
        DateTimeOffset startDateRange, DateTimeOffset endDateRange, CancellationToken cancellationToken)
    {
        var passenger = await _modelQueryExecutor.FirstOrDefaultAsync(
            _passengerModelQueryProvider.Queryable.Where(item => item.PassengerId == passengerId),
            cancellationToken);
        if (passenger is null)
            throw new PassengerNotFoundException($"Passenger with ID '{passengerId}' not found");

        var tickets = await _modelQueryExecutor.ToListAsync(
            _ticketModelQueryProvider.Queryable.Where(item => item.PassengerId == passengerId)
                .Where(item => (item.ArrivalDate >= startDateRange && item.ArrivalDate <= endDateRange) ||
                               (item.RegistrationDate >= startDateRange && item.RegistrationDate <= endDateRange)),
            cancellationToken);

        return tickets.Select(x => new TicketReportViewByPassenger
        {
            RegistrationDate = x.RegistrationDate,
            OrderNumber = x.OrderNumber,
            DeparturePoint = x.DeparturePoint,
            DestinationPoint = x.DestinationPoint,
            IsFinished = x.ArrivalDate >= startDateRange &&
                         x.ArrivalDate <= endDateRange &&
                         x.ArrivalDate <= DateTimeOffset.UtcNow
        });
    }

    public async Task<IEnumerable<TicketReference>> GetListAsync(CancellationToken cancellationToken)
    {
        var tickets = await _modelQueryExecutor.ToListAsync(
            _ticketModelQueryProvider.Queryable
                .Select(item => new TicketReference
                {
                    TicketId = item.TicketId,
                    OrderNumber = item.OrderNumber,
                    ServiceProvider = item.ServiceProvider,
                    DeparturePoint = item.DeparturePoint,
                    DestinationPoint = item.DestinationPoint,
                    DepartureDate = item.DepartureDate,
                    ArrivalDate = item.ArrivalDate,
                    RegistrationDate = item.RegistrationDate
                }), cancellationToken);

        return tickets;
    }

    public async Task UpdateAsync(UpdateTicketModel ticketModel, CancellationToken cancellationToken)
    {
        var ticket = await _ticketsRepository.FindByIdAsync(ticketModel.TicketId, cancellationToken);
        if (ticket is null)
            throw new TicketNotFoundException($"Ticket with ID '{ticketModel.TicketId}' not found");

        ticket.ServiceProvider = ticketModel.ServiceProvider;
        ticket.DeparturePoint = ticketModel.DeparturePoint;
        ticket.DestinationPoint = ticketModel.DestinationPoint;
        ticket.DepartureDate = ticketModel.DepartureDate;
        ticket.ArrivalDate = ticketModel.ArrivalDate;

        await _ticketsRepository.SaveAsync(ticket);
    }

    public async Task DeleteAsync(Guid ticketId, CancellationToken cancellationToken)
    {
        var ticket = await _ticketsRepository.FindByIdAsync(ticketId, cancellationToken);
        if (ticket is null)
            throw new TicketNotFoundException($"Ticket with ID '{ticketId}' not found");

        await _ticketsRepository.DeleteAsync(ticket);
    }

    public async Task DeleteRangeAsync(List<Guid> ticketIds, CancellationToken cancellationToken)
    {
        var tickets = new List<Ticket>();
        foreach (var ticketId in ticketIds)
        {
            var ticket = await _ticketsRepository.FindByIdAsync(ticketId, cancellationToken);
            if (ticket is not null) tickets.Add(ticket);
        }

        await _ticketsRepository.DeleteRangeAsync(tickets);
    }

    private static void ThrowIfNotIdempotent(CreateTicketModel ticketModel, Ticket ticket)
    {
        if (ticket.RegistrationDate != ticketModel.RegistrationDate ||
            ticket.PassengerId != ticketModel.PassengerId ||
            ticket.ArrivalDate != ticketModel.ArrivalDate ||
            ticket.DestinationPoint != ticketModel.DestinationPoint ||
            ticket.DepartureDate != ticketModel.DepartureDate ||
            ticket.DeparturePoint != ticketModel.DeparturePoint ||
            ticket.ServiceProvider != ticketModel.ServiceProvider ||
            ticket.OrderNumber != ticketModel.OrderNumber)
        {
            throw new TicketAlreadyExistsException(
                $"Passenger with same ID '{ticketModel.TicketId}' already exists");
        }
    }
}

public interface ITicketService
{
    Task CreateAsync(CreateTicketModel ticketModel, CancellationToken cancellationToken);
    Task<TicketReference?> GetByIdAsync(Guid ticketId, CancellationToken cancellationToken);
    Task<TicketView?> GetInfoViewByIdAsync(Guid ticketId, CancellationToken cancellationToken);

    Task<IEnumerable<TicketReportViewByPassenger?>> GetReportByPassengerAsync(Guid passengerId,
        DateTimeOffset startDateRange, DateTimeOffset endDateRange, CancellationToken cancellationToken);

    Task<IEnumerable<TicketReference>> GetListAsync(CancellationToken cancellationToken);
    Task UpdateAsync(UpdateTicketModel ticketModel, CancellationToken cancellationToken);
    Task DeleteAsync(Guid ticketId, CancellationToken cancellationToken);
    Task DeleteRangeAsync(List<Guid> ticketIds, CancellationToken cancellationToken);
}