namespace AirTravelService.Service.Models.Ticket;

public class TicketReference
{
    public required Guid TicketId { get; set; }
    public required string OrderNumber { get; set; }
    public string? ServiceProvider { get; set; }
    public required string DeparturePoint { get; set; }
    public required string DestinationPoint { get; set; }
    public DateTimeOffset? DepartureDate { get; set; }
    public DateTimeOffset? ArrivalDate { get; set; }
    public required DateTimeOffset RegistrationDate { get; set; }
}