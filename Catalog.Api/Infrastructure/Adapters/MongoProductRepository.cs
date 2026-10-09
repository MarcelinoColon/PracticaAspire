using Catalog.Api.Core.Entities;
using Catalog.Api.Core.Ports;
using MongoDB.Driver;

namespace Catalog.Api.Infrastructure.Adapters
{
    public class MongoProductRepository : IProductRepository
    {
        private readonly IMongoCollection<TravelProduct> _products;

        public MongoProductRepository(IMongoClient mongoClient)
        {
            // Nos conectamos a la BD y a la colección (se crean solas si no existen)
            var database = mongoClient.GetDatabase("CatalogDb");
            _products = database.GetCollection<TravelProduct>("Products");
        }

        public async Task<IEnumerable<TravelProduct>> GetAllAsync()
        {
            return await _products.Find(_ => true).ToListAsync();
        }

        public async Task<TravelProduct?> GetByIdAsync(string id)
        {
            return await _products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(TravelProduct product)
        {
            await _products.InsertOneAsync(product);
        }
    }
}
