using Microsoft.EntityFrameworkCore;

namespace AirTravelService.DataAccess.PostgresSql;

internal sealed class DataAccessSchemaMigrator : IDataAccessSchemaMigrator
{
    private readonly DataAccessSchemaMigratorDbContext _context;

    public DataAccessSchemaMigrator(DataAccessSchemaMigratorDbContext context) => _context = context;

    public async ValueTask MigrateAsync() => await _context.Database.MigrateAsync();
}

public interface IDataAccessSchemaMigrator
{
    ValueTask MigrateAsync();
}