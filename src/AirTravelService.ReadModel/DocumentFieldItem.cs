using AirTravelService.ReadModel._shared;

namespace AirTravelService.ReadModel;

public sealed class DocumentFieldItem : IReadModelItem
{
    private DocumentFieldItem()
    { }

    public Guid DocumentId { get; private set; }
    public string Name { get; private set; }
    public string Value { get; private set; }
}