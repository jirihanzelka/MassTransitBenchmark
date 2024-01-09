
using System.Text;
using MassTransitBenchmark.Contracts;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client;

namespace MassTransitBenchmark.RabbitMq;

public class RabbitPublisher : BackgroundService
{
    public RabbitPublisher()
    {
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        int i = 0;
        
        // common code
        var factory = new ConnectionFactory {HostName = "localhost", UserName = "guest", Password = "guest"};
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "hello",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Yield();
            
            var message = new Message((i * 1000000000).ToString());
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: string.Empty,
                routingKey: "hello",
                basicProperties: null,
                body: body);
        }
    }
}