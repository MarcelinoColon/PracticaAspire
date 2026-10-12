using Agency.Api.Application.DTOs;
using Agency.Api.Application.Interfaces.Queries;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Agency.Api.Infrastructure.Repositories.Queries
{
    public class AgencyQueryRepository : IAgencyQueryRepository
    {
        private readonly AgencyDbContext _db;

        public AgencyQueryRepository(AgencyDbContext db) => _db = db;

        public async Task<IEnumerable<AgencyResponse>> GetAllAgenciesAsync()
        {
            var connection = _db.Database.GetDbConnection();

            // Hacemos JOIN con las reglas
            const string sql = """
            SELECT 
                a.Id, a.Name, a.ContactEmail, a.IsActive,
                c.Id, c.DestinationCategory, c.Percentage
            FROM Agencies a
            LEFT JOIN CommissionRules c ON a.Id = c.AgencyId
            """;

            var agencyDictionary = new Dictionary<int, AgencyResponse>();

            // Le decimos a Dapper que mapee 2 objetos (AgencyResponse y CommissionRuleDto)
            await connection.QueryAsync<AgencyResponse, CommissionRuleDto, AgencyResponse>(
                sql,
                (agency, rule) =>
                {
                    // Si la agencia no está en el diccionario, la agregamos
                    if (!agencyDictionary.TryGetValue(agency.Id, out var currentAgency))
                    {
                        currentAgency = agency;
                        agencyDictionary.Add(currentAgency.Id, currentAgency);
                    }

                    // Si hay una regla (no es nula por el LEFT JOIN), la agregamos a la lista
                    if (rule != null && !string.IsNullOrEmpty(rule.DestinationCategory))
                    {
                        currentAgency.CommissionRules.Add(rule);
                    }

                    return currentAgency;
                },
                splitOn: "Id" // Le dice a Dapper en qué columna empieza la tabla hija (CommissionRules)
            );

            return agencyDictionary.Values;
        }
    }
}