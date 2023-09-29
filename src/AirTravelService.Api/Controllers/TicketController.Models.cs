using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace AirTravelService.Api.Controllers;

public partial class TicketController
{
    public sealed class CreationTicketModel
    {
        public Guid? TicketId { get; set; }
        public string? OrderNumber { get; set; }
        public string? ServiceProvider { get; set; }
        public string? DeparturePoint { get; set; }
        public string? DestinationPoint { get; set; }
        public DateTimeOffset? DepartureDate { get; set; }
        public DateTimeOffset? ArrivalDate { get; set; }
        public DateTimeOffset? RegistrationDate { get; set; }
        public Guid? PassengerId { get; set; }

        [SuppressMessage("ReSharper", "UnusedType.Global")]
        public sealed class Validator : AbstractValidator<CreationTicketModel>
        {
            public Validator()
            {
                RuleFor(model => model.TicketId)
                    .NotEmpty()
                    .WithMessage("TicketId is required");

                RuleFor(model => model.OrderNumber)
                    .NotEmpty()
                    .WithMessage("OrderNumber is required");

                RuleFor(model => model.DeparturePoint)
                    .NotEmpty()
                    .WithMessage("DeparturePoint is required");
                
                RuleFor(model => model.DestinationPoint)
                    .NotEmpty()
                    .WithMessage("DestinationPoint is required");
                
                RuleFor(model => model.DepartureDate)
                    .NotEmpty()
                    .WithMessage("DepartureDate is required");
                
                RuleFor(model => model.ArrivalDate)
                    .NotEmpty()
                    .WithMessage("ArrivalDate is required");
                
                RuleFor(model => model.RegistrationDate)
                    .NotEmpty()
                    .WithMessage("RegistrationDate is required");
                
                RuleFor(model => model.PassengerId)
                    .NotEmpty()
                    .WithMessage("PassengerId is required");
            }
        }
    }
    
    public sealed class ChangeTicketModel 
    {
        public Guid? TicketId { get; set; }
        public string? ServiceProvider { get; set; }
        public string? DeparturePoint { get; set; }
        public string? DestinationPoint { get; set; }
        public DateTimeOffset? DepartureDate { get; set; }
        public DateTimeOffset? ArrivalDate { get; set; }

        [SuppressMessage("ReSharper", "UnusedType.Global")]
        public sealed class Validator : AbstractValidator<ChangeTicketModel>
        {
            public Validator()
            {
                RuleFor(model => model.TicketId)
                    .NotEmpty()
                    .WithMessage("TicketId is required");

                RuleFor(model => model.DeparturePoint)
                    .NotEmpty()
                    .WithMessage("DeparturePoint is required");
                
                RuleFor(model => model.DestinationPoint)
                    .NotEmpty()
                    .WithMessage("DestinationPoint is required");
                
                RuleFor(model => model.DepartureDate)
                    .NotEmpty()
                    .WithMessage("DepartureDate is required");
                
                RuleFor(model => model.ArrivalDate)
                    .NotEmpty()
                    .WithMessage("ArrivalDate is required");
            }
        }
    }
}