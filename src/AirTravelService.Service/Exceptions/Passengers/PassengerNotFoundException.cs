namespace AirTravelService.Service.Exceptions.Passengers;

public class PassengerNotFoundException : Exception
{
    private PassengerNotFoundException()
    {
    }

    public PassengerNotFoundException(string? message) : base(message)
    {
    }
}