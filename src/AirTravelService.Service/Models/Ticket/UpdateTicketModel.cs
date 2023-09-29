namespace AirTravelService.Service.Models.Ticket;

public class UpdateTicketModel
{
    public required Guid TicketId { get; set; }
    public string? ServiceProvider { get; set; }
    public required string DeparturePoint { get; set; }
    public required string DestinationPoint { get; set; }
    public required DateTimeOffset DepartureDate { get; set; }
    public required DateTimeOffset ArrivalDate { get; set; }
}