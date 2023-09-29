namespace AirTravelService.Service.Exceptions.Tickets;

public class TicketAlreadyExistsException : Exception
{
    private TicketAlreadyExistsException()
    {
    }

    public TicketAlreadyExistsException(string? message): base(message)
    {
    }
}