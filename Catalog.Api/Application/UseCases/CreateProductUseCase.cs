using Catalog.Api.Core;
using Catalog.Api.Core.Entities;
using Catalog.Api.Core.Ports;
using MassTransit;
using MassTransit.Transports;
using TravelAgenciesB2B.Messages;

namespace Catalog.Api.Application.UseCases
{
    public record CreateProductRequest(
    string Name,
    string Category,
    decimal BasePrice,
    List<ItineraryDay>? Itineraries = null,
    List<string>? Amenities = null);

    public class CreateProductUseCase
    {
        private readonly IProductRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateProductUseCase(IProductRepository repository, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _publishEndpoint = publishEndpoint;
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
                BasePrice = request.BasePrice,

                Itineraries = request.Itineraries ?? new List<ItineraryDay>(),
                Amenities = request.Amenities ?? new List<string>()
            };

            await _repository.CreateAsync(newProduct);

            await _publishEndpoint.Publish(new ProductCreatedEvent(
            newProduct.Id!,
            newProduct.Name,
            newProduct.Category,
            newProduct.BasePrice));

            return Result<TravelProduct>.Success(newProduct);
        }
    }
}
