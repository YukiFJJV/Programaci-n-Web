using PomodoroForge.Domain.Entities;

namespace PomodoroForge.Application.Interfaces;

public interface IPomodoroRepository {
    Task<IEnumerable<GlobalPomodoro>> GetAllAsync();
    Task<GlobalPomodoro> AddAsync(GlobalPomodoro pomodoro);
}