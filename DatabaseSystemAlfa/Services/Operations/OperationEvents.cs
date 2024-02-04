namespace DatabaseSystemAlfa.Services.Operations;

public struct OperationEvents
{
    public event Func<string, string, string>? OnInputStringRequestedEvent;
    public event Func<string, string, string>? OnOptionalAndSecretInputStringRequestedEvent;
    public event Func<string, string, int>? OnInputIntRequestedEvent;
    
    public OperationEvents(
        Func<string, string, string>? onInputStringRequestedEvent = null,
        Func<string, string, string>? onOptionalAndSecretInputStringRequestedEvent = null,
        Func<string, string, int>? onInputIntRequestedEvent = null)
    {
        OnInputStringRequestedEvent += onInputStringRequestedEvent;
        OnOptionalAndSecretInputStringRequestedEvent += onOptionalAndSecretInputStringRequestedEvent;
        OnInputIntRequestedEvent += onInputIntRequestedEvent;
    }

    public string RequestOnInputString(string prompt, string arg)
    {
        if (OnInputStringRequestedEvent != null)
            return OnInputStringRequestedEvent.Invoke(prompt, arg);

        throw new InvalidOperationException($"{nameof(OnInputStringRequestedEvent)} event is not subscribed.");
    }

    public string RequestOnOptionalAndSecretInputString(string prompt, string arg)
    {
        if (OnOptionalAndSecretInputStringRequestedEvent != null)
            return OnOptionalAndSecretInputStringRequestedEvent.Invoke(prompt, arg);

        throw new InvalidOperationException($"{nameof(OnOptionalAndSecretInputStringRequestedEvent)} event is not subscribed.");
    }

    public int RequestOnInputInt(string prompt, string arg)
    {
        if (OnInputIntRequestedEvent != null)
            return OnInputIntRequestedEvent.Invoke(prompt, arg);

        throw new InvalidOperationException($"{nameof(OnInputIntRequestedEvent)} event is not subscribed.");
    }
}