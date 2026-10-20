using System.Text.Json.Serialization;
using MediatR;
using PomodoroForge.Application.Features.Pomodoros.DTOs;
using PomodoroForge.Application.Interfaces;

namespace PomodoroForge.Application.Features.Pomodoros.Commands;

public record ModifyPomodoroCommand(
    [property: JsonIgnore] Guid Id,
    string Name,
    int Duration,
    int BreakTime) : IRequest<PomodoroResponseDto>;