using MediatR;
using PomodoroForge.Application.Features.Pomodoros.DTOs;

namespace PomodoroForge.Application.Features.Pomodoros.Queries;

public record GetPomodoroByIdQuery(Guid Id) : IRequest<PomodoroResponseDto?>;