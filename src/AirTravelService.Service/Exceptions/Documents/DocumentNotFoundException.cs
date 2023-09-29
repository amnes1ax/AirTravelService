namespace AirTravelService.Service.Exceptions.Documents;

public class DocumentNotFoundException : Exception
{
    private DocumentNotFoundException()
    {
    }

    public DocumentNotFoundException(string? message) : base(message)
    {
    }
}