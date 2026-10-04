using DotNetEnv;
using PomodoroForge.Application;
using PomodoroForge.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresConnection")
                    ?? throw new InvalidOperationException("Falta la cadena de conexión.");

builder.Services.AddApplication();
builder.Services.AddInfrastructure(connectionString);
builder.Services.AddControllers();

// 2. Generación nativa de OpenAPI (viene por defecto en .NET 10)
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference(options =>
    {
        options.WithOpenApiRoutePattern("/openapi/v1.json");
    });
}

app.MapControllers();
app.Run();