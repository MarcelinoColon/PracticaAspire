using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Api.Core.Entities
{
    public class TravelProduct
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }

        public List<ItineraryDay> Itineraries { get; set; } = new();
        public List<string> Amenities { get; set; } = new();
    }
}
