using Agency.Api.Application.DTOs;

namespace Agency.Api.Application.Interfaces.Queries
{
    public interface IAgencyQueryRepository
    {
        Task<IEnumerable<AgencyResponse>> GetAllAgenciesAsync();
    }
}
