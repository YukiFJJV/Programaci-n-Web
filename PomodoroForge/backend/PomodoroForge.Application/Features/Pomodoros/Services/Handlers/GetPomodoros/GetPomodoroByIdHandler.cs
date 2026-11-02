using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PomodoroForge.Application.Features.Pomodoros.Queries;
using PomodoroForge.Application.Features.Pomodoros.DTOs;
using PomodoroForge.Application.Interfaces;
using PomodoroForge.Domain.Enums;

namespace PomodoroForge.Application.Features.Pomodoros.Handlers.GetPomodoros
{
    public class GetPomodoroByIdHandler : IRequestHandler<GetPomodoroByIdQuery, PomodoroResponseDto?>
    {
        private readonly IPomodoroRepository _pomodoroRepository;

        public GetPomodoroByIdHandler(IPomodoroRepository pomodoroRepository)
        {
            _pomodoroRepository = pomodoroRepository;
        }

        public async Task<PomodoroResponseDto?> Handle(GetPomodoroByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                return null;

            var pomodoro = await _pomodoroRepository.GetPomodoroByIdAsync(request.Id, cancellationToken);

            if (pomodoro == null)
                return null;

            // Obtener el estado de la última sesión
            var lastSession = pomodoro.Sessions?
                .OrderByDescending(s => s.FinishDate)
                .FirstOrDefault();

            PomodoroStatus cardStatus = lastSession?.Status ?? PomodoroStatus.Pending;

            int totalAttempts = pomodoro.Sessions?.Count ?? 0;
            double completionRate = totalAttempts > 0
                ? (double)pomodoro.TotalSessionsCompleted / totalAttempts * 100
                : 0;

            // Mapear hacia el DTO
            return new PomodoroResponseDto
            {
                Id = pomodoro.Id,
                Name = pomodoro.Name,
                Duration = pomodoro.Duration,
                BreakTime = pomodoro.BreakTime,
                LastUsedAt = pomodoro.LastUsedAt,
                Status = cardStatus,

                Statistics = new PomodoroStatisticsDto
                {
                    TotalSessionsCompleted = pomodoro.TotalSessionsCompleted,
                    TotalFocusTime = (int)pomodoro.TotalFocusTime,
                    CompletionRate = Math.Round(completionRate, 2),
                }
            };
        }
    }
}