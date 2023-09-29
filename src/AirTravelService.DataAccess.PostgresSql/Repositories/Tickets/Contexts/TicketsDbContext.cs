using AirTravelService.DataAccess.PostgresSql._shared;
using AirTravelService.DataAccess.PostgresSql.Extensions;
using AirTravelService.DataAccess.PostgresSql.Repositories.Tickets.Contexts.Configurations;
using AirTravelService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirTravelService.DataAccess.PostgresSql.Repositories.Tickets.Contexts;

public class TicketsDbContext : PostgresSqlAggregateRootDbContext<Ticket>
{
    public TicketsDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TicketConfiguration());
        modelBuilder.ApplyUtcDateTimeConverter();
        base.OnModelCreating(modelBuilder);
    }
}