using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PomodoroForge.Application.Features.Pomodoros.Commands;
using PomodoroForge.Application.Features.Behaviors;

namespace PomodoroForge.Application;

public static class DependencyInjection {
    public static IServiceCollection AddApplication(this IServiceCollection services) {

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        // Handlers
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<CreatePomodoroCommand>();

            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        return services;
    }
}
