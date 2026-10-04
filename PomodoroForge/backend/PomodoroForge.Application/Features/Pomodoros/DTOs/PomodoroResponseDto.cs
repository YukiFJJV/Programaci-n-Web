namespace PomodoroForge.Application.Features.Pomodoros.DTOs;

public class PomodoroResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Duration { get; set; } = 25; // Default duration in minutes
}