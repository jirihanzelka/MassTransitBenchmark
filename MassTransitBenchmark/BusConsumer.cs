using MassTransit;
using MassTransitBenchmark.Contracts;
using MassTransitBenchmark.Helpers;

namespace MassTransitBenchmark;

public class BusConsumer : IConsumer<Message>
{
    private readonly ILogger<BusConsumer> _logger;
    private readonly Statistics _statistics;

    public BusConsumer(ILogger<BusConsumer> logger, Statistics statistics)
    {
        _logger = logger;
        _statistics = statistics;
    }

    public Task Consume(ConsumeContext<Message> context)
    {
        var text = context.Message.Text;
        // _logger.LogInformation("Message {Text}", text);

        // count messages per second use timer
        _statistics.IncreaseCount();

        return Task.CompletedTask;
    }
}