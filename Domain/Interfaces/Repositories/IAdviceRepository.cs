using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface IAdviceRepository
    {
        Task<SlipModel> GetAsync(int id);
        Task SaveAsync(SlipModel data);
    }
}
