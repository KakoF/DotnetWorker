using Domain.Models;

namespace Domain.Interfaces.Services
{
    public interface IChuckNorrisService
    {
        Task<ChuckNorrisModel> GetAsync(string id);
        Task SaveAsync(ChuckNorrisModel chuckNorris);
    }
}
