using Microsoft.Extensions.Configuration;

namespace ServiceBus
{
    public class QueueSettings
    {
        public readonly string HostName;
        public readonly string VirtualHost;
        public readonly string UserName;
        public readonly string Password;
        public readonly ushort MaxMessageConsumer;
        public readonly ushort QuantityConsumers;
        public readonly string Exchange;
        public readonly string Queue;
        public readonly string RoutingKey;
        public readonly string QueueType;
        public readonly string ExchangeType;

        public QueueSettings(string hostName, string virtualHost, string userName, string password, ushort maxMessageConsumer, ushort quantityConsumers, string exchange, string queue, string routingKey, string queueType, string exchangeType)
        {
            HostName = hostName;
            VirtualHost = virtualHost;
            UserName = userName;
            Password = password;
            MaxMessageConsumer = maxMessageConsumer;
            QuantityConsumers = quantityConsumers;
            Exchange = exchange;
            Queue = queue;
            RoutingKey = routingKey;
            QueueType = queueType;
            ExchangeType = exchangeType;
        }

        public QueueSettings(string name, IConfiguration configuration)
        {
            var conn = configuration.GetSection("Rabbit:connection").Value;
            var parameters = conn.Split(";");

            foreach (var param in parameters)
            {
                if (string.IsNullOrEmpty(param)) continue;

                var value = param.Split("=");
                if (value[0].ToUpper().Trim()== "HOST") HostName = value[1].Split(",")[0].Trim();
                if (value[0].ToUpper().Trim()== "VIRTUALHOST") VirtualHost = value[1].Trim();
                if (value[0].ToUpper().Trim()== "USERNAME") UserName = value[1].Trim();
                if (value[0].ToUpper().Trim()== "PASSWORD") Password = value[1].Trim();
            }
            MaxMessageConsumer = (ushort)Convert.ToUInt16(configuration.GetSection($"Rabbit:{name}:MaxMessageConsumer").Value);
            QuantityConsumers = (ushort)Convert.ToUInt16(configuration.GetSection($"Rabbit:{name}:QuantityConsumers").Value);
            Exchange = configuration.GetSection($"Rabbit:{name}:Exchange").Value;
            Queue = configuration.GetSection($"Rabbit:{name}:Queue").Value;
            RoutingKey = configuration.GetSection($"Rabbit:{name}:RoutingKey").Value;
            QueueType = configuration.GetSection($"Rabbit:{name}:QueueType").Value;
            ExchangeType = configuration.GetSection($"Rabbit:{name}:ExchangeType").Value;
        }
    }
}
