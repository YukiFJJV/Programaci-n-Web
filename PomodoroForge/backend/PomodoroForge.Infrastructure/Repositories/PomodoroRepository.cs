using Microsoft.EntityFrameworkCore;
using PomodoroForge.Application.Interfaces;
using PomodoroForge.Domain.Entities;
using PomodoroForge.Infrastructure.Data;
using PomodoroForge.Application.Features.Pomodoros.DTOs;

namespace PomodoroForge.Infrastructure.Repositories;

public class GlobalPomodoroRepository : IPomodoroRepository
{
    private readonly PomodoroForgeDbContext _context;

    public GlobalPomodoroRepository(PomodoroForgeDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GlobalPomodoro>> GetAllPomodorosAsync(CancellationToken cancellationToken)
    {
        return await _context.GlobalPomodoros.ToListAsync(cancellationToken);
    }

    public async Task<GlobalPomodoro> AddPomodoroAsync(GlobalPomodoro pomodoro, CancellationToken cancellationToken)
    {
        await _context.GlobalPomodoros.AddAsync(pomodoro, cancellationToken);
        await _context.SaveChangesAsync();
        return pomodoro;
    }

    public async Task<GlobalPomodoro?> GetPomodoroByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.GlobalPomodoros.FindAsync(id, cancellationToken);
    }

    public async Task<GlobalPomodoro?> ModifyPomodoroAsync(GlobalPomodoro? pomodoro, CancellationToken cancellationToken)
    {
        if (pomodoro == null)
        {
            return null;
        }

        _context.GlobalPomodoros.Update(pomodoro);
        await _context.SaveChangesAsync(cancellationToken);
        return pomodoro;
    }

    public async Task<GlobalPomodoro?> DeletePomodoroAsync(GlobalPomodoro pomodoro, CancellationToken cancellationToken)
    {
        if (pomodoro == null)
        {
            return null;
        }

        _context.GlobalPomodoros.Remove(pomodoro);
        await _context.SaveChangesAsync(cancellationToken);
        return pomodoro;
    }

    public async Task<GlobalPomodoro?> UpdatePomodoroStatusAsync(GlobalPomodoro pomodoro, CancellationToken cancellationToken)
    {
        if (pomodoro == null)
            return null;

        _context.GlobalPomodoros.Update(pomodoro);
        await _context.SaveChangesAsync(cancellationToken);
        return pomodoro;
    }
}