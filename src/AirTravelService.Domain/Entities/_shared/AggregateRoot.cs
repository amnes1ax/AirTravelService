namespace AirTravelService.Domain.Entities;

public abstract class AggregateRoot
{
    public Guid AggregateRootId { get; set; }
}