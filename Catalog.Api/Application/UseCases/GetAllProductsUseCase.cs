using Catalog.Api.Core;
using Catalog.Api.Core.Entities;
using Catalog.Api.Core.Ports;

namespace Catalog.Api.Application.UseCases
{
    public class GetAllProductsUseCase
    {
        private readonly IProductRepository _repository;

        public GetAllProductsUseCase(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<TravelProduct>>> ExecuteAsync()
        {
            var products = await _repository.GetAllAsync();

            // El GET rara vez falla, pero mantenemos la consistencia del patrón Result
            return Result<IEnumerable<TravelProduct>>.Success(products);
        }
    }
}
