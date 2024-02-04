using DatabaseSystemAlfa.Libraries.Tools.Console.Message;
using Spectre.Console;

namespace DatabaseSystemAlfa.Libraries.Tools.Console.Prompt;

public abstract class PromptTemplate(SelectionPrompt<string> selectionPrompt)
{
    public static SelectionPrompt<string> Classic(string title, IEnumerable<string> choices) =>
        new SelectionPrompt<string>().Title(MessageTemplate.Bold(title).ToString()).AddChoices(choices);
}