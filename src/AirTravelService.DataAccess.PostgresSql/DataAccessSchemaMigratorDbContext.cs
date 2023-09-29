using Microsoft.EntityFrameworkCore;

namespace AirTravelService.DataAccess.PostgresSql;

internal sealed class DataAccessSchemaMigratorDbContext : DbContext
{
    public DataAccessSchemaMigratorDbContext(
        DbContextOptions<DataAccessSchemaMigratorDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataAccessSchemaMigratorDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}