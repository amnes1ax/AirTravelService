using AirTravelService.ReadModel._shared;

namespace AirTravelService.ReadModel.PostgresSql._shared;

public sealed class EntityFrameworkReadModelQueryProvider<TReadModelItem> : IReadModelQueryProvider<TReadModelItem>
    where TReadModelItem : class, IReadModelItem
{
    private readonly IEntityFrameworkReadModelDbContext _context;

    public EntityFrameworkReadModelQueryProvider(IEntityFrameworkReadModelDbContext context) => _context = context;

    public IQueryable<TReadModelItem> Queryable => _context.Get<TReadModelItem>();
}