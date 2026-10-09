using GraphQl.Bff.Models;

namespace GraphQl.Bff.GraphQl
{
    public class Query
    {
        // El atributo [Service] le dice a HotChocolate que inyecte la dependencia
        public async Task<IEnumerable<TravelProductResponse>> GetProductsAsync([Service] IHttpClientFactory clientFactory)
        {
            // Usamos el nombre exacto que le pusiste en el AppHost: "catalog-api"
            var client = clientFactory.CreateClient("catalog-api");
            var response = await client.GetFromJsonAsync<IEnumerable<TravelProductResponse>>("/api/products");
            return response ?? Array.Empty<TravelProductResponse>();
        }

        public async Task<IEnumerable<AgencyResponse>> GetAgenciesAsync([Service] IHttpClientFactory clientFactory)
        {
            // Usamos el nombre exacto del AppHost: "agency-api"
            var client = clientFactory.CreateClient("agency-api");
            var response = await client.GetFromJsonAsync<IEnumerable<AgencyResponse>>("/api/agencies");
            return response ?? Array.Empty<AgencyResponse>();
        }
    }
}
