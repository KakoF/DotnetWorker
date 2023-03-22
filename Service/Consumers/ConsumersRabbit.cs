using Domain.Interfaces.Consumers;

namespace Service.Consumers
{
    public class ConsumersRabbit : IConsumersRabbit
    {

        private readonly IEnumerable<IConsumer> _consumers;

        public ConsumersRabbit(IEnumerable<IConsumer> consumers)
        {
            _consumers = consumers;
        }

        public Task Inicialize(CancellationToken cancellationToken)
        {
            foreach (var consumer in _consumers)
            {
                consumer.StartConsumer();
            }

            return Task.CompletedTask;
        }
    }
}