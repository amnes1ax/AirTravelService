using System.Diagnostics.CodeAnalysis;
using AirTravelService.Service.Models;
using FluentValidation;

namespace AirTravelService.Api.Controllers;

public partial class DocumentController
{
    public sealed class CreationDocumentModel
    {
        public Guid? DocumentId { get; init; }
        public string? Type { get; init; }
        public IReadOnlyCollection<DocumentFieldReference>? Fields { get; init; }
        public Guid? PassengerId { get; init; }
        
        [SuppressMessage("ReSharper", "UnusedType.Global")]
        public sealed class Validator : AbstractValidator<CreationDocumentModel>
        {
            public Validator()
            {
                RuleFor(model => model.DocumentId)
                    .NotEmpty()
                    .WithMessage("DocumentId is required");

                RuleFor(model => model.Type)
                    .NotEmpty()
                    .WithMessage("Type is required");

                RuleFor(model => model.Fields)
                    .NotEmpty()
                    .WithMessage("Fields is required");

                RuleFor(model => model.PassengerId)
                    .NotEmpty()
                    .WithMessage("PassengerId is required.");
            }
        }
    }
    
    public sealed class ChangeDocumentModel
    {
        public Guid? DocumentId { get; init; }
        public string? Type { get; init; }
        public IReadOnlyCollection<DocumentFieldReference>? Fields { get; init; }
        
        [SuppressMessage("ReSharper", "UnusedType.Global")]
        public sealed class Validator : AbstractValidator<ChangeDocumentModel>
        {
            public Validator()
            {
                RuleFor(model => model.DocumentId)
                    .NotEmpty()
                    .WithMessage("DocumentId is required");

                RuleFor(model => model.Type)
                    .NotEmpty()
                    .WithMessage("Type is required");

                RuleFor(model => model.Fields)
                    .NotEmpty()
                    .WithMessage("Fields is required");
            }
        }
    }
}