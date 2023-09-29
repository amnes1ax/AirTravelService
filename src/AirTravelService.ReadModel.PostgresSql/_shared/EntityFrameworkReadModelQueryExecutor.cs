using AirTravelService.ReadModel._shared;
using Microsoft.EntityFrameworkCore;

namespace AirTravelService.ReadModel.PostgresSql._shared;

public sealed class EntityFrameworkReadModelQueryExecutor : IReadModelQueryExecutor
{
    public Task<bool> AnyAsync<T>(IQueryable<T> source, CancellationToken cancellationToken = default) =>
        source.AnyAsync(cancellationToken);

    public Task<T> FirstAsync<T>(IQueryable<T> source, CancellationToken cancellationToken = default)
        => source.FirstAsync(cancellationToken);

    public Task<long> CountAsync<T>(IQueryable<T> source, CancellationToken cancellationToken = default) =>
        source.LongCountAsync(cancellationToken);

    public Task<T?> FirstOrDefaultAsync<T>(IQueryable<T?> source, CancellationToken cancellationToken = default) =>
        source.FirstOrDefaultAsync(cancellationToken);

    public Task<T> SingleAsync<T>(IQueryable<T> source, CancellationToken cancellationToken = default)
        => source.SingleAsync(cancellationToken);

    public Task<T?> SingleOrDefaultAsync<T>(IQueryable<T?> source, CancellationToken cancellationToken = default) =>
        source.SingleOrDefaultAsync(cancellationToken);

    public Task<T[]> ToArrayAsync<T>(IQueryable<T> source, CancellationToken cancellationToken = default) =>
        source.ToArrayAsync(cancellationToken);

    public Task<List<T>> ToListAsync<T>(IQueryable<T> source, CancellationToken cancellationToken = default) =>
        source.ToListAsync(cancellationToken);
}