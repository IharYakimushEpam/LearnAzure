using Azure.Storage.Queues;

namespace Storage.Sample;

public class QueueSample
{
    public static void Run()
    {
        QueueClient client = new QueueClient("UseDevelopmentStorage=true", "qwe");

        client.CreateIfNotExists();
    }
}