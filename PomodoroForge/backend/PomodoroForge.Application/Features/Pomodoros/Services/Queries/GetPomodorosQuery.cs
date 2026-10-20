using MediatR;
using PomodoroForge.Application.Features.Pomodoros.DTOs;

namespace PomodoroForge.Application.Features.Pomodoros.Queries;

public record GetPomodorosQuery : IRequest<IEnumerable<PomodoroResponseDto>>;