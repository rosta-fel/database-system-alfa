namespace DatabaseSystemAlfa.Services.Operations;

/// <summary>
///     Represents an event-driven mechanism to handle various input requests during operations.
/// </summary>
public class OperationEvents
{
    /// <summary>
    ///     Event handler for requesting input as a string.
    /// </summary>
    public event Func<string, string, string>? OnInputStringRequestedEvent;

    /// <summary>
    ///     Event handler for requesting optional and secret input as a string.
    /// </summary>
    public event Func<string, string, string>? OnOptionalAndSecretInputStringRequestedEvent;

    /// <summary>
    ///     Event handler for requesting input as an integer.
    /// </summary>
    public event Func<string, string, int>? OnInputIntRequestedEvent;

    /// <summary>
    ///     Initializes a new instance of the <see cref="OperationEvents" /> class.
    /// </summary>
    /// <param name="onInputStringRequestedEvent">Optional event handler for requesting input as a string.</param>
    /// <param name="onOptionalAndSecretInputStringRequestedEvent">
    ///     Optional event handler for requesting optional and secret
    ///     input as a string.
    /// </param>
    /// <param name="onInputIntRequestedEvent">Optional event handler for requesting input as an integer.</param>
    public OperationEvents(
        Func<string, string, string>? onInputStringRequestedEvent = null,
        Func<string, string, string>? onOptionalAndSecretInputStringRequestedEvent = null,
        Func<string, string, int>? onInputIntRequestedEvent = null)
    {
        OnInputStringRequestedEvent += onInputStringRequestedEvent;
        OnOptionalAndSecretInputStringRequestedEvent += onOptionalAndSecretInputStringRequestedEvent;
        OnInputIntRequestedEvent += onInputIntRequestedEvent;
    }

    /// <summary>
    ///     Requests input as a string by invoking the subscribed event handler.
    /// </summary>
    /// <param name="prompt">The prompt message for the input request.</param>
    /// <param name="arg">The argument related to the input request.</param>
    /// <returns>The user input as a string.</returns>
    public string RequestOnInputString(string prompt, string arg)
    {
        if (OnInputStringRequestedEvent != null)
            return OnInputStringRequestedEvent.Invoke(prompt, arg);

        throw new InvalidOperationException($"{nameof(OnInputStringRequestedEvent)} event is not subscribed.");
    }

    /// <summary>
    ///     Requests optional and secret input as a string by invoking the subscribed event handler.
    /// </summary>
    /// <param name="prompt">The prompt message for the input request.</param>
    /// <param name="arg">The argument related to the input request.</param>
    /// <returns>The user input as a string.</returns>
    public string RequestOnOptionalAndSecretInputString(string prompt, string arg)
    {
        if (OnOptionalAndSecretInputStringRequestedEvent != null)
            return OnOptionalAndSecretInputStringRequestedEvent.Invoke(prompt, arg);

        throw new InvalidOperationException(
            $"{nameof(OnOptionalAndSecretInputStringRequestedEvent)} event is not subscribed.");
    }

    /// <summary>
    ///     Requests input as an integer by invoking the subscribed event handler.
    /// </summary>
    /// <param name="prompt">The prompt message for the input request.</param>
    /// <param name="arg">The argument related to the input request.</param>
    /// <returns>The user input as an integer.</returns>
    public int RequestOnInputInt(string prompt, string arg)
    {
        if (OnInputIntRequestedEvent != null)
            return OnInputIntRequestedEvent.Invoke(prompt, arg);

        throw new InvalidOperationException($"{nameof(OnInputIntRequestedEvent)} event is not subscribed.");
    }
}