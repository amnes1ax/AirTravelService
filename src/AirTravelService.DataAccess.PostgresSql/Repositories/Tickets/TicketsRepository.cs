using AirTravelService.DataAccess.PostgresSql._shared;
using AirTravelService.DataAccess.PostgresSql.Repositories.Tickets.Contexts;
using AirTravelService.DataAccess.Repositories;
using AirTravelService.Domain.Entities;

namespace AirTravelService.DataAccess.PostgresSql.Repositories.Tickets;

public class TicketsRepository : PostgresSqlAggregateRootRepository<Ticket, TicketsDbContext>,
    ITicketsRepository
{
    public TicketsRepository(TicketsDbContext context) : base(context)
    {
    }
}