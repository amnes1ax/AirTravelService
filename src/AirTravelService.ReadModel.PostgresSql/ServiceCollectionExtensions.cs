using AirTravelService.ReadModel._shared;
using AirTravelService.ReadModel.PostgresSql._shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AirTravelService.ReadModel.PostgresSql;

public static class ServiceCollectionExtensions
{
    public static void AddPostgresSqlReadModel(this IServiceCollection collection, string connectionString)
    {
        collection.AddDbContextPool<IEntityFrameworkReadModelDbContext, ReadModelDbContext>(builder =>
            builder.UseNpgsql(connectionString).UseSnakeCaseNamingConvention());
        collection.AddScoped<IReadModelQueryExecutor, EntityFrameworkReadModelQueryExecutor>();
        collection.AddScoped(typeof(IReadModelQueryProvider<>), typeof(EntityFrameworkReadModelQueryProvider<>));
    }
}