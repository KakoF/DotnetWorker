
namespace Domain.Interfaces.Consumers
{
    public interface IConsumersRabbit
    {
        Task Inicialize(CancellationToken cancellationToken);
    }
}
