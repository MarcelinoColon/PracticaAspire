using GraphQl.Bff.Models;
using HotChocolate;
using HotChocolate.Types;

namespace GraphQl.Bff.GraphQl
{
    [ExtendObjectType(typeof(TravelProductResponse))]
    public class TravelProductResolvers
    {
        public async Task<decimal> GetB2bPriceAsync(
            [Parent] TravelProductResponse product,
            int agencyId,
            [Service] IHttpClientFactory clientFactory)
        {
            var client = clientFactory.CreateClient("agency-api");

            var agencies = await client.GetFromJsonAsync<IEnumerable<AgencyResponse>>("/api/agencies");
            var agency = agencies?.FirstOrDefault(a => a.Id == agencyId);

            // Si la agencia no existe o no tiene reglas, paga precio base
            if (agency?.CommissionRules == null || !agency.CommissionRules.Any())
                return product.BasePrice;

            // MAGIA B2B: Compara la categoría del producto (Mongo) con la regla (SQL)
            var rule = agency.CommissionRules.FirstOrDefault(r => r.DestinationCategory == product.Category);

            // Si no tiene descuento para esta categoría en específico, paga precio base
            if (rule == null)
                return product.BasePrice;

            // Calculamos el precio con el porcentaje dinámico
            return product.BasePrice - (product.BasePrice * rule.Percentage);
        }
    }
}