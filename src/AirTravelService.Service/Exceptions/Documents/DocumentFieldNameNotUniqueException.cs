namespace AirTravelService.Service.Exceptions.Documents;

public class DocumentFieldNameNotUniqueException: Exception
{
    private DocumentFieldNameNotUniqueException()
    {
    }

    public DocumentFieldNameNotUniqueException(string? message) : base(message)
    {
    }
}