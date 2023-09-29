using AirTravelService.ReadModel._shared;
using AirTravelService.ReadModel.PostgresSql._shared;
using Microsoft.EntityFrameworkCore;

namespace AirTravelService.ReadModel.PostgresSql;

internal sealed class ReadModelDbContext : DbContext, IEntityFrameworkReadModelDbContext
{
    public ReadModelDbContext(DbContextOptions<ReadModelDbContext> options) : base(options) =>
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReadModelDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<TReadModelItem> Get<TReadModelItem>() where TReadModelItem : class, IReadModelItem
        => Set<TReadModelItem>();
}