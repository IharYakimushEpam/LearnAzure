using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Storage.Sample;

public class QueueProducerSample : BackgroundService
{
    public IConfiguration Configuration { get; }

    public QueueProducerSample(IConfiguration configuration)
    {
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();

        QueueClient client = new QueueClient(this.Configuration["AzureStorageTest"], "qwe");

        var response = await client.CreateIfNotExistsAsync(cancellationToken: stoppingToken);

        Console.WriteLine(response);

        while (!stoppingToken.IsCancellationRequested)
        {
            var send = await client.SendMessageAsync(
                DateTime.UtcNow.ToString("s"),
                TimeSpan.FromSeconds(20),
                TimeSpan.FromHours(1),
                stoppingToken);

            Console.WriteLine(send);

            await Task.Delay(3000, stoppingToken);
        }
    }
}