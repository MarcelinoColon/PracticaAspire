using Agency.Api.Core;
using Microsoft.EntityFrameworkCore;

namespace Agency.Api.Infrastructure
{
    public class AgencyDbContext : DbContext
    {
        public AgencyDbContext(DbContextOptions<AgencyDbContext> options) : base(options) { }

        // El alias 'Core.Agency' evita conflictos con el nombre del proyecto
        public DbSet<Core.Agency> Agencies => Set<Core.Agency>();
        public DbSet<CommissionRule> CommissionRules => Set<CommissionRule>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Sembrar el registro de Administración Global con ID 0
            modelBuilder.Entity<Core.Agency>().HasData(
                Core.Agency.CreateGlobalAdminAgency()
            );

            // Precisión para los porcentajes (ej: 0.1500)
            modelBuilder.Entity<CommissionRule>()
                .Property(c => c.Percentage)
                .HasPrecision(5, 4);
        }
    }
}

