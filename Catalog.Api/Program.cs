using Catalog.Api.Application.UseCases;
using Catalog.Api.Core.Ports;
using Catalog.Api.Endpoints;
using Catalog.Api.Infrastructure.Adapters;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//RabbitMQ

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        // Aspire inyecta automáticamente la cadena de conexión "messaging"
        cfg.Host(builder.Configuration.GetConnectionString("messaging"));
    });
});

builder.AddMongoDBClient("CatalogDb");

// Adaptador Secundario (Infraestructura)
builder.Services.AddScoped<IProductRepository, MongoProductRepository>();

// Caso de Uso (Aplicación)
builder.Services.AddScoped<CreateProductUseCase>();
builder.Services.AddScoped<GetAllProductsUseCase>();
// -------------------------------------------

var app = builder.Build();



app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapCatalogEndpoints();

app.Run();

