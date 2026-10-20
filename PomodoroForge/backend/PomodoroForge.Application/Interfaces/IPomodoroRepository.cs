using PomodoroForge.Application.Features.Pomodoros.DTOs;
using PomodoroForge.Domain.Entities;

namespace PomodoroForge.Application.Interfaces;

public interface IPomodoroRepository {
    Task<IEnumerable<GlobalPomodoro>> GetAllPomodorosAsync(CancellationToken cancellationToken);
    Task<GlobalPomodoro> AddPomodoroAsync(GlobalPomodoro pomodoro, CancellationToken cancellationToken);
    Task<GlobalPomodoro?> GetPomodoroByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<GlobalPomodoro?> ModifyPomodoroAsync(GlobalPomodoro? pomodoro, CancellationToken cancellationToken);
    Task<GlobalPomodoro?> DeletePomodoroAsync(GlobalPomodoro pomodoro, CancellationToken cancellationToken);
    Task<GlobalPomodoro?> UpdatePomodoroStatusAsync(GlobalPomodoro pomodoro, CancellationToken cancellationToken);
}