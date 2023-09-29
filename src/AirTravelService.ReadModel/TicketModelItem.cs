using AirTravelService.ReadModel._shared;

namespace AirTravelService.ReadModel;

public class TicketModelItem : IReadModelItem
{
    private TicketModelItem()
    {
    }
    
    public Guid TicketId { get; private set; }
    public string OrderNumber { get; private set; }
    public string? ServiceProvider { get; private set; }
    public string DeparturePoint { get; private set; }
    public string DestinationPoint { get; private set; }
    public DateTimeOffset DepartureDate { get; private set; }
    public DateTimeOffset ArrivalDate { get; private set; }
    public DateTimeOffset RegistrationDate { get; private set; }
    public Guid PassengerId { get; private set; }
}