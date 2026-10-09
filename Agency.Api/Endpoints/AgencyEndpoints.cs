using Agency.Api.Application.DTOs;
using Agency.Api.Application.UseCases;

namespace Agency.Api.Endpoints
{
    public static class AgencyEndpoints
    {
        public static void MapAgencyEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/agencies").WithTags("Agencies");

            // Inyectamos el Caso de Uso de creación
            group.MapPost("/", async (CreateAgencyRequest request, CreateAgencyUseCase useCase) =>
            {
                var result = await useCase.ExecuteAsync(request);
                return Results.Created($"/api/agencies/{result.Id}", result);
            });

            // Inyectamos el Caso de Uso de lectura
            group.MapGet("/", async (GetAllAgenciesUseCase useCase) =>
            {
                var result = await useCase.ExecuteAsync();
                return Results.Ok(result);
            });
        }
    }
}
