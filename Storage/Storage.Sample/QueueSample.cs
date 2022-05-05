using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Storage.Sample;

public class QueueSample : BackgroundService
{
    public IConfiguration Configuration { get; }

    public QueueSample(IConfiguration configuration)
    {
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();

        QueueClient client = new QueueClient(this.Configuration["AzureStorageTest"], "qwe");

        var response = await client.CreateIfNotExistsAsync(cancellationToken: stoppingToken);

        Console.WriteLine(response);
    }
}