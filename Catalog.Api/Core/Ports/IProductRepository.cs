using Catalog.Api.Core.Entities;

namespace Catalog.Api.Core.Ports
{
    public interface IProductRepository
    {
        Task<IEnumerable<TravelProduct>> GetAllAsync();
        Task<TravelProduct?> GetByIdAsync(string id);
        Task CreateAsync(TravelProduct product);
    }
}
