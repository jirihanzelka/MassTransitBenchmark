namespace MassTransitBenchmark.Contracts;

// public record Message(string Text);
public class Message
{
    public string Text { get; }
    
    public Message(string text)
    {
        Text = text;
    }
}