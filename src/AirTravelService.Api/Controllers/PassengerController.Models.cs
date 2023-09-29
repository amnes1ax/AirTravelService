using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace AirTravelService.Api.Controllers;

public partial class PassengerController
{
    public sealed class CreationPassengerModel
    {
        public Guid? PassengerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Patronymic { get; set; }

        [SuppressMessage("ReSharper", "UnusedType.Global")]
        public sealed class Validator : AbstractValidator<CreationPassengerModel>
        {
            public Validator()
            {
                RuleFor(model => model.PassengerId)
                    .NotEmpty()
                    .WithMessage("PassengerId is required");

                RuleFor(model => model.FirstName)
                    .NotEmpty()
                    .WithMessage("FirstName is required");

                RuleFor(model => model.LastName)
                    .NotEmpty()
                    .WithMessage("LastName is required");

                RuleFor(model => model.Patronymic);
            }
        }
    }
    
    public sealed class ChangePassengerModel 
    {
        public Guid? PassengerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Patronymic { get; set; }

        [SuppressMessage("ReSharper", "UnusedType.Global")]
        public sealed class Validator : AbstractValidator<ChangePassengerModel>
        {
            public Validator()
            {
                RuleFor(model => model.PassengerId)
                    .NotEmpty()
                    .WithMessage("PassengerId is required");

                RuleFor(model => model.FirstName)
                    .NotEmpty()
                    .WithMessage("FirstName is required");

                RuleFor(model => model.LastName)
                    .NotEmpty()
                    .WithMessage("LastName is required");

                RuleFor(model => model.Patronymic);
            }
        }
    }
}