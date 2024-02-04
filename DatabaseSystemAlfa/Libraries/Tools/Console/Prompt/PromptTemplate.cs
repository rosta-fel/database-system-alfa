using DatabaseSystemAlfa.Libraries.Tools.Console.Message;
using DatabaseSystemAlfa.Libraries.Tools.Console.Message.Extensions;
using Spectre.Console;

namespace DatabaseSystemAlfa.Libraries.Tools.Console.Prompt;

/// <summary>
///     Represents a template class for creating common prompts with enhanced features.
/// </summary>
/// <typeparam name="T">The type of the expected user input.</typeparam>
public abstract class PromptTemplate<T> : PromptBase<T> where T : notnull
{
    /// <summary>
    ///     Displays a selection prompt with a specified title and choices.
    /// </summary>
    /// <param name="title">The title of the selection prompt.</param>
    /// <param name="choices">The available choices for the user.</param>
    /// <returns>The user's selected choice.</returns>
    public static T Selection(string title, IEnumerable<T> choices)
    {
        return RequestOnPrompt(new SelectionPrompt<T>().Title(MessageTemplate.Bold(title).ToString())
            .AddChoices(choices));
    }

    /// <summary>
    ///     Displays a text prompt allowing optional and secret input.
    /// </summary>
    /// <param name="prompt">The prompt message with a highlighted argument.</param>
    /// <param name="arg">The argument to be highlighted.</param>
    /// <returns>The user's response to the prompt.</returns>
    public static T OptionalAndSecret(string prompt, string arg)
    {
        var msgT = MessageTemplate.HighlightArg(prompt, arg).MarkAsOptional();

        var textPrompt = new TextPrompt<T>(msgT.ToString())
            .AllowEmpty()
            .PromptStyle("red")
            .Secret();

        return RequestOnPrompt(textPrompt);
    }

    /// <summary>
    ///     Displays a prompt with a highlighted argument using the ask event.
    /// </summary>
    /// <param name="promptAsk">The prompt message with a highlighted argument.</param>
    /// <param name="arg">The argument to be highlighted.</param>
    /// <returns>The user's response to the prompt.</returns>
    public static T HighlightAsk(string promptAsk, string arg)
    {
        return RequestOnAsk(MessageTemplate.HighlightArg(promptAsk, arg).ToString());
    }
}