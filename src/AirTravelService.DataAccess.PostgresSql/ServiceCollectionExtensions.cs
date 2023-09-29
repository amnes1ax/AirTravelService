using AirTravelService.DataAccess.PostgresSql._shared;
using AirTravelService.DataAccess.PostgresSql.Repositories.Documents;
using AirTravelService.DataAccess.PostgresSql.Repositories.Documents.Context;
using AirTravelService.DataAccess.PostgresSql.Repositories.Passengers;
using AirTravelService.DataAccess.PostgresSql.Repositories.Passengers.Context;
using AirTravelService.DataAccess.PostgresSql.Repositories.Tickets;
using AirTravelService.DataAccess.PostgresSql.Repositories.Tickets.Contexts;
using AirTravelService.DataAccess.Repositories;
using AirTravelService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AirTravelService.DataAccess.PostgresSql;

public static class ServiceCollectionExtensions
{
    public static void AddPostgresSqlDataAccessSchemaMigrator(this IServiceCollection collection,
        string connectionString)
    {
        collection.AddDbContext<DataAccessSchemaMigratorDbContext>(builder =>
            builder.UseNpgsql(connectionString,
                    x => x.MigrationsAssembly(typeof(ServiceCollectionExtensions).Assembly.FullName))
                .UseSnakeCaseNamingConvention());
        collection.AddScoped<IDataAccessSchemaMigrator, DataAccessSchemaMigrator>();
    }

    public static void AddPostgresSqlRepositories(this IServiceCollection collection, string connectionString)
    {
        collection.AddPostgresSqlAggregateRootRepository<Document, IDocumentsRepository, DocumentsRepository,
            DocumentsDbContext>(connectionString);
        collection.AddPostgresSqlAggregateRootRepository<Passenger, IPassengersRepository, PassengersRepository,
            PassengersDbContext>(connectionString);
        collection.AddPostgresSqlAggregateRootRepository<Ticket, ITicketsRepository, TicketsRepository,
            TicketsDbContext>(connectionString);
    }
}