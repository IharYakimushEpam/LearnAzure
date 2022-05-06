using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Storage.Sample;

public class QueueConsumerSample : BackgroundService
{
    public IConfiguration Configuration { get; }

    public QueueConsumerSample(IConfiguration configuration)
    {
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();

        QueueClient client = new QueueClient(this.Configuration["AzureStorageTest"], "qwe");

        var response = await client.CreateIfNotExistsAsync(cancellationToken: stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            var msg = await client.ReceiveMessageAsync(null, stoppingToken);

            Console.WriteLine(msg.Value);
        }
    }
}