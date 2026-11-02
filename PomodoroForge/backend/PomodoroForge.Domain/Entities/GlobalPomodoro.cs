using PomodoroForge.Domain.Enums;
namespace PomodoroForge.Domain.Entities;

public class GlobalPomodoro
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Duration { get; set; }
    public double BreakTime { get; set; }
    public DateTime? LastUsedAt { get; set; }
    public int TotalSessionsCompleted { get; set; } = 0;
    public int TotalSessionsAttempted { get; set; } = 0;
    public double TotalFocusTime { get; set; } = 0;
    public ICollection<PomodoroSession> Sessions { get; set; } = new List<PomodoroSession>();
}