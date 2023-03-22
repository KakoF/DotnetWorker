using Dapper;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Infra.Interfaces;

namespace Infra.Repositories
{
    public class AdviceRepository : IAdviceRepository
    {
        private readonly IDbConnector _connector;
        public AdviceRepository(IDbConnector connector)
        {
            _connector = connector;
        }
        public async Task SaveAsync(SlipModel data)
        {
            await _connector.dbConnection.QuerySingleAsync<SlipModel>("INSERT INTO Slip(id, advice) VALUES (@id, @advice) RETURNING  id, advice ;", new { id = data.Id, advice = data.Advice});
        }

        public async Task<SlipModel> GetAsync(int id)
        {
            return await _connector.dbConnection.QueryFirstOrDefaultAsync<SlipModel>("Select id, advice from Slip where id = @id", new { id });
        }
    }
}