using MediatR;
using PomodoroForge.Application.Features.Pomodoros.DTOs;
using PomodoroForge.Application.Interfaces;
using PomodoroForge.Domain.Enums;

namespace PomodoroForge.Application.Features.Pomodoros.Commands;

public record UpdatePomodoroStatusCommand(Guid Id, PomodoroStatus Status) : IRequest<PomodoroResponseDto?>;