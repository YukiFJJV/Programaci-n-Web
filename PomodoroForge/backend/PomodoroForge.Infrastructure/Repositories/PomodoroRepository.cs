using Microsoft.EntityFrameworkCore;
using PomodoroForge.Application.Interfaces;
using PomodoroForge.Domain.Entities;
using PomodoroForge.Infrastructure.Data;

namespace PomodoroForge.Infrastructure.Repositories;

public class GlobalPomodoroRepository : IPomodoroRepository
{
    private readonly PomodoroForgeDbContext _context;

    public GlobalPomodoroRepository(PomodoroForgeDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GlobalPomodoro>> GetAllAsync()
    {
        return await _context.GlobalPomodoros.ToListAsync();
    }

    public async Task<GlobalPomodoro> AddAsync(GlobalPomodoro pomodoro)
    {
        await _context.GlobalPomodoros.AddAsync(pomodoro);
        await _context.SaveChangesAsync();
        return pomodoro;
    }
}