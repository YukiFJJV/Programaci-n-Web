using Microsoft.EntityFrameworkCore;
using PomodoroForge.Application.Interfaces;
using PomodoroForge.Domain.Entities;
using PomodoroForge.Infrastructure.Data;

namespace PomodoroForge.Infrastructure.Repositories;

// Implementamos la interfaz de la capa superior
public class GlobalPomodoroRepository : IPomodoroRepository {
    private readonly PomodoroForgeDbContext _context;
    
    // El constructor recibe el contexto de EF Core
    public GlobalPomodoroRepository(PomodoroForgeDbContext context) {
        _context = context;
    }

    public async Task<IEnumerable<GlobalPomodoro>> GetAllAsync() {
        return await _context.HabitosGlobales.ToListAsync(); // Consulta real a BD
    }
}