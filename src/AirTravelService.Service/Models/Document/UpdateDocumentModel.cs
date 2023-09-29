namespace AirTravelService.Service.Models.Document;

public class UpdateDocumentModel
{
    public Guid DocumentId { get; init; }
    public required string Type { get; init; }
    public required IReadOnlyCollection<DocumentFieldReference> Fields { get; init; }
}