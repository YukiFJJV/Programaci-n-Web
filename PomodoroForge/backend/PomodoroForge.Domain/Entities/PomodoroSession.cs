using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PomodoroForge.Domain.Enums;

namespace PomodoroForge.Domain.Entities
{
    public class PomodoroSession
    {
        public Guid Id { get; set; }
        public Guid GlobalPomodoroId { get; set; }
        public GlobalPomodoro GlobalPomodoro { get; set; } = null!;
        public int Cycles { get; set; } = 1;
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public PomodoroStatus Status { get; set; }
    }
}