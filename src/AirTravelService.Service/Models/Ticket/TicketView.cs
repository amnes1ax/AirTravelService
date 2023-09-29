namespace AirTravelService.Service.Models.Ticket;

public class TicketView
{
    public required string OrderNumber { get; init; }
    public string? ServiceProvider { get; init; }
    public required string DeparturePoint { get; init; }
    public required string DestinationPoint { get; init; }
    public required DateTimeOffset DepartureDate { get; init; }
    public required DateTimeOffset ArrivalDate { get; init; }
    public required DateTimeOffset RegistrationDate { get; init; }
    public required PassengerView Passenger { get; init; }
}

public class PassengerView
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? Patronymic { get; init; }
    public required IEnumerable<DocumentView> Documents { get; init; }
}

public class DocumentView
{
    public required string Type { get; init; }
    public required IEnumerable<DocumentFieldView> Fields { get; init; }
}

public class DocumentFieldView
{
    public required string Name { get; init; }
    public required string Value { get; init; }
}