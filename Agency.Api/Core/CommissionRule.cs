namespace Agency.Api.Core
{
    public class CommissionRule
    {
        public int Id { get; set; }
        public int AgencyId { get; set; }
        public string DestinationCategory { get; set; } = string.Empty; // ej: "Vuelos", "Hoteles", "Paquetes"
        public decimal Percentage { get; set; } // ej: 0.15 para 15%

        // Propiedad de navegación para EF Core
        public Agency? Agency { get; set; }
    }
}
