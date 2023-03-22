using RabbitMQ.Client;

namespace ServiceBus
{
    public class QueueConnection: IDisposable
    {
        public IConnection Instance;
        private readonly object syncRoot = new object();

        public IModel _channel { get; protected set; }

        public QueueSettings _settings { get; set; }

        public QueueConnection(QueueSettings settings)
        {
            _settings = settings;
            if(Instance == null)
            {
                lock (syncRoot)
                {
                    Instance = new ConnectionFactory()
                    {
                        HostName = settings.HostName,
                        UserName = settings.UserName,
                        Password = settings.Password,
                        DispatchConsumersAsync = true,
                    }.CreateConnection();
                }
            }
        }

        public IModel CreateChannel()
        {
            _channel = Instance.CreateModel();
            _channel.ExchangeDeclare(exchange: _settings.Exchange, type: _settings.ExchangeType, durable: true, autoDelete: false);

            var args = new Dictionary<string, object>();
            /*{
                {"x-queue-type", _settings.QueueType},
            };*/

            _channel.QueueDeclare(_settings.Queue, durable: true, autoDelete: false, exclusive: false, arguments: args);

            _channel.QueueBind(queue: _settings.Queue, exchange: _settings.Exchange, routingKey: _settings.RoutingKey);
            _channel.BasicQos(prefetchSize: 0, prefetchCount: _settings.MaxMessageConsumer, global: false);

            return _channel;
        }

        public void Dispose()
        {
            if(Instance != null)
            {
                _channel?.Dispose();
                Instance?.Dispose();
            }
        }
    }
}
