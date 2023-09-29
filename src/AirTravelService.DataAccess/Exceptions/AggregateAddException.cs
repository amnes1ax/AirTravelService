namespace AirTravelService.DataAccess.Exceptions;

public class AggregateAddException: Exception
{
    public AggregateAddException(string message) : base(message)
    {
    }

    public AggregateAddException(string message, Exception innerException) : base(message, innerException)
    {
    }
}