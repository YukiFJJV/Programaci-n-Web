using MediatR;
using PomodoroForge.Application.Features.Pomodoros.Queries;
using PomodoroForge.Application.Features.Pomodoros.DTOs;
using PomodoroForge.Application.Interfaces;
using PomodoroForge.Domain.Enums;

namespace PomodoroForge.Application.Features.Pomodoros.Handlers.GetPomodoros
{
    public class GetPomodorosHandler(IPomodoroRepository pomodoroRepository) : IRequestHandler<GetPomodorosQuery, IEnumerable<PomodoroResponseDto>>
    {
        private readonly IPomodoroRepository _pomodoroRepository = pomodoroRepository;

        public async Task<IEnumerable<PomodoroResponseDto>> Handle(GetPomodorosQuery request, CancellationToken cancellationToken)
        {
            // Obtenemos todos los pomodoros
            var pomodoros = await _pomodoroRepository.GetAllPomodorosAsync(cancellationToken);

            return [.. pomodoros.Select(pomodoro =>
            {
                // Obtener el estado de la última sesión
                var lastSession = pomodoro.Sessions?
                    .OrderByDescending(s => s.FinishDate)
                    .FirstOrDefault();

                var cardStatus = lastSession?.Status ?? PomodoroStatus.Pending;

                // Calcular la tasa de completados
                int totalAttempts = pomodoro.Sessions?.Count ?? 0;
                double completionRate = totalAttempts > 0
                    ? (double)pomodoro.TotalSessionsCompleted / totalAttempts * 100
                    : 0;

                return new PomodoroResponseDto
                {
                    Id = pomodoro.Id,
                    Name = pomodoro.Name,
                    Duration = pomodoro.Duration,
                    BreakTime = pomodoro.BreakTime,
                    LastUsedAt = pomodoro.LastUsedAt,
                    Status = cardStatus,
                    Statistics = new()
                    {
                        TotalSessionsCompleted = pomodoro.TotalSessionsCompleted,
                        TotalFocusTime = (int)pomodoro.TotalFocusTime,
                        CompletionRate = Math.Round(completionRate, 2)
                    }
                };
            })];
        }
    }
}