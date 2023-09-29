using AirTravelService.DataAccess.Exceptions;
using AirTravelService.DataAccess.Repositories;
using AirTravelService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirTravelService.DataAccess.PostgresSql._shared;

public abstract class PostgresSqlAggregateRootRepository<TAggregateRoot, TAggregateRootDbContext>
    : IAggregateRootRepository<TAggregateRoot>
    where TAggregateRoot : AggregateRoot
    where TAggregateRootDbContext : PostgresSqlAggregateRootDbContext<TAggregateRoot>
{
    private readonly TAggregateRootDbContext _context;

    protected PostgresSqlAggregateRootRepository(TAggregateRootDbContext context) => _context = context;

    public virtual Task<TAggregateRoot?> FindByIdAsync(Guid aggregateRootId,
        CancellationToken cancellationToken = default) =>
        FindByIdAsyncFunc(_context, aggregateRootId, cancellationToken);

    public Task<bool> ExistsAsync(Guid aggregateRootId, CancellationToken cancellationToken = default) =>
        ExistsAsyncFunc(_context, aggregateRootId, cancellationToken);

    public virtual async Task SaveAsync(TAggregateRoot aggregateRoot)
    {
        try
        {
            if (_context.Entry(aggregateRoot).State == EntityState.Detached)
            {
                await _context.AggregateRoots.AddAsync(aggregateRoot);
            }
        }
        catch (InvalidOperationException ex)
        {
            throw new AggregateAddException(ex.Message, ex);
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {
            _context.ChangeTracker.Clear();
            throw new OptimisticConcurrencyException(e.Message, e);
        }
        catch (DbUpdateException e)
        {
            _context.ChangeTracker.Clear();
            throw new AggregateUpdateException(e.Message, e);
        }
    }

    public async Task DeleteAsync(TAggregateRoot aggregateRoot)
    {
        _context.AggregateRoots.Remove(aggregateRoot);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRangeAsync(IEnumerable<TAggregateRoot> aggregateRoot)
    {
        _context.AggregateRoots.RemoveRange(aggregateRoot);
        await _context.SaveChangesAsync();
    }

    private static readonly Func<TAggregateRootDbContext, Guid, CancellationToken, Task<TAggregateRoot?>>
        FindByIdAsyncFunc = EF.CompileAsyncQuery((TAggregateRootDbContext context, Guid aggregateRootId,
                CancellationToken cancellationToken) =>
            context.AggregateRoots.FirstOrDefault(m => m.AggregateRootId == aggregateRootId));

    private static readonly Func<TAggregateRootDbContext, Guid, CancellationToken, Task<bool>> ExistsAsyncFunc =
        EF.CompileAsyncQuery(
            (TAggregateRootDbContext context, Guid aggregateRootId, CancellationToken cancellationToken) =>
                context.AggregateRoots.Any(m => m.AggregateRootId == aggregateRootId));
}