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

            const string sql = """
            SELECT Id, Name, ContactEmail, IsActive 
            FROM Agencies
            """;

            return await connection.QueryAsync<AgencyResponse>(sql);
        }
    }
}
