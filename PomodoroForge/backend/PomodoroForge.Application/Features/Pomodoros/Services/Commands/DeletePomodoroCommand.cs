using System.Text.Json.Serialization;
using MediatR;
using PomodoroForge.Application.Features.Pomodoros.DTOs;
using PomodoroForge.Application.Interfaces;

namespace PomodoroForge.Application.Features.Pomodoros.Commands;

public record DeletePomodoroCommand(
    [property: JsonIgnore] Guid Id) : IRequest<PomodoroResponseDto>;