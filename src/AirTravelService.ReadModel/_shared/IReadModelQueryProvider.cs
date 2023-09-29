namespace AirTravelService.ReadModel._shared;

public interface IReadModelQueryProvider<out TReadModelItem>
    where TReadModelItem : class, IReadModelItem
{
    IQueryable<TReadModelItem> Queryable { get; }
}