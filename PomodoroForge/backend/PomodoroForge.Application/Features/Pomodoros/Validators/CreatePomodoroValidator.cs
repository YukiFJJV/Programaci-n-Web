using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PomodoroForge.Application.Features.Pomodoros.Commands;

namespace PomodoroForge.Application.Features.Pomodoros.Validators
{
    public class CreatePomodoroValidator : BasePomodoroValidator<CreatePomodoroCommand>
    {
        public CreatePomodoroValidator()
        {
            ApplyPomodoroRules(
                x => x.Name,
                x => x.Duration,
                x => x.BreakTime
            );
        }
    }
}