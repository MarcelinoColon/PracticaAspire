using Agency.Api.Application.DTOs;
using Agency.Api.Application.Interfaces.Commands;

namespace Agency.Api.Application.UseCases
{
    public class CreateAgencyUseCase
    {
        private readonly IAgencyCommandRepository _commandRepository;

        // Inyectamos el repositorio
        public CreateAgencyUseCase(IAgencyCommandRepository commandRepository)
        {
            _commandRepository = commandRepository;
        }

        public async Task<AgencyResponse> ExecuteAsync(CreateAgencyRequest request)
        {
            // Aquí irían las validaciones de negocio antes de tocar la base de datos.
            // Por ejemplo: if (string.IsNullOrWhiteSpace(request.Name)) throw new Exception(...);

            return await _commandRepository.CreateAgencyAsync(request);
        }
    }
}
