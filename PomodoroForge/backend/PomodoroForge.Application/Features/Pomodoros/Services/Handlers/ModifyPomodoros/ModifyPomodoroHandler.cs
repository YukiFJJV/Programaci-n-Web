using MediatR;
using PomodoroForge.Application.Features.Pomodoros.Commands;
using PomodoroForge.Application.Features.Pomodoros.DTOs;
using PomodoroForge.Application.Interfaces;
using PomodoroForge.Domain.Enums;
namespace PomodoroForge.Application.Features.Pomodoros.Handlers.ModifyPomodoros
{
    public class ModifyPomodoroHandler : IRequestHandler<ModifyPomodoroCommand, PomodoroResponseDto?>
    {
        private readonly IPomodoroRepository _pomodoroRepository;

        public ModifyPomodoroHandler(IPomodoroRepository pomodoroRepository)
        {
            _pomodoroRepository = pomodoroRepository;
        }
        public async Task<PomodoroResponseDto?> Handle(ModifyPomodoroCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return null;
            }

            var pomodoro = await _pomodoroRepository.GetPomodoroByIdAsync(request.Id, cancellationToken);
            if (pomodoro == null)
            {
                return null;
            }

            var lastSession = pomodoro.Sessions?
                .OrderByDescending(s=> s.FinishDate)
                .FirstOrDefault();

            var cardStatus = lastSession?.Status ?? PomodoroStatus.Pending;

            int totalAttempts = pomodoro.Sessions?.Count ?? 0;
            double completionRate = totalAttempts > 0?
                (double)pomodoro.TotalSessionsCompleted/totalAttempts
                : 0
            ;

            pomodoro.Name = request.Name ?? pomodoro.Name;
            pomodoro.Duration = request.Duration;
            pomodoro.BreakTime = request.BreakTime;

            var updatedPomodoro = await _pomodoroRepository.ModifyPomodoroAsync(pomodoro, cancellationToken);
            if (updatedPomodoro == null)
            {
                return null;
            }

            return new PomodoroResponseDto
            {
                Id = updatedPomodoro.Id,
                Name = updatedPomodoro.Name,
                Duration = updatedPomodoro.Duration,
                BreakTime = updatedPomodoro.BreakTime,
                Status = cardStatus,
                LastUsedAt = updatedPomodoro.LastUsedAt,

                Statistics = new()
                {
                    TotalFocusTime = updatedPomodoro.TotalFocusTime,
                    TotalSessionsCompleted = updatedPomodoro.TotalSessionsCompleted,
                    CompletionRate = completionRate
                }
            };
        }
    }
}