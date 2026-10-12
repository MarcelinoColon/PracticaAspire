namespace TravelAgenciesB2B.Messages
{
    public record ProductCreatedEvent(string ProductId, string Name, string Category, decimal BasePrice);
}
