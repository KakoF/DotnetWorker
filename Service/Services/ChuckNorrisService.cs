using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Service.Services
{
    public class ChuckNorrisService : IChuckNorrisService
    {
        private readonly IChuckNorrisRepository _chuckNorrisRepository;
        public ChuckNorrisService(IChuckNorrisRepository chuckNorrisRepository)
        {
            _chuckNorrisRepository = chuckNorrisRepository;
        }

        public async Task<ChuckNorrisModel> GetAsync(string id)
        {
            return await _chuckNorrisRepository.GetAsync(id);
        }

        public async Task SaveAsync(ChuckNorrisModel chuckNorris)
        {
           await _chuckNorrisRepository.SaveAsync(chuckNorris);
        }
    }
}
