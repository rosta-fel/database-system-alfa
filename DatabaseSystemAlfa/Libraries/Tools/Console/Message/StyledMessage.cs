namespace DatabaseSystemAlfa.Libraries.Tools.Console.Message;

// TODO: Rename to PatternMessage, TemplateMessage or LayoutMessage
public class StyledMessage(string message) : MessageBase(message)
{
    public static StyledMessage Info(string message) => new($"[cyan]INFO[/]: [gray]{message}[/]");
    
    public static StyledMessage Success(string message) => new($"[green]{message}[/]");

    public static StyledMessage Warning(string message) => new($"[yellow]WARN[/]: [gray]{message}[/]");

    public static StyledMessage Error(string message) => new($"[red]ERROR[/]: [gray]{message}[/]");
    
    public static StyledMessage Tip(string message) => new($"[blue]TIP[/]: [gray]{message}[/]");

    public static StyledMessage Title(string message) => new($"[bold]{message}[/]");
}