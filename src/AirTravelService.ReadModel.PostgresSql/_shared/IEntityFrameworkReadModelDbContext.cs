using AirTravelService.ReadModel._shared;
using Microsoft.EntityFrameworkCore;

namespace AirTravelService.ReadModel.PostgresSql._shared;

public interface IEntityFrameworkReadModelDbContext
{
    DbSet<TReadModelItem> Get<TReadModelItem>()
        where TReadModelItem : class, IReadModelItem;
}