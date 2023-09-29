using AirTravelService.DataAccess.Repositories;
using AirTravelService.Domain.Entities;
using AirTravelService.ReadModel;
using AirTravelService.ReadModel._shared;
using AirTravelService.Service.Exceptions.Passengers;
using AirTravelService.Service.Exceptions.Tickets;
using AirTravelService.Service.Models.Passenger;

namespace AirTravelService.Service.Services.Domain;

public class PassengerService : IPassengerService
{
    private readonly IPassengersRepository _passengersRepository;
    private readonly IReadModelQueryProvider<TicketModelItem> _ticketModelQueryProvider;
    private readonly IReadModelQueryProvider<PassengerModelItem> _passengerModelQueryProvider;
    private readonly IReadModelQueryExecutor _modelQueryExecutor;

    public PassengerService(IPassengersRepository passengersRepository, IReadModelQueryExecutor modelQueryExecutor,
        IReadModelQueryProvider<PassengerModelItem> passengerModelQueryProvider,
        IReadModelQueryProvider<TicketModelItem> ticketModelQueryProvider)
    {
        _passengersRepository = passengersRepository;
        _modelQueryExecutor = modelQueryExecutor;
        _passengerModelQueryProvider = passengerModelQueryProvider;
        _ticketModelQueryProvider = ticketModelQueryProvider;
    }

    public async Task CreateAsync(CreatePassengerModel passengerModel, CancellationToken cancellationToken)
    {
        var passenger = await _passengersRepository.FindByIdAsync(passengerModel.PassengerId, cancellationToken);
        if (passenger is not null)
        {
            ThrowIfNotIdempotent(passengerModel, passenger);
            return;
        }

        passenger = new Passenger
        {
            AggregateRootId = passengerModel.PassengerId,
            FirstName = passengerModel.FirstName,
            LastName = passengerModel.LastName,
            Patronymic = passengerModel.Patronymic
        };

        await _passengersRepository.SaveAsync(passenger);
    }

    public async Task<PassengerReference?> GetByIdAsync(Guid passengerId, CancellationToken cancellationToken)
    {
        var passenger = await _modelQueryExecutor.FirstOrDefaultAsync(_passengerModelQueryProvider.Queryable
            .Where(item => item.PassengerId == passengerId), cancellationToken);

        if (passenger is null)
            throw new PassengerNotFoundException($"Passenger with ID '{passengerId}' not found");

        return new PassengerReference
        {
            PassengerId = passenger.PassengerId,
            FirstName = passenger.FirstName,
            LastName = passenger.LastName,
            Patronymic = passenger.Patronymic
        };
    }

    public async Task<PassengerReference?> GetByTicketIdAsync(Guid ticketId, CancellationToken cancellationToken)
    {
        var ticket = await _modelQueryExecutor.FirstOrDefaultAsync(_ticketModelQueryProvider.Queryable
            .Where(item => item.TicketId == ticketId), cancellationToken);
        
        if (ticket is null)
            throw new TicketNotFoundException($"Ticket with ID '{ticketId}' not found");

        var passenger = await _modelQueryExecutor.FirstOrDefaultAsync(
            _passengerModelQueryProvider.Queryable
                .Where(item => item.PassengerId == ticket.PassengerId)
                .Select(item => new PassengerReference
                {
                    PassengerId = item.PassengerId,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Patronymic = item.Patronymic
                }), cancellationToken);

        if (passenger is null)
            throw new PassengerNotFoundException($"Passenger with ticket ID '{ticketId}' not found");

        return passenger;
    }

    public async Task<IEnumerable<PassengerReference>> GetListAsync(CancellationToken cancellationToken)
    {
        var result = await _modelQueryExecutor.ToListAsync(
            _passengerModelQueryProvider.Queryable, cancellationToken);

        return result.Select(x => new PassengerReference
        {
            PassengerId = x.PassengerId,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Patronymic = x.Patronymic
        });
    }

    public async Task UpdateAsync(UpdatePassengerModel passengerModel, CancellationToken cancellationToken)
    {
        var passenger = await _passengersRepository.FindByIdAsync(passengerModel.PassengerId, cancellationToken);
        if (passenger is null)
            throw new PassengerNotFoundException($"Passenger with ID '{passengerModel.PassengerId}' not found");

        passenger.FirstName = passengerModel.FirstName;
        passenger.LastName = passengerModel.LastName;
        passenger.Patronymic = passengerModel.Patronymic;

        await _passengersRepository.SaveAsync(passenger);
    }

    public async Task DeleteAsync(Guid passengerId, CancellationToken cancellationToken)
    {
        var passenger = await _passengersRepository.FindByIdAsync(passengerId, cancellationToken);
        if (passenger is null)
            throw new PassengerNotFoundException($"Passenger with ID '{passengerId}' not found");

        await _passengersRepository.DeleteAsync(passenger);
    }
    
    public async Task DeleteRangeAsync(List<Guid> passengerIds, CancellationToken cancellationToken)
    {
        var passengers = new List<Passenger>();
        foreach (var passengerId in passengerIds)
        {
            var passenger = await _passengersRepository.FindByIdAsync(passengerId, cancellationToken);
            if (passenger is not null) passengers.Add(passenger);
        }
        await _passengersRepository.DeleteRangeAsync(passengers);
    }

    private static void ThrowIfNotIdempotent(CreatePassengerModel passengerModel, Passenger passenger)
    {
        if (passenger.FirstName != passengerModel.FirstName ||
            passenger.LastName != passengerModel.LastName ||
            passenger.Patronymic != passengerModel.Patronymic)
        {
            throw new PassengerAlreadyExistsException(
                $"Passenger with same ID '{passengerModel.PassengerId}' already exists");
        }
    }
}

public interface IPassengerService
{
    Task CreateAsync(CreatePassengerModel passengerModel, CancellationToken cancellationToken = default);
    Task<PassengerReference?> GetByIdAsync(Guid passengerId, CancellationToken cancellationToken = default);
    Task<PassengerReference?> GetByTicketIdAsync(Guid ticketId, CancellationToken cancellationToken = default);
    Task<IEnumerable<PassengerReference>> GetListAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(UpdatePassengerModel passengerModel, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid passengerId, CancellationToken cancellationToken = default);
    Task DeleteRangeAsync(List<Guid> passengerIds, CancellationToken cancellationToken = default);
}