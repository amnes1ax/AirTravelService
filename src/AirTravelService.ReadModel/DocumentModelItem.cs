using AirTravelService.ReadModel._shared;

namespace AirTravelService.ReadModel;

public sealed class DocumentModelItem : IReadModelItem
{
    private DocumentModelItem()
    {
    }

    public Guid DocumentId { get; private set; }
    public string Type { get; private set; }
    public Guid PassengerId { get; private set; }
}