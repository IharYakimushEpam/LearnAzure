using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Storage.Sample.Blob;

Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(builder => builder.AddUserSecrets(typeof(Program).Assembly))
    .ConfigureServices(services =>
    {
        // https://docs.microsoft.com/en-us/rest/api/storageservices/operations-on-messages
        //services.AddHostedService<QueueProducerSample<Instance1>>();
        //services.AddHostedService<QueueProducerSample<Instance2>>();
        //services.AddHostedService<QueueConsumerSample<Instance1>>();

        services.AddHostedService<BlobProducerSample>();

    }).Build().Run();