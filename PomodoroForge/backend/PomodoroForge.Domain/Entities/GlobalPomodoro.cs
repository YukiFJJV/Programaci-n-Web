using PomodoroForge.Domain.Enums;
namespace PomodoroForge.Domain.Entities;

public class GlobalPomodoro
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Duration { get; set; }
    public int BreakTime { get; set; }

    public PomodoroStatus Status { get; set; } = PomodoroStatus.Pending;
}