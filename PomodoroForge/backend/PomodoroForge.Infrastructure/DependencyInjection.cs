using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PomodoroForge.Application.Interfaces;
using PomodoroForge.Infrastructure.Data;
using PomodoroForge.Infrastructure.Repositories;

namespace PomodoroForge.Infrastructure;

public static class DependencyInjection {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString) {

        services.AddDbContext<PomodoroForgeDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IPomodoroRepository, GlobalPomodoroRepository>();

        return services;
    }
}
