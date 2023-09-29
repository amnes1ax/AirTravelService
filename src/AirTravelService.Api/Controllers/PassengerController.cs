using System.ComponentModel.DataAnnotations;
using AirTravelService.Service.Exceptions.Passengers;
using AirTravelService.Service.Exceptions.Tickets;
using AirTravelService.Service.Models.Passenger;
using AirTravelService.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirTravelService.Api.Controllers;


[ApiController]
[Route("api/passenger")]
public partial class PassengerController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> CreatePassengerAsync(
        [FromServices] IPassengerBllService passengerService,
        [FromBody] [Required] CreationPassengerModel model,
        CancellationToken cancellationToken = default)
    {
        await passengerService.CreateAsync(new CreatePassengerModel
        {
            PassengerId = model.PassengerId!.Value,
            FirstName = model.FirstName!,
            LastName = model.LastName!,
            Patronymic = model.Patronymic,
        }, cancellationToken);

        return Ok();
    }
    
    [HttpGet("{passengerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync(
        [FromServices] IPassengerBllService passengerService,
        [FromRoute] [Required] Guid passengerId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await passengerService.GetByIdAsync(passengerId, cancellationToken);
            return Ok(response);
        }
        catch (PassengerNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpGet("by-ticket")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByTicketIdAsync(
        [FromServices] IPassengerBllService passengerService,
        [FromQuery] [Required] Guid ticketId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await passengerService.GetByTicketIdAsync(ticketId, cancellationToken);
            return Ok(response);
        }
        catch (PassengerNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (TicketNotFoundException ex)
        {
            return UnprocessableEntity(ex.Message);
        }
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetListAsync(
        [FromServices] IPassengerBllService passengerService,
        CancellationToken cancellationToken = default)
    {
        var response = await passengerService.GetListAsync(cancellationToken);
        return Ok(response);
    }
    
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> UpdateAsync(
        [FromServices] IPassengerBllService passengerService,
        [FromBody] [Required] ChangePassengerModel model,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await passengerService.UpdateAsync(new UpdatePassengerModel
            {
                PassengerId = model.PassengerId!.Value,
                FirstName = model.FirstName!,
                LastName = model.LastName!,
                Patronymic = model.Patronymic,
            }, cancellationToken);

            return Ok();
        }
        catch (PassengerNotFoundException ex)
        {
            return UnprocessableEntity(ex.Message);
        }
    }
    
    [HttpDelete("{passengerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> DeleteAsync(
        [FromServices] IPassengerBllService passengerService,
        [FromRoute] [Required] Guid passengerId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await passengerService.DeleteAsync(passengerId, cancellationToken);

            return Ok();
        }
        catch (PassengerNotFoundException ex)
        {
            return UnprocessableEntity(ex.Message);
        }
    }
}