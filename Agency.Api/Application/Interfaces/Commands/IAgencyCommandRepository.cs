using Agency.Api.Application.DTOs;

namespace Agency.Api.Application.Interfaces.Commands
{
    public interface IAgencyCommandRepository
    {
        Task<AgencyResponse> CreateAgencyAsync(CreateAgencyRequest request);
    }
}
