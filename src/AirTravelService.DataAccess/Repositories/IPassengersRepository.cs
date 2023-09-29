using AirTravelService.Domain.Entities;

namespace AirTravelService.DataAccess.Repositories;

public interface IPassengersRepository : IAggregateRootRepository<Passenger>
{
}