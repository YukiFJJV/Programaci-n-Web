using MediatR;
using PomodoroForge.Application.Features.Pomodoros.DTOs;
using PomodoroForge.Application.Interfaces;

namespace PomodoroForge.Application.Features.Pomodoros.Commands;

public record CreatePomodoroCommand(string Name, int Duration, int BreakTime) : IRequest<PomodoroResponseDto>;