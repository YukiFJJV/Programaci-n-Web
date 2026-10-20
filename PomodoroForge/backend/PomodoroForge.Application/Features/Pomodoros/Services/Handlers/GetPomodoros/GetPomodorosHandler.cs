using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using PomodoroForge.Application.Features.Pomodoros.Commands;
using PomodoroForge.Application.Features.Pomodoros.DTOs;
using PomodoroForge.Application.Features.Pomodoros.Queries;
using PomodoroForge.Application.Interfaces;
using PomodoroForge.Domain.Entities;

namespace PomodoroForge.Application.Features.Pomodoros.Handlers.GetPomodoros
{
    public class GetPomodorosHandler : IRequestHandler<GetPomodorosQuery, IEnumerable<PomodoroResponseDto>>
    {
        private IPomodoroRepository _pomodoroRepository;

        public GetPomodorosHandler(IPomodoroRepository pomodoroRepository)
        {
            _pomodoroRepository = pomodoroRepository;
        }

        public async Task<IEnumerable<PomodoroResponseDto>> Handle(GetPomodorosQuery request, CancellationToken cancellationToken)
        {
            var pomodoros = await _pomodoroRepository.GetAllPomodorosAsync(cancellationToken);

            return pomodoros.Select(p => new PomodoroResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Duration = p.Duration,
                BreakTime = p.BreakTime,
                Status = p.Status
            });
        }
    }
}