using AirTravelService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirTravelService.DataAccess.PostgresSql._shared;

public abstract class PostgresSqlAggregateRootDbContext<TAggregateRoot> : DbContext
    where TAggregateRoot : AggregateRoot
{
    protected PostgresSqlAggregateRootDbContext(DbContextOptions options) : base(options)
    {
    }

    protected internal DbSet<TAggregateRoot> AggregateRoots { get; set; } = null!;
}