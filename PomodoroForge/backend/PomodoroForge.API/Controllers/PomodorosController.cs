using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PomodoroForge.Application.Features.Pomodoros.DTOs;
using PomodoroForge.Application.Interfaces;
using PomodoroForge.Domain.Entities;

namespace PomodoroForge.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PomodorosController : ControllerBase
{
    private readonly IPomodoroRepository _repository;
    private readonly IValidator<CreatePomodoroRequestDto> _validator;

    public PomodorosController(IPomodoroRepository repository, IValidator<CreatePomodoroRequestDto> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetPomodoros()
    {
        var pomodoros = await _repository.GetAllAsync();

        var response = pomodoros.Select(p => new PomodoroResponse
        {
            Id = p.Id,
            Name = p.Name,
            Duration = p.Duration
        }).ToList();

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CrearPomodoro([FromBody] CreatePomodoroRequestDto request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var nuevoPomodoro = new GlobalPomodoro
        {
            Name = request.Name,
            Duration = request.Duration
        };

        var pomodoroCreado = await _repository.AddAsync(nuevoPomodoro);

        var response = new PomodoroResponse
        {
            Id = pomodoroCreado.Id,
            Name = pomodoroCreado.Name,
            Duration = pomodoroCreado.Duration
        };

        return CreatedAtAction(nameof(GetPomodoros), new { id = response.Id }, response);
    }
}
