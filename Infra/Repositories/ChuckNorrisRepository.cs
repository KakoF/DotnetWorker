using Dapper;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Infra.Interfaces;
using Newtonsoft.Json;

namespace Infra.Repositories
{
    public class ChuckNorrisRepository : IChuckNorrisRepository
    {
        private readonly IDbConnector _connector;
        public ChuckNorrisRepository(IDbConnector connector)
        {
            _connector = connector;
        }
        public async Task SaveAsync(ChuckNorrisModel data)
        {
            await _connector.dbConnection.QuerySingleAsync<ChuckNorrisModel>("INSERT INTO ChuckNorris(id, categories, createdAt, iconUrl, updatedAt, url, value) VALUES (@id, @categories, @createdAt, @iconUrl, @updatedAt, @url, @value) RETURNING  id, createdAt, iconUrl, updatedAt, url, value;", new
            {
                id = data.Id,
                categories = JsonConvert.SerializeObject(data.Categories),
                createdAt = data.CreatedAt,
                iconUrl = data.IconUrl,
                updatedAt = data.UpdatedAt,
                url = data.Url,
                value = data.Value
            });

        }

        public async Task<ChuckNorrisModel> GetAsync(string id)
        {
            return await _connector.dbConnection.QueryFirstOrDefaultAsync<ChuckNorrisModel>("Select id, categories, createdAt, iconUrl, updatedAt, url, value from ChuckNorris where id = @id", new { id });
        }
    }
}