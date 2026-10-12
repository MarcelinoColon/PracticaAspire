using GraphQl.Bff.Models;
using GraphQl.Bff.Models.Inputs;

namespace GraphQl.Bff.GraphQl
{
    public class Mutation
    {
        public async Task<TravelProductResponse> CreateProductAsync(
            CreateTravelProductInput input,
            [Service] IHttpClientFactory clientFactory)
        {
            var client = clientFactory.CreateClient("catalog-api");

            // Enviamos el Input como JSON al microservicio
            var response = await client.PostAsJsonAsync("/api/products", input);

            // Validamos que el microservicio lo haya creado bien
            response.EnsureSuccessStatusCode();

            // Leemos y retornamos la respuesta final
            var createdProduct = await response.Content.ReadFromJsonAsync<TravelProductResponse>();
            return createdProduct!;
        }
    }
}
