using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Storage.Sample;

Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(builder => builder.AddUserSecrets(typeof(Program).Assembly))
    .ConfigureServices(services =>
    {
        // create user secret with AzureStorageTest key
        services.AddHostedService<QueueProducerSample<Instance1>>();
        services.AddHostedService<QueueConsumerSample<Instance1>>();
    }).Build().Run();