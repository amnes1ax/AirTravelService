using AirTravelService.DataAccess.PostgresSql._shared;
using AirTravelService.DataAccess.PostgresSql.Repositories.Passengers.Context;
using AirTravelService.DataAccess.Repositories;
using AirTravelService.Domain.Entities;

namespace AirTravelService.DataAccess.PostgresSql.Repositories.Passengers;

public class PassengersRepository : PostgresSqlAggregateRootRepository<Passenger, PassengersDbContext>,
    IPassengersRepository
{
    public PassengersRepository(PassengersDbContext context) : base(context)
    {
    }
}