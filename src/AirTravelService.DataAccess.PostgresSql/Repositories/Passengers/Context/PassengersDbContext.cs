using AirTravelService.DataAccess.PostgresSql._shared;
using AirTravelService.DataAccess.PostgresSql.Extensions;
using AirTravelService.DataAccess.PostgresSql.Repositories.Documents.Context.Configurations;
using AirTravelService.DataAccess.PostgresSql.Repositories.Passengers.Context.Configurations;
using AirTravelService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirTravelService.DataAccess.PostgresSql.Repositories.Passengers.Context;

public class PassengersDbContext : PostgresSqlAggregateRootDbContext<Passenger>
{
    public PassengersDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PassengerConfiguration());
        modelBuilder.ApplyUtcDateTimeConverter();
        base.OnModelCreating(modelBuilder);
    }
}