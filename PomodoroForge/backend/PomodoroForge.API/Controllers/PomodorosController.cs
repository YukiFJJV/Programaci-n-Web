using Microsoft.AspNetCore.Mvc;
using PomodoroForge.Application.Features.Pomodoros.DTOs;
using MediatR;
using PomodoroForge.Application.Features.Pomodoros.Commands;
using PomodoroForge.Application.Features.Pomodoros.Queries;
using PomodoroForge.Domain.Enums;

namespace PomodoroForge.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PomodorosController(IMediator _mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PomodoroResponseDto>>> GetAllPomodoros(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetPomodorosQuery(), cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PomodoroResponseDto>> GetPomodoroById(Guid id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetPomodoroByIdQuery(id), cancellationToken);

        return response == null
            ? NotFound(new { message  = "El pomodoro no se encontró" })
            : Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddPomodoro([FromBody] CreatePomodoroCommand command, CancellationToken cancellationToken)
    {
        var createdPomodoro = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetPomodoroById), new { id = createdPomodoro.Id }, createdPomodoro);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<PomodoroResponseDto>> EditPomodoro(
        Guid id,
        [FromBody] ModifyPomodoroCommand command,
        CancellationToken cancellationToken)
    {
        var secureCommand = command with { Id = id };
        var modifiedPomodoro = await _mediator.Send(secureCommand, cancellationToken);

        return modifiedPomodoro == null
            ? NotFound(new { message = "El pomodoro no se encontró" })
            : Ok(modifiedPomodoro);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePomodoro(Guid id, CancellationToken cancellationToken)
    {
        var secureCommand = new DeletePomodoroCommand(id);
        var request = await _mediator.Send(secureCommand, cancellationToken);

        return request == null
            ? NotFound(new { message  = "El pomodoro no se encontró" })
            : Ok(new { message  = "El pomodoro se borró con éxito" });
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<ActionResult<PomodoroResponseDto?>> UpdatePomodoroStatus(Guid id, [FromBody] PomodoroStatus status, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdatePomodoroStatusCommand(id, status), cancellationToken);

        return response == null
            ? NotFound(new { message = "El pomodoro no se encontró" })
            : Ok(response);
    }
}