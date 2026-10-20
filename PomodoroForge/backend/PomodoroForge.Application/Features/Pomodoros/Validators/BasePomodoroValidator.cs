using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace PomodoroForge.Application.Features.Pomodoros.Validators
{
    public abstract class BasePomodoroValidator<T> : AbstractValidator<T>
        where T : notnull
    {
        protected void ApplyPomodoroRules(
            Func<T, string> nameSelector,
            Func<T, int> durationSelector,
            Func<T, int> breakTimeSelector)
        {
            RuleFor(x => nameSelector(x))
            .NotEmpty().WithMessage("El nombre del pomodoro no puede estar vacío.")
            .MinimumLength(3).WithMessage("El pomodoro debe tener al menos 3 caracteres.")
            .MaximumLength(100).WithMessage("El pomodoro es demasiado largo.");

        RuleFor(x => durationSelector(x))
            .GreaterThan(0).WithMessage("La duración no es válida")
            .LessThan(120).WithMessage("La duración no es válida")
            .NotNull().WithMessage("La duración no debe estar vacía.");

        RuleFor(x => breakTimeSelector(x))
            .GreaterThan(0).WithMessage("La duración no es válida")
            .LessThan(x => durationSelector(x)).WithMessage("El descanso no debe ser mayor que el tiempo de trabajo")
            .LessThan(120).WithMessage("La duración no es válida")
            .NotNull().WithMessage("La duración no debe estar vacía.");
        }
    }
}