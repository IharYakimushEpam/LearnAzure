using Azure;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Storage.Sample;

public class QueueConsumerSample<T> : BackgroundService
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

        await client.CreateIfNotExistsAsync(cancellationToken: stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            Response<QueueMessage>? msg = await client.ReceiveMessageAsync(null, stoppingToken);
            if (msg?.Value != null)
            {
                Console.WriteLine($"{typeof(T).Name} receive {msg.Value.MessageId} {msg.Value.MessageText}");
            }
        }
    }
}