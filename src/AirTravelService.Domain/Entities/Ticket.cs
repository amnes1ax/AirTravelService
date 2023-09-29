namespace AirTravelService.Domain.Entities;

public sealed class Ticket : AggregateRoot
{
    public required string OrderNumber { get; set; }
    public string? ServiceProvider { get; set; }
    public required string DeparturePoint { get; set; }
    public required string DestinationPoint { get; set; }
    public required DateTimeOffset DepartureDate { get; set; }
    public required DateTimeOffset ArrivalDate { get; set; }
    public required DateTimeOffset RegistrationDate { get; set; }
    public required Guid PassengerId { get; set; }
}