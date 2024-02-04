namespace DatabaseSystemAlfa.Libraries.Tools.Console.Message;

/// <summary>
///     Represents a message template with various formatting options for console display.
/// </summary>
public class MessageTemplate(string message) : MessageBase(message)
{
    /// <summary>
    ///     Creates a new instance of <see cref="MessageTemplate" /> with regular formatting.
    /// </summary>
    /// <param name="message">The content of the message.</param>
    /// <returns>A new instance of <see cref="MessageTemplate" /> with regular formatting.</returns>
    public static MessageTemplate Regular(string message)
    {
        return new MessageTemplate(message);
    }

    /// <summary>
    ///     Creates a new instance of <see cref="MessageTemplate" /> with information formatting.
    /// </summary>
    /// <param name="message">The content of the message.</param>
    /// <returns>A new instance of <see cref="MessageTemplate" /> with information formatting.</returns>
    public static MessageTemplate Info(string message)
    {
        return new MessageTemplate($"[cyan]INFO[/]: [gray]{message}[/]");
    }

    /// <summary>
    ///     Creates a new instance of <see cref="MessageTemplate" /> with success formatting.
    /// </summary>
    /// <param name="message">The content of the message.</param>
    /// <returns>A new instance of <see cref="MessageTemplate" /> with success formatting.</returns>
    public static MessageTemplate Success(string message)
    {
        return new MessageTemplate($"[green]{message}[/]");
    }

    /// <summary>
    ///     Creates a new instance of <see cref="MessageTemplate" /> with warning formatting.
    /// </summary>
    /// <param name="message">The content of the message.</param>
    /// <returns>A new instance of <see cref="MessageTemplate" /> with warning formatting.</returns>
    public static MessageTemplate Warning(string message)
    {
        return new MessageTemplate($"[yellow]WARN[/]: [gray]{message}[/]");
    }

    /// <summary>
    ///     Creates a new instance of <see cref="MessageTemplate" /> with error formatting.
    /// </summary>
    /// <param name="message">The content of the message.</param>
    /// <returns>A new instance of <see cref="MessageTemplate" /> with error formatting.</returns>
    public static MessageTemplate Error(string message)
    {
        return new MessageTemplate($"[red]ERROR[/]: [gray]{message}[/]");
    }

    /// <summary>
    ///     Creates a new instance of <see cref="MessageTemplate" /> with tip formatting.
    /// </summary>
    /// <param name="message">The content of the message.</param>
    /// <returns>A new instance of <see cref="MessageTemplate" /> with tip formatting.</returns>
    public static MessageTemplate Tip(string message)
    {
        return new MessageTemplate($"[blue]TIP[/]: [gray]{message}[/]");
    }

    /// <summary>
    ///     Creates a new instance of <see cref="MessageTemplate" /> with bold formatting.
    /// </summary>
    /// <param name="message">The content of the message.</param>
    /// <returns>A new instance of <see cref="MessageTemplate" /> with bold formatting.</returns>
    public static MessageTemplate Bold(string message)
    {
        return new MessageTemplate($"[bold]{message}[/]");
    }

    /// <summary>
    ///     Creates a new instance of <see cref="MessageTemplate" /> with italic formatting.
    /// </summary>
    /// <param name="message">The content of the message.</param>
    /// <returns>A new instance of <see cref="MessageTemplate" /> with italic formatting.</returns>
    public static MessageTemplate Italic(string message)
    {
        return new MessageTemplate($"[italic]{message}[/]");
    }

    /// <summary>
    ///     Creates a new instance of <see cref="MessageTemplate" /> with highlighted argument formatting.
    /// </summary>
    /// <param name="promptMsg">The message with a placeholder for the highlighted argument.</param>
    /// <param name="promptArg">The argument to be highlighted.</param>
    /// <returns>A new instance of <see cref="MessageTemplate" /> with highlighted argument formatting.</returns>
    public static MessageTemplate HighlightArg(string promptMsg, string promptArg)
    {
        return new MessageTemplate(string.Format(promptMsg, $"[green][underline]{promptArg}[/][/]"));
    }
}