using Domain.Interfaces.Consumers;

namespace DotnetWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConsumersRabbit _consumers;

        public Worker(ILogger<Worker> logger, IConsumersRabbit consumers)
        {
            _logger = logger;
            _consumers = consumers;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _consumers.Inicialize(stoppingToken);
            /*while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }*/
        }
    }
}