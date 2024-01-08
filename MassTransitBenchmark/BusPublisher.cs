using MassTransit;
using MassTransitDemo.Contracts;

namespace MassTransitDemo;

public class BusPublisher : BackgroundService
{
    private readonly IBus _bus;

    public BusPublisher(IBus bus)
    {
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        int i = 0;
        
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Yield();
            await _bus.Publish(new Message($"{i*1000000000}"));
            i++;
        }
    }
}