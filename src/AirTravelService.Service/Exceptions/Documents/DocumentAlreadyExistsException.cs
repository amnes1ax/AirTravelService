namespace AirTravelService.Service.Exceptions.Documents;

public sealed class DocumentAlreadyExistsException : Exception
{
    private DocumentAlreadyExistsException()
    {
    }

    public DocumentAlreadyExistsException(string? message) : base(message)
    {
    }
}