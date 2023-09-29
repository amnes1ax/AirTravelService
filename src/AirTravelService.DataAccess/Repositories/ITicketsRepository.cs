using AirTravelService.Domain.Entities;

namespace AirTravelService.DataAccess.Repositories;

public interface ITicketsRepository : IAggregateRootRepository<Ticket>
{
}