using AirTravelService.Service.Services;
using AirTravelService.Service.Services.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace AirTravelService.Service;

public static class ServiceCollectionExtensions
{
    public static void AddTravelServices(this IServiceCollection collection)
    {
        collection.AddScoped<IDocumentService, DocumentService>();
        collection.AddScoped<IDocumentBllService, DocumentBllService>();
        
        collection.AddScoped<IPassengerService, PassengerService>();
        collection.AddScoped<IPassengerBllService, PassengerBllService>();
        
        collection.AddScoped<ITicketService, TicketService>();
        collection.AddScoped<ITicketBllService, TicketBllService>();
    }
}