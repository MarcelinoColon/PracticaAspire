using MassTransit;
using TravelAgenciesB2B.Messages;

namespace Agency.Api.Application.Consumers
{
    public class ProductCreatedConsumer : IConsumer<ProductCreatedEvent>
    {
        private readonly ILogger<ProductCreatedConsumer> _logger;

        public ProductCreatedConsumer(ILogger<ProductCreatedConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ProductCreatedEvent> context)
        {
            var message = context.Message;

            // Imprimimos en los logs de Aspire para confirmar que la comunicación asíncrona funciona
            _logger.LogInformation("✅ [EVENTO RABBITMQ RECIBIDO en Agency.Api] -> Nuevo paquete disponible: {Name} (Categoría: {Category}) a ${Price}",
                message.Name, message.Category, message.BasePrice);

            // En un futuro, aquí insertaríamos en SQL una tabla de Notificaciones para las agencias
            await Task.CompletedTask;
        }
    }
}
