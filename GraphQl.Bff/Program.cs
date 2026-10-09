using GraphQl.Bff.GraphQl;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

//Configuracion de los HttpClient usando Service Discovery de Aspire
// El prefijo https+http:// le dice a Aspire que resuelva la URL automáticamente
builder.Services.AddHttpClient("agency-api", client =>
    client.BaseAddress = new Uri("https+http://agency-api"))
        .AddStandardResilienceHandler(); // POLLY

builder.Services.AddHttpClient("catalog-api", client =>
    client.BaseAddress = new Uri("https+http://catalog-api"))
    .AddStandardResilienceHandler(); // POLLY

//Configuracion HotChocolate
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddTypeExtension<TravelProductResolvers>();


var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();

app.UseHttpsRedirection();

app.MapGraphQL();

app.Run();


