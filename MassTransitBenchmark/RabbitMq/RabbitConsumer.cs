using System.Text;
using MassTransitBenchmark.Contracts;
using MassTransitBenchmark.Helpers;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MassTransitBenchmark.RabbitMq;

public class RabbitConsumer : BackgroundService
{
    private readonly ILogger<RabbitConsumer> _logger;
    private readonly Statistics _statistics;

    public RabbitConsumer(ILogger<RabbitConsumer> logger, Statistics statistics)
    {
        _logger = logger;
        _statistics = statistics;
    }

    private async Task Consume(CancellationToken stoppingToken)
    {
        // common code
        var factory = new ConnectionFactory {HostName = "localhost", UserName = "guest", Password = "guest"};
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "hello",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);


        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var json = Encoding.UTF8.GetString(body);
            var message = JsonConvert.DeserializeObject<Message>(json);
            _statistics.IncreaseCount();
        };
        channel.BasicConsume(queue: "hello",
            autoAck: true,
            consumer: consumer);

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Consume(stoppingToken);
    }
}