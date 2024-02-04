using DatabaseSystemAlfa.Libraries.Tools.Console.Message;
using DatabaseSystemAlfa.Libraries.Tools.Console.Message.Extensions;
using Spectre.Console;

namespace DatabaseSystemAlfa.Libraries.Tools.Console.Prompt;

public abstract class PromptTemplate<T> : PromptBase<T> where T : notnull
{
    public static T Selection(string title, IEnumerable<T> choices)
    {
        return RequestOnPrompt(new SelectionPrompt<T>().Title(MessageTemplate.Bold(title).ToString())
            .AddChoices(choices));
    }

    public static T OptionalAndSecret(string prompt, string arg)
    {
        MessageTemplate msgT = MessageTemplate.HighlightArg(prompt, arg).MarkAsOptional();
        
        TextPrompt<T> textPrompt = new TextPrompt<T>(msgT.ToString())
            .AllowEmpty()
            .PromptStyle("red")
            .Secret();
            
        return RequestOnPrompt(textPrompt);
    }

    public static T HighlightAsk(string promptAsk, string arg)
    {
        return RequestOnAsk(MessageTemplate.HighlightArg(promptAsk, arg).ToString());
    }
}