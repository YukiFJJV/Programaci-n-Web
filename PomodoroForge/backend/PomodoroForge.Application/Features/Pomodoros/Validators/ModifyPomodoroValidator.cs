using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using PomodoroForge.Application.Features.Pomodoros.Commands;

namespace PomodoroForge.Application.Features.Pomodoros.Validators
{
    public class ModifyPomodoroValidator : BasePomodoroValidator<ModifyPomodoroCommand>
    {
        public ModifyPomodoroValidator()
        {
            ApplyPomodoroRules(
                x => x.Name,
                x => x.Duration,
                x => x.BreakTime
            );
        }
    }
}