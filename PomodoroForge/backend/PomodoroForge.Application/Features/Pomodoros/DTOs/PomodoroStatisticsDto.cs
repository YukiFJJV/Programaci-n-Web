using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PomodoroForge.Application.Features.Pomodoros.DTOs
{
    public class PomodoroStatisticsDto
    {
        public int TotalSessionsCompleted { get; set; }
        public double TotalFocusTime { get; set; }
        public double CompletionRate { get; set; }
    }
}