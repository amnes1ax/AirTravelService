namespace AirTravelService.ReadModel._shared;

public interface IReadModelQueryExecutor
{
    Task<long> CountAsync<T>(IQueryable<T> source, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync<T>(IQueryable<T> source, CancellationToken cancellationToken = default);
    Task<T> FirstAsync<T>(IQueryable<T> source, CancellationToken cancellationToken = default);
    Task<T?> FirstOrDefaultAsync<T>(IQueryable<T?> source, CancellationToken cancellationToken = default);
    Task<T> SingleAsync<T>(IQueryable<T> source, CancellationToken cancellationToken = default);
    Task<T?> SingleOrDefaultAsync<T>(IQueryable<T?> source, CancellationToken cancellationToken = default);
    Task<List<T>> ToListAsync<T>(IQueryable<T> source, CancellationToken cancellationToken = default);
    Task<T[]> ToArrayAsync<T>(IQueryable<T> source, CancellationToken cancellationToken = default);
}