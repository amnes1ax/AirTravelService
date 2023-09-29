namespace AirTravelService.Domain.Entities;

public sealed class DocumentField
{
    public required string Name { get; set; }
    public required string Value { get; set; }
}