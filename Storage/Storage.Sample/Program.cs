using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Storage.Sample;

Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(builder => builder.AddUserSecrets(typeof(Program).Assembly))
    .ConfigureServices(services =>
    {
        // create user secret with AzureStorageTest key
        services.AddHostedService<QueueProducerSample>();
        services.AddHostedService<QueueConsumerSample>();
    }).Build().Run();