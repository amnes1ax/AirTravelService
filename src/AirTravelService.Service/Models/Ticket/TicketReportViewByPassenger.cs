namespace AirTravelService.Service.Models.Ticket;

public class TicketReportViewByPassenger
{
    public required DateTimeOffset RegistrationDate { get; set; }
    public DateTimeOffset? DepartureDate { get; set; }
    public required string OrderNumber { get; set; }
    public required string DeparturePoint { get; set; }
    public required string DestinationPoint { get; set; }
    public required bool IsFinished { get; set; }
}