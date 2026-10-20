using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PomodoroForge.Application.Features.Pomodoros.Commands;
using PomodoroForge.Application.Features.Pomodoros.DTOs;
using PomodoroForge.Application.Interfaces;

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
                Status = updatedPomodoro.Status
            };
        }
    }
}