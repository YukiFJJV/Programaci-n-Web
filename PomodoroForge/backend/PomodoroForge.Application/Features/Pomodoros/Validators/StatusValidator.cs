using FluentValidation;
using PomodoroForge.Application.Features.Pomodoros.Commands;
using PomodoroForge.Application.Features.Pomodoros.DTOs;

namespace PomodoroForge.Application.Features.Pomodoros.Validators
{
    public class StatusValidator : AbstractValidator<UpdatePomodoroStatusCommand>
    {
        public StatusValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("El estado especificado no es válido");
        }
    }
}