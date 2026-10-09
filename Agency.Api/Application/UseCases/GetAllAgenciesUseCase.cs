using Agency.Api.Application.DTOs;
using Agency.Api.Application.Interfaces.Queries;

namespace Agency.Api.Application.UseCases
{
    public class GetAllAgenciesUseCase
    {
        private readonly IAgencyQueryRepository _queryRepository;

        public GetAllAgenciesUseCase(IAgencyQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task<IEnumerable<AgencyResponse>> ExecuteAsync()
        {
            // Lógica extra si fuera necesaria (ej. filtrado, logs, etc.)
            return await _queryRepository.GetAllAgenciesAsync();
        }
    }
}
