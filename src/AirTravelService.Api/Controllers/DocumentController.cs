using System.ComponentModel.DataAnnotations;
using AirTravelService.Service.Exceptions.Documents;
using AirTravelService.Service.Exceptions.Passengers;
using AirTravelService.Service.Models.Document;
using AirTravelService.Service.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AirTravelService.Api.Controllers;

[ApiController]
[Route("api/document")]
public partial class DocumentController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> CreateDocumentAsync(
        [FromServices] IDocumentBllService documentService,
        [FromBody] [Required] CreationDocumentModel model,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await documentService.CreateAsync(new CreateDocumentModel
            {
                DocumentId = model.DocumentId!.Value,
                Type = model.Type!,
                Fields = model.Fields!,
                PassengerId = model.PassengerId!.Value
            }, cancellationToken);

            return Ok();
        }
        catch (PassengerNotFoundException ex)
        {
            return UnprocessableEntity(ex.Message);
        }
        catch (DocumentAlreadyExistsException ex)
        {
            return Conflict(ex.Message);
        }
        catch (DocumentFieldNameNotUniqueException ex)
        {
            return UnprocessableEntity(ex.Message);
        }
    }
    
    [HttpGet("{documentId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync(
        [FromServices] IDocumentBllService documentService,
        [FromRoute] [Required] Guid documentId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await documentService.GetByIdAsync(documentId, cancellationToken);
            return Ok(response);
        }
        catch (DocumentNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpGet("by-passenger")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetListByPassengerIdAsync(
        [FromServices] IDocumentBllService documentService,
        [FromQuery] [Required] Guid passengerId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await documentService.GetListByPassengerIdAsync(passengerId, cancellationToken);
            return Ok(response);
        }
        catch (DocumentNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> UpdateAsync(
        [FromServices] IDocumentBllService documentService,
        [FromBody] [Required] ChangeDocumentModel model,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var documentModel = new UpdateDocumentModel
            {
                DocumentId = model.DocumentId!.Value,
                Type = model.Type!,
                Fields = model.Fields!
            };
            await documentService.UpdateAsync(documentModel, cancellationToken);

            return Ok();
        }
        catch (PassengerNotFoundException ex)
        {
            return UnprocessableEntity(ex.Message);
        }
        catch (DocumentRequiredFieldsNotExistsException ex)
        {
            return UnprocessableEntity(ex.Message);
        }
    }
    
    [HttpDelete("{documentId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync(
        [FromServices] IDocumentBllService documentService,
        [FromRoute] [Required] Guid documentId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await documentService.DeleteAsync(documentId, cancellationToken);
            return Ok();
        }
        catch (DocumentNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}