namespace AirTravelService.Service.Exceptions.Documents;

public class DocumentRequiredFieldsNotExistsException : Exception
{
    private DocumentRequiredFieldsNotExistsException()
    {
    }

    public DocumentRequiredFieldsNotExistsException(string? message) : base(message)
    {
    }
}