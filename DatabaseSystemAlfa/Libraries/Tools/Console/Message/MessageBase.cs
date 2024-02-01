namespace DatabaseSystemAlfa.Libraries.Tools.Console.Message;

public abstract class MessageBase(string message) : IDisplayHandler
{
    public static event EventHandler<string>? DisplayRequestedEvent;

    public void Display()
    {
        OnDisplayRequested(message);
    }

    private void OnDisplayRequested(string messageInput)
    {
        DisplayRequestedEvent?.Invoke(this, messageInput);
    }
    
    public override string ToString() => message;
}