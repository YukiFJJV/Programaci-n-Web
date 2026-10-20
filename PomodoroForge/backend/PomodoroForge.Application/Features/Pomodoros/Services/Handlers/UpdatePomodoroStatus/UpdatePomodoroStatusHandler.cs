using PomodoroForge.Domain.Enums;
using PomodoroForge.Application.Interfaces;
using MediatR;
using PomodoroForge.Application.Features.Pomodoros.Commands;
using PomodoroForge.Application.Features.Pomodoros.DTOs;

namespace PomodoroForge.Application.Features.Pomodoros.Handlers.UpdatePomodoroStatus;

public class UpdatePomodoroStatusHandler : IRequestHandler<UpdatePomodoroStatusCommand, PomodoroResponseDto?>
{
    private readonly IPomodoroRepository _pomodoroRepository;

    public UpdatePomodoroStatusHandler(IPomodoroRepository pomodoroRepository)
    {
        _pomodoroRepository = pomodoroRepository;
    }

    public async Task<PomodoroResponseDto?> Handle(UpdatePomodoroStatusCommand request, CancellationToken cancellationToken)
    {
        var pomodoro = await _pomodoroRepository.GetPomodoroByIdAsync(request.Id, cancellationToken);

        if (pomodoro == null)
            return null;

        pomodoro.Status = request.Status;
        await _pomodoroRepository.UpdatePomodoroStatusAsync(pomodoro, cancellationToken);

        return new PomodoroResponseDto
        {
            Id = pomodoro.Id,
            Name = pomodoro.Name,
            Duration = pomodoro.Duration,
            BreakTime = pomodoro.BreakTime,
            Status = pomodoro.Status
        };
    }
}