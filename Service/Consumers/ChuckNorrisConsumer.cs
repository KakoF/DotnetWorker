using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Events;
using Service.Abstracts;

namespace Service.Consumers
{
    public class ChuckNorrisConsumer : AbstractBaseConsumer
    {
        const string CONSUMER = "ChuckNorrisConsumer";
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public ChuckNorrisConsumer(ILogger<ChuckNorrisConsumer> logger, IConfiguration configuration, IServiceScopeFactory serviceScopeFactory) : base(logger, configuration, CONSUMER)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public override async Task Consumer_Received(object sender, BasicDeliverEventArgs e)
        {

            var data = GetMessage<ChuckNorrisModel>(e);
            if (data != default)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<IChuckNorrisService>();
                    if (await service.GetAsync(data.Id) == null)
                        await service.SaveAsync(data);
                }
            }
        }
    }
}
