using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using PomodoroForge.Application.Features.Pomodoros.Queries;
using PomodoroForge.Application.Features.Pomodoros.DTOs;
using PomodoroForge.Application.Interfaces;
using PomodoroForge.Domain.Entities;

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
            if(request == null)
                return null;

            var pomodoro = await _pomodoroRepository.GetPomodoroByIdAsync(request.Id, cancellationToken);

            return pomodoro == null?
                null
                : new PomodoroResponseDto
                {
                    Id = pomodoro.Id,
                    Name = pomodoro.Name,
                    Duration = pomodoro.Duration,
                    BreakTime = pomodoro.BreakTime,
                    Status = pomodoro.Status
                }
            ;
        }
    }
}