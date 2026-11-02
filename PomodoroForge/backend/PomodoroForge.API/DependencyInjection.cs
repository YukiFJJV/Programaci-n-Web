using PomodoroForge.API.Middlewares;

namespace PomodoroForge.API.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddExceptionHandler<ValidationExceptionHandler>();
        services.AddProblemDetails();
        return services;
    }
}