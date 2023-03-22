using Domain.Interfaces.Consumers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServiceBus;
using System.Text;

namespace Service.Abstracts
{
    public abstract class AbstractBaseConsumer : IConsumer, IDisposable
    {
        protected readonly ILogger<AbstractBaseConsumer> _logger;
        protected readonly List<ConsumerChannel> _consumersChannels;
        protected readonly QueueConnection _queueConnection;
        protected readonly QueueSettings _queueSettings;
        protected readonly string _consumer;

        protected AbstractBaseConsumer(ILogger<AbstractBaseConsumer> logger, IConfiguration configuration,  string consumer)
        {
            _consumersChannels = new List<ConsumerChannel>();
            _queueSettings = new QueueSettings(consumer, configuration);
            _queueConnection = new QueueConnection(_queueSettings);
            _logger = logger;
            _consumer = consumer;
        }
        protected T GetMessage<T>(BasicDeliverEventArgs data)
        {
            string message = Encoding.UTF8.GetString(data.Body.ToArray());
            try
            {
                T model = JsonConvert.DeserializeObject<T>(message);
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{_consumer}, não foi possível ler a mensagem");
            }

            return default;
        }

        public void StartConsumer()
        {
            _logger.LogWarning($"Iniciando {_consumer}, total de {_queueSettings.QuantityConsumers}");

            var channel = _queueConnection.CreateChannel();
            for (int i = 0; i < _queueSettings.QuantityConsumers; i++)
            {
                _logger.LogWarning($"Iniciando consumo das mensagens do serviço {_consumer}. Consumidor: {i}");
                AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(channel);
                consumer.Received += Consumer_Received;
                channel.BasicConsume(_queueSettings.Queue, true, consumer);
            }
            _consumersChannels.Add(new ConsumerChannel(_consumer, channel.ChannelNumber, channel));
        }

        public abstract Task Consumer_Received(object sender, BasicDeliverEventArgs e);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposing) 
            { 
                if(_consumersChannels != null)
                {
                    foreach (var channel in _consumersChannels)
                    {
                        channel.Model?.Dispose();
                    }
                }
                _queueConnection?.Dispose();
            }
        }
    }
}
