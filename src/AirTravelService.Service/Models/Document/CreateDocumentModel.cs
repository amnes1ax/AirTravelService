namespace AirTravelService.Service.Models.Document;

public class CreateDocumentModel
{
    public required Guid DocumentId { get; init; }
    public required string Type { get; init; }
    public required IReadOnlyCollection<DocumentFieldReference> Fields { get; init; }
    public required Guid PassengerId { get; init; }
}