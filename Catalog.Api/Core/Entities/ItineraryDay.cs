namespace Catalog.Api.Core.Entities
{
    public class ItineraryDay
    {
        public int DayNumber { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
