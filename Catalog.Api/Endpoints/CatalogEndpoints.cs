using Catalog.Api.Application.UseCases;

namespace Catalog.Api.Endpoints
{
    public static class CatalogEndpoints
    {
        public static void MapCatalogEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/products").WithTags("Catalog");

            group.MapPost("/", async (CreateProductRequest request, CreateProductUseCase useCase) =>
            {
                var result = await useCase.ExecuteAsync(request);

                
                if (!result.IsSuccess)
                {
                    return Results.BadRequest(new { Error = result.ErrorMessage });
                }

                return Results.Created($"/api/products/{result.Value!.Id}", result.Value);
            });

            group.MapGet("/", async (GetAllProductsUseCase useCase) =>
            {
                var result = await useCase.ExecuteAsync();

                return Results.Ok(result.Value);
            });
        }
    }
}
