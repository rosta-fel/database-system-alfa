namespace DatabaseSystemAlfa.Libraries.Tools.Console.Message;

public abstract class MessageBase(string message) : IDisplayHandler
{
    public static event EventHandler<string>? OnDisplayRequestedEvent;

    public void Display()
    {
        RequestOnDisplay(message);
    }

    private void RequestOnDisplay(string messageInput)
    {
        OnDisplayRequestedEvent?.Invoke(this, messageInput);
    }
    
    public override string ToString() => message;
}