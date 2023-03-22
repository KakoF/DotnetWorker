using Domain.Interfaces.Consumers;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using DotnetWorker;
using Infra.DataConnector;
using Infra.Interfaces;
using Infra.Repositories;
using Service.Consumers;
using Service.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostBuiler, services)=>
    {
        services.AddHostedService<Worker>();
        services.AddScoped<IDbConnector>(db => new PostgreeConnector(hostBuiler.Configuration["ConnectionStrings:postgree"]));
        services.AddSingleton<IConsumersRabbit, ConsumersRabbit>();
        services.AddSingleton<IConsumer, AdviceConsumer>();
        services.AddSingleton<IConsumer, ChuckNorrisConsumer>();
        
        services.AddScoped<IAdviceService, AdviceService>();
        services.AddScoped<IChuckNorrisService, ChuckNorrisService>();

        services.AddScoped<IAdviceRepository, AdviceRepository>();
        services.AddScoped<IChuckNorrisRepository, ChuckNorrisRepository>();

    })
    .Build();

await host.RunAsync();
