namespace AirTravelService.Service.Exceptions.Tickets;

public class TicketNotFoundException : Exception
{
    private TicketNotFoundException()
    {
    }

    public TicketNotFoundException(string? message) : base(message)
    {
    }
}