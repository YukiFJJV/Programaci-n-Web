using MediatR;
using PomodoroForge.Application.Features.Pomodoros.Commands;
using PomodoroForge.Application.Features.Pomodoros.DTOs;
using PomodoroForge.Application.Interfaces;
using PomodoroForge.Domain.Entities;
using PomodoroForge.Domain.Enums;

namespace PomodoroForge.Application.Features.Pomodoros.Handlers.UpdatePomodoroStatus;

public class UpdatePomodoroStatusHandler : IRequestHandler<UpdatePomodoroStatusCommand, PomodoroResponseDto?>
{
    private readonly IPomodoroRepository _pomodoroRepository;

    public UpdatePomodoroStatusHandler(IPomodoroRepository pomodoroRepository)
    {
        _pomodoroRepository = pomodoroRepository ?? throw new ArgumentNullException(nameof(pomodoroRepository));
    }

    public async Task<PomodoroResponseDto?> Handle(UpdatePomodoroStatusCommand request, CancellationToken cancellationToken)
    {
        var pomodoro = await _pomodoroRepository.GetPomodoroByIdAsync(request.Id, cancellationToken);
        if (pomodoro == null)
            return null;

        var lastSession = pomodoro.Sessions
            .OrderByDescending(session => session.StartDate)
            .FirstOrDefault();
        var startsNewSession = lastSession == null
            || lastSession.Status is PomodoroStatus.Completed or PomodoroStatus.Interrupted;

        PomodoroSession? session = null;
        if (startsNewSession)
        {
            if (lastSession == null)
                return null;
            session = new PomodoroSession
            {
                Id = Guid.NewGuid(),
                GlobalPomodoroId = pomodoro.Id,
                GlobalPomodoro = pomodoro,
                Cycles = lastSession.Cycles,
                StartDate = DateTime.UtcNow,
                FinishDate = DateTime.UtcNow,
                Status = request.Status
            };
            pomodoro.Sessions.Add(session);
        }
        else if (startsNewSession && lastSession != null)
        {
            session = new PomodoroSession
            {
                Id = Guid.NewGuid(),
                GlobalPomodoroId = pomodoro.Id,
                GlobalPomodoro = pomodoro,
                Cycles = lastSession.Cycles,
                StartDate = DateTime.UtcNow,
                FinishDate = DateTime.UtcNow,
                Status = request.Status
            };
            pomodoro.Sessions.Add(session);
        }
        else if (lastSession != null)
        {
            session = lastSession;
            session.Status = request.Status;
            if (request.Status is PomodoroStatus.Completed or PomodoroStatus.Interrupted)
                session.FinishDate = DateTime.UtcNow;
        }

        if (session == null)
            return null;

        var completedNow = session.Status == PomodoroStatus.Completed
            && lastSession?.Status != PomodoroStatus.Completed;
        if (completedNow)
        {
            pomodoro.TotalSessionsCompleted++;
            pomodoro.TotalFocusTime += pomodoro.Duration;
        }

        pomodoro.TotalSessionsAttempted = pomodoro.Sessions.Count;
        pomodoro.LastUsedAt = DateTime.UtcNow;

        var updatedPomodoro = await _pomodoroRepository.UpdatePomodoroStatusAsync(pomodoro, cancellationToken);
        if (updatedPomodoro == null)
            return null;

        var cardStatus = updatedPomodoro.Sessions
            .OrderByDescending(item => item.StartDate)
            .FirstOrDefault()?.Status ?? PomodoroStatus.Pending;
        var totalAttempts = updatedPomodoro.TotalSessionsAttempted > 0
            ? updatedPomodoro.TotalSessionsAttempted
            : updatedPomodoro.Sessions.Count;
        var completionRate = totalAttempts > 0
            ? (double)updatedPomodoro.TotalSessionsCompleted / totalAttempts * 100
            : 0;

        return new PomodoroResponseDto
        {
            Id = updatedPomodoro.Id,
            Name = updatedPomodoro.Name,
            Duration = updatedPomodoro.Duration,
            BreakTime = updatedPomodoro.BreakTime,
            Status = cardStatus,
            LastUsedAt = updatedPomodoro.LastUsedAt,
            Statistics = new PomodoroStatisticsDto
            {
                TotalSessionsCompleted = updatedPomodoro.TotalSessionsCompleted,
                TotalFocusTime = updatedPomodoro.TotalFocusTime,
                CompletionRate = completionRate
            }
        };
    }
}
