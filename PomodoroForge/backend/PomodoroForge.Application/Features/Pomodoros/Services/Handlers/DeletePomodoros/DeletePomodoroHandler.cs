using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using PomodoroForge.Application.Features.Pomodoros.Commands;
using PomodoroForge.Application.Features.Pomodoros.DTOs;
using PomodoroForge.Application.Interfaces;
using PomodoroForge.Domain.Entities;
using PomodoroForge.Domain.Enums;

namespace PomodoroForge.Application.Features.Pomodoros.Handlers.DeletePomodoros
{
    public class DeletePomodoroHandler : IRequestHandler<DeletePomodoroCommand, PomodoroResponseDto?>
    {
        private IPomodoroRepository _pomodoroRepository;

        public DeletePomodoroHandler(IPomodoroRepository pomodoroRepository)
        {
            _pomodoroRepository = pomodoroRepository;
        }

        public async Task<PomodoroResponseDto?> Handle(DeletePomodoroCommand request, CancellationToken cancellationToken)
        {
            var pomodoro = await _pomodoroRepository.GetPomodoroByIdAsync(request.Id, cancellationToken);
            if(pomodoro == null)
                return null;

            var lastSession = pomodoro.Sessions?
                .OrderByDescending(s => s.FinishDate)
                .FirstOrDefault();

            var cardStatus = lastSession?.Status ?? PomodoroStatus.Pending;

            int totalAttempts = pomodoro.Sessions?.Count ?? 0;
            double completionRate = totalAttempts > 0?
                (double)pomodoro.TotalSessionsCompleted / totalAttempts * 100
                : 0;

            var deletedPomodoro = await _pomodoroRepository.DeletePomodoroAsync(pomodoro, cancellationToken);

            if(deletedPomodoro == null)
                return null;

            return new PomodoroResponseDto
            {
                Id = deletedPomodoro.Id,
                Name = deletedPomodoro.Name,
                Duration = deletedPomodoro.Duration,
                BreakTime = deletedPomodoro.BreakTime,
                LastUsedAt = deletedPomodoro.LastUsedAt,
                Status = cardStatus,

                Statistics = new()
                {
                    TotalSessionsCompleted = deletedPomodoro.TotalSessionsCompleted,
                    TotalFocusTime = deletedPomodoro.TotalFocusTime,
                    CompletionRate = completionRate
                }
            };
        }
    }
}