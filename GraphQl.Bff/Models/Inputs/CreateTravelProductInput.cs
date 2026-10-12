namespace GraphQl.Bff.Models.Inputs
{
    public record CreateTravelProductInput(
        string Name,
        string Category,
        decimal BasePrice,
        List<ItineraryDayInput>? Itineraries,
        List<string>? Amenities
    );
}
