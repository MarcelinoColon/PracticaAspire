using Agency.Api.Application.DTOs;
using Agency.Api.Application.Interfaces.Commands;

namespace Agency.Api.Infrastructure.Repositories.Commands
{
    public class AgencyCommandRepository : IAgencyCommandRepository
    {
        private readonly AgencyDbContext _db;

        public AgencyCommandRepository(AgencyDbContext db) => _db = db;

        public async Task<AgencyResponse> CreateAgencyAsync(CreateAgencyRequest request)
        {
            var newAgency = new Core.Agency
            {
                Name = request.Name,
                ContactEmail = request.ContactEmail,
                IsActive = true,
                // Mapeamos las reglas que vienen del JSON hacia la entidad Core
                CommissionRules = request.CommissionRules?.Select(r => new Core.CommissionRule
                {
                    DestinationCategory = r.DestinationCategory,
                    Percentage = r.Percentage
                }).ToList() ?? new List<Core.CommissionRule>()
            };

            _db.Agencies.Add(newAgency);
            await _db.SaveChangesAsync();

            // Retornamos el DTO actualizado
            return new AgencyResponse
            {
                Id = newAgency.Id,
                Name = newAgency.Name,
                ContactEmail = newAgency.ContactEmail,
                IsActive = newAgency.IsActive,
                CommissionRules = newAgency.CommissionRules.Select(r => new CommissionRuleDto(r.Id,r.DestinationCategory, r.Percentage)).ToList()
            };
        }
    }
}
