using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace PomodoroForge.API.Middlewares;

public sealed class ValidationExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not ValidationException validationException)
            return false;

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        var response = new
        {
            message = "Errores de validación",
            errors = validationException.Errors
                .Select(error => new
                {
                    property = error.PropertyName,
                    message = error.ErrorMessage
                })
                .ToArray()
        };

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
        return true;
    }
}
