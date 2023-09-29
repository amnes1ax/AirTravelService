using AirTravelService.Domain.Entities;

namespace AirTravelService.DataAccess.Repositories;

public interface IAggregateRootRepository<TAggregateRoot>
    where TAggregateRoot : AggregateRoot
{
    Task<TAggregateRoot?> FindByIdAsync(Guid aggregateRootId, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid aggregateRootId, CancellationToken cancellationToken = default);
    Task SaveAsync(TAggregateRoot aggregateRoot);
    Task DeleteAsync(TAggregateRoot aggregateRoot);
    Task DeleteRangeAsync(IEnumerable<TAggregateRoot> aggregateRoots);
}