using Catalog.Api.Core;
using Catalog.Api.Core.Entities;
using Catalog.Api.Core.Ports;

namespace Catalog.Api.Application.UseCases
{
    public record CreateProductRequest(string Name, string Category, decimal BasePrice);

    public class CreateProductUseCase
    {
        private readonly IProductRepository _repository;

        public CreateProductUseCase(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<TravelProduct>> ExecuteAsync(CreateProductRequest request)
        {
            // Validaciones de negocio usando el patrón Result
            if (string.IsNullOrWhiteSpace(request.Name))
                return Result<TravelProduct>.Failure("El nombre del producto es obligatorio.");

            if (request.BasePrice <= 0)
                return Result<TravelProduct>.Failure("El precio base debe ser mayor a cero.");

            var newProduct = new TravelProduct
            {
                Name = request.Name,
                Category = request.Category,
                BasePrice = request.BasePrice
            };

            await _repository.CreateAsync(newProduct);

            return Result<TravelProduct>.Success(newProduct);
        }
    }
}
