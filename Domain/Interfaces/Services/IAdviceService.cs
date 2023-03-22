using Domain.Models;
namespace Domain.Interfaces.Services
{
    public interface IAdviceService
    {
        Task<SlipModel> GetAsync(int id);
        Task SaveAsync(AdviceModel advice);
    }
}
