using FluentValidation;
using PomodoroForge.Application.Features.Pomodoros.DTOs;

namespace PomodoroForge.Application.Features.Pomodoros.Validators;

public class CreatePomodoroValidator : AbstractValidator<CreatePomodoroRequestDto> {
    public CreatePomodoroValidator() {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del pomodoro no puede estar vacío.")
            .MinimumLength(3).WithMessage("El pomodoro debe tener al menos 3 caracteres.")
            .MaximumLength(100).WithMessage("El pomodoro es demasiado largo.");
    }
}
