using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PomodoroForge.Application.Features.Pomodoros.DTOs
{
    public class CreatePomodoroRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public int Duration { get; set; } = 25; // Default duration in minutes
    }
}