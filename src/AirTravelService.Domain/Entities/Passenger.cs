namespace AirTravelService.Domain.Entities;

public sealed class Passenger : AggregateRoot
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Patronymic { get; set; }
}