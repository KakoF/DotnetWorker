using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Service.Services
{
    public class AdviceService : IAdviceService
    {
        private readonly IAdviceRepository _adviceRepository;

        public AdviceService(IAdviceRepository adviceRepository)
        {
            _adviceRepository = adviceRepository;
        }

        public async Task<SlipModel> GetAsync(int id)
        {
            return await _adviceRepository.GetAsync(id);
        }

        public async Task SaveAsync(AdviceModel advice)
        {
            await _adviceRepository.SaveAsync(advice.Slip);
        }
    }
}
