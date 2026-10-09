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
                IsActive = true
            };

            _db.Agencies.Add(newAgency);
            await _db.SaveChangesAsync();

            return new AgencyResponse(newAgency.Id, newAgency.Name, newAgency.ContactEmail, newAgency.IsActive);
        }
    }
}
