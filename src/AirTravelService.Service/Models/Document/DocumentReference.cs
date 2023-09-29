namespace AirTravelService.Service.Models.Document;

public class DocumentReference
{
    public required Guid DocumentId { get; init; }
    public required string Type { get; init; }
    public required IEnumerable<DocumentFieldReference> Fields { get; set; }
}