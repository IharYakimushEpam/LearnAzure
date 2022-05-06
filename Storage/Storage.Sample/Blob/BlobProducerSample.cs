using System.Diagnostics;

using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Storage.Sample.Blob;

public class BlobProducerSample : BackgroundService
{
    public BlobProducerSample(IConfiguration configuration)
    {
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public IConfiguration Configuration { get; }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();

        // Get a reference to a container named "sample-container" and then create it
        BlobContainerClient container = new BlobContainerClient(this.Configuration["AzureStorageTest"], "test-container");
        await container.CreateIfNotExistsAsync(PublicAccessType.BlobContainer, cancellationToken: stoppingToken);

        using (new Activity("Blob").Start())
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using Activity upload = new Activity("Upload").Start();

                string path = upload.ParentSpanId.ToHexString() + "/" + upload.SpanId.ToHexString();

                // Get a reference to a blob named "sample-file" in a container named "sample-container"
                BlobClient blob = container.GetBlobClient(path);

                var result = await blob.UploadAsync(new BinaryData(Guid.NewGuid().ToString("N")),
                    new BlobUploadOptions
                    {
                        Metadata = new Dictionary<string, string> { { "k1", "v1" } }
                    }, stoppingToken);

                Console.WriteLine($"{result.Value.BlobSequenceNumber} {result.Value.LastModified}");

                await Task.Delay(3000, stoppingToken);
            }
        }
    }
}