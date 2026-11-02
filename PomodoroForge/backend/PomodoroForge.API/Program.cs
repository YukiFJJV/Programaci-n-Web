using DotNetEnv;
using PomodoroForge.Application;
using PomodoroForge.Infrastructure;
using PomodoroForge.API.Middlewares;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresConnection")
                    ?? throw new InvalidOperationException("Falta la cadena de conexión.");

builder.Services.AddApplication();
builder.Services.AddInfrastructure(connectionString);
builder.Services.AddControllers();
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseExceptionHandler();

app.MapOpenApi();

app.MapScalarApiReference("/scalar", options =>
{
    options.WithOpenApiRoutePattern("/openapi/v1.json");
});

app.MapControllers();
app.Run();