using PomodoroForge.Domain.Enums;
namespace PomodoroForge.Application.Features.Pomodoros.DTOs;

public class PomodoroResponseDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public double Duration { get; set; } = 25; // Default duration in minutes
    public double BreakTime { get; set; } = 10;
    public DateTime? LastUsedAt { get; set; }
    public PomodoroStatus Status { get; set; } = PomodoroStatus.Pending;
    public PomodoroStatisticsDto Statistics { get; set; } = new();
}