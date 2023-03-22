
using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface IChuckNorrisRepository
    {
        Task<ChuckNorrisModel> GetAsync(string id);
        Task SaveAsync(ChuckNorrisModel data);
    }
}
