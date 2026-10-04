using Microsoft.EntityFrameworkCore;
using PomodoroForge.Domain.Entities;

namespace PomodoroForge.Infrastructure.Data;

public class PomodoroForgeDbContext : DbContext
{
    public PomodoroForgeDbContext(DbContextOptions<PomodoroForgeDbContext> options) : base(options) { }
    public DbSet<GlobalPomodoro> GlobalPomodoros { get; set; }
}