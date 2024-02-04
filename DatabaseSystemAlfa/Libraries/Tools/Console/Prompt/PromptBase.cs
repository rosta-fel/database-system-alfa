using Spectre.Console;

namespace DatabaseSystemAlfa.Libraries.Tools.Console.Prompt;

/// <summary>
///     Represents a base class for prompting user input in a console application.
/// </summary>
/// <typeparam name="T">The type of the expected user input.</typeparam>
public abstract class PromptBase<T> where T : notnull
{
    /// <summary>
    ///     Event triggered when a simple text prompt is requested.
    ///     Subscribers should provide a response based on the given prompt.
    /// </summary>
    public static event Func<string, T>? OnAskRequestedEvent;

    /// <summary>
    ///     Event triggered when a custom prompt implementation is requested.
    ///     Subscribers should provide a response based on the given prompt.
    /// </summary>
    public static event Func<IPrompt<T>, T>? OnPromptRequestedEvent;

    /// <summary>
    ///     Requests user input using a simple text prompt.
    /// </summary>
    /// <param name="prompt">The text prompt presented to the user.</param>
    /// <returns>The user's response to the prompt.</returns>
    /// <exception cref="NullReferenceException">Thrown when the event is not subscribed.</exception>
    protected static T RequestOnAsk(string prompt)
    {
        if (OnAskRequestedEvent != null) return OnAskRequestedEvent.Invoke(prompt);
        throw new NullReferenceException($"{nameof(OnAskRequestedEvent)} event is not subscribed.");
    }

    /// <summary>
    ///     Requests user input using a custom prompt implementation.
    /// </summary>
    /// <param name="prompt">The custom prompt implementation.</param>
    /// <returns>The user's response to the prompt.</returns>
    /// <exception cref="NullReferenceException">Thrown when the event is not subscribed.</exception>
    protected static T RequestOnPrompt(IPrompt<T> prompt)
    {
        if (OnPromptRequestedEvent != null) return OnPromptRequestedEvent.Invoke(prompt);
        throw new NullReferenceException($"{nameof(OnPromptRequestedEvent)} event is not subscribed.");
    }
}