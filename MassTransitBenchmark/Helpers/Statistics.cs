namespace MassTransitDemo.Helpers;

public class Statistics
{
    private readonly object _lockObj = new();

    private int _messagesReceived;
    private Timer _timer;
    private readonly ILogger<Statistics> _logger;
    
    public Statistics(ILogger<Statistics> logger)
    {
        _logger = logger;
        _timer = new Timer(UpdateMessagesPerSecond, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
    }

    public void IncreaseCount()
    {
        lock (_lockObj)
        {
            _messagesReceived++;
        }
    }
    
    private void UpdateMessagesPerSecond(object? state)
    {
        lock (_lockObj)
        {
            double messagesPerSecond = _messagesReceived;
            _messagesReceived = 0;
            _logger.LogInformation($"Messages received per second: {messagesPerSecond}");
        }
    }
}