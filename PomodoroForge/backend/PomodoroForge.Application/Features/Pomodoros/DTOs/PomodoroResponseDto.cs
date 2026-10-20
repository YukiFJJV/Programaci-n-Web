using PomodoroForge.Domain.Enums;
namespace PomodoroForge.Application.Features.Pomodoros.DTOs;

public class PomodoroResponseDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public int Duration { get; set; } = 25; // Default duration in minutes
    public int BreakTime { get; set; } = 10;
    public required PomodoroStatus Status { get; set; }
}