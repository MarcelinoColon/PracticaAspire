using GraphQl.Bff.Models;

namespace GraphQl.Bff.GraphQl
{
    [ExtendObjectType(typeof(TravelProductResponse))]
    public class TravelProductResolvers
    {
        // GraphQL convertirá este método en un campo "b2bPrice(agencyId: Int!)"
        public async Task<decimal> GetB2bPriceAsync(
            [Parent] TravelProductResponse product,
            int agencyId,
            [Service] IHttpClientFactory clientFactory)
        {
            var client = clientFactory.CreateClient("agency-api");

            // Consultamos al microservicio relacional (SQL Server)
            var agencies = await client.GetFromJsonAsync<IEnumerable<AgencyResponse>>("/api/agencies");
            var agency = agencies?.FirstOrDefault(a => a.Id == agencyId);

            // Si la agencia no existe, devolvemos el precio base (sin descuento)
            if (agency == null) return product.BasePrice;

            // Lógica B2B: El Admin Global (Id -1) tiene 20% de descuento. El resto 10%.
            decimal discountPercentage = agency.Id == -1 ? 0.20m : 0.10m;

            return product.BasePrice - (product.BasePrice * discountPercentage);
        }
    }
}
