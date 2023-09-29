using AirTravelService.ReadModel._shared;

namespace AirTravelService.ReadModel;

public class PassengerModelItem : IReadModelItem
{
    private PassengerModelItem()
    {
    }

    public Guid PassengerId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string? Patronymic { get; private set; }
}