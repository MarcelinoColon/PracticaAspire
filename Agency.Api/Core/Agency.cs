namespace Agency.Api.Core
{
    public class Agency
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        // Relación: Una agencia puede tener múltiples reglas de comisiones
        public List<CommissionRule> CommissionRules { get; set; } = new();

        // Método de fábrica para crear la agencia "Global" (ID 0) 
        // de forma segura desde código.
        public static Agency CreateGlobalAdminAgency()
        {
            return new Agency
            {
                Id = -1,
                Name = "Administración Global",
                ContactEmail = "admin@plataforma.com",
                IsActive = true
            };
        }
    }}
