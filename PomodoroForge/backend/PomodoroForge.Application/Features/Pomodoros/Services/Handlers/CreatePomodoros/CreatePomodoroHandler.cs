using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PomodoroForge.Application.Features.Pomodoros.Commands;
using PomodoroForge.Application.Features.Pomodoros.DTOs;
using PomodoroForge.Application.Interfaces;
using PomodoroForge.Domain.Entities;

namespace PomodoroForge.Application.Features.Pomodoros.Handlers.CreatePomodoros;

public class CreatePomodoroHandler : IRequestHandler<CreatePomodoroCommand, PomodoroResponseDto>
{
    private readonly IPomodoroRepository _pomodoroRepository;

    public CreatePomodoroHandler(IPomodoroRepository pomodoroRepository)
    {
        _pomodoroRepository = pomodoroRepository;
    }

    public async Task<PomodoroResponseDto> Handle(CreatePomodoroCommand request, CancellationToken cancellationToken)
    {
        var pomodoro = new GlobalPomodoro
        {
            Name = request.Name,
            Duration = request.Duration,
            BreakTime = request.BreakTime,
            Status = Domain.Enums.PomodoroStatus.Pending
        };

        var createdPomodoro = await _pomodoroRepository.AddPomodoroAsync(pomodoro, cancellationToken);

        return new PomodoroResponseDto
        {
            Id = createdPomodoro.Id,
            Name = createdPomodoro.Name,
            Duration = createdPomodoro.Duration,
            BreakTime = createdPomodoro.BreakTime,
            Status = createdPomodoro.Status
        };
    }
}