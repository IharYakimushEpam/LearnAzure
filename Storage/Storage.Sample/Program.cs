using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Storage.Sample;

Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(builder => builder.AddUserSecrets(typeof(Program).Assembly))
    .ConfigureServices(services =>
    {
        services.AddHostedService<QueueSample>();
    }).Build().Run();