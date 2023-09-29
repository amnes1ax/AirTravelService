using System.ComponentModel.DataAnnotations;
using AirTravelService.Service.Exceptions.Passengers;
using AirTravelService.Service.Exceptions.Tickets;
using AirTravelService.Service.Models.Ticket;
using AirTravelService.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirTravelService.Api.Controllers;

[ApiController]
[Route("api/ticket")]
public partial class TicketController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> CreateAsync(
        [FromServices] ITicketBllService ticketService,
        [FromBody] [Required] CreationTicketModel model,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await ticketService.CreateAsync(new CreateTicketModel
            {
                TicketId = model.TicketId!.Value,
                OrderNumber = model.OrderNumber!,
                ServiceProvider = model.ServiceProvider,
                DeparturePoint = model.DeparturePoint!,
                DestinationPoint = model.DestinationPoint!,
                DepartureDate = model.DepartureDate!.Value,
                ArrivalDate = model.ArrivalDate!.Value,
                RegistrationDate = model.RegistrationDate!.Value,
                PassengerId = model.PassengerId!.Value,
            }, cancellationToken);

            return Ok();
        }
        catch (TicketAlreadyExistsException ex)
        {
            return Conflict(ex.Message);
        }
        catch (PassengerNotFoundException ex)
        {
            return UnprocessableEntity(ex.Message);
        }
    }


    [HttpGet("{ticketId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync(
        [FromServices] ITicketBllService ticketService,
        [FromRoute] [Required] Guid ticketId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await ticketService.GetByIdAsync(ticketId, cancellationToken);
            return Ok(response);
        }
        catch (TicketNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("{passengerId:guid}/view")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetInfoViewByIdAsync(
        [FromServices] ITicketBllService ticketService,
        [FromRoute] [Required] Guid passengerId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await ticketService.GetInfoViewByIdAsync(passengerId, cancellationToken);
            return Ok(response);
        }
        catch (TicketNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("by-passenger")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetReportByPassengerAsync(
        [FromServices] ITicketBllService ticketService,
        [FromQuery] [Required] Guid passengerId,
        [FromQuery] [Required] DateTimeOffset startDate,
        [FromQuery] [Required] DateTimeOffset endDate,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response =
                await ticketService.GetReportByPassengerAsync(passengerId, startDate, endDate, cancellationToken);

            return Ok(response);
        }
        catch (PassengerNotFoundException ex)
        {
            return UnprocessableEntity(ex.Message);
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetListAsync(
        [FromServices] ITicketBllService ticketService,
        CancellationToken cancellationToken = default)
    {
        var response = await ticketService.GetListAsync(cancellationToken);

        return Ok(response);
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> UpdateAsync(
        [FromServices] ITicketBllService ticketService,
        [FromBody] [Required] ChangeTicketModel model,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await ticketService.UpdateAsync(new UpdateTicketModel
            {
                TicketId = model.TicketId!.Value,
                ServiceProvider = model.ServiceProvider,
                DeparturePoint = model.DeparturePoint!,
                DestinationPoint = model.DeparturePoint!,
                DepartureDate = model.DepartureDate!.Value,
                ArrivalDate = model.ArrivalDate!.Value
            }, cancellationToken);

            return Ok();
        }
        catch (TicketNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{ticketId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> DeleteAsync(
        [FromServices] ITicketBllService ticketService,
        [FromRoute] [Required] Guid ticketId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await ticketService.DeleteAsync(ticketId, cancellationToken);

            return Ok();
        }
        catch (TicketNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}