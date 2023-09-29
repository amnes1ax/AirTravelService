using AirTravelService.DataAccess.Repositories;
using AirTravelService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AirTravelService.DataAccess.PostgresSql._shared;

public static class ServiceCollectionExtensions
{
    public static void AddPostgresSqlAggregateRootRepository<TAggregateRoot, TAggregateRootRepository,
        TAggregateRootRepositoryImplementation, TAggregateRootDbContext>(
        this IServiceCollection collection, string connectionString)
        where TAggregateRoot : AggregateRoot
        where TAggregateRootRepository : class, IAggregateRootRepository<TAggregateRoot>
        where TAggregateRootRepositoryImplementation :
        PostgresSqlAggregateRootRepository<TAggregateRoot, TAggregateRootDbContext>,
        TAggregateRootRepository
        where TAggregateRootDbContext : PostgresSqlAggregateRootDbContext<TAggregateRoot>
    {
        collection.AddDbContextPool<TAggregateRootDbContext>(builder => builder.UseNpgsql(connectionString)
            .UseSnakeCaseNamingConvention()
        );
        
        collection.AddScoped<TAggregateRootRepository, TAggregateRootRepositoryImplementation>();
    }
}