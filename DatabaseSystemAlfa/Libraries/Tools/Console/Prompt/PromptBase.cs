using Spectre.Console;

namespace DatabaseSystemAlfa.Libraries.Tools.Console.Prompt;

public abstract class PromptBase<T> where T: notnull
{
    public static event Func<string, T>? OnAskRequestedEvent;
    public static event Func<IPrompt<T>, T>? OnPromptRequestedEvent;

    protected static T RequestOnAsk(string prompt)
    {
        if (OnAskRequestedEvent != null) return OnAskRequestedEvent.Invoke(prompt);
        throw new NullReferenceException($"{nameof(OnAskRequestedEvent)} event doesnt have subscribers.");
    }

    protected static T RequestOnPrompt(IPrompt<T> prompt)
    {
        if (OnPromptRequestedEvent != null) return OnPromptRequestedEvent.Invoke(prompt);
        throw new NullReferenceException($"{nameof(OnPromptRequestedEvent)} event doesnt have subscribers.");
    }
}