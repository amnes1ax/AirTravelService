namespace AirTravelService.Service.Exceptions.Passengers;

public class PassengerAlreadyExistsException : Exception
{
    private PassengerAlreadyExistsException()
    {
    }

    public PassengerAlreadyExistsException(string? message) : base(message)
    {
    }
}