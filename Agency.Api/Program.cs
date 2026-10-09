using Agency.Api.Application.Interfaces.Commands;
using Agency.Api.Application.Interfaces.Queries;
using Agency.Api.Application.UseCases;
using Agency.Api.Endpoints;
using Agency.Api.Infrastructure;
using Agency.Api.Infrastructure.Repositories.Commands;
using Agency.Api.Infrastructure.Repositories.Queries;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.AddSqlServerDbContext<AgencyDbContext>("AgenciesDb");

// Repositorios (Infraestructura)
builder.Services.AddScoped<IAgencyCommandRepository, AgencyCommandRepository>();
builder.Services.AddScoped<IAgencyQueryRepository, AgencyQueryRepository>();

// Casos de Uso (Aplicación)
builder.Services.AddScoped<CreateAgencyUseCase>();
builder.Services.AddScoped<GetAllAgenciesUseCase>();

var app = builder.Build();

app.MapDefaultEndpoints();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AgencyDbContext>();

    // Ejecuta la migración usando la estrategia de reintentos configurada por Aspire
    var strategy = db.Database.CreateExecutionStrategy();
    strategy.Execute(() =>
    {
        db.Database.Migrate();
    });
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Registrar Endpoints
app.MapAgencyEndpoints();

app.UseHttpsRedirection();

app.Run();

