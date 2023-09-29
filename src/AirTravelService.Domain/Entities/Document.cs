namespace AirTravelService.Domain.Entities;

public sealed class Document : AggregateRoot
{
    public required string Type { get; set; }
    public required ICollection<DocumentField> Fields { get; set; }
    public required Guid PassengerId { get; set; }
}