namespace AirTravelService.Service.Models.Passenger;

public class UpdatePassengerModel
{
    public required Guid PassengerId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Patronymic { get; set; }
}