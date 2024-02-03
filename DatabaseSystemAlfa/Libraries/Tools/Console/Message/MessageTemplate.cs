namespace DatabaseSystemAlfa.Libraries.Tools.Console.Message;

public class MessageTemplate(string message) : MessageBase(message)
{
    public static MessageTemplate Info(string message) => new($"[cyan]INFO[/]: [gray]{message}[/]");
    
    public static MessageTemplate Success(string message) => new($"[green]{message}[/]");

    public static MessageTemplate Warning(string message) => new($"[yellow]WARN[/]: [gray]{message}[/]");

    public static MessageTemplate Error(string message) => new($"[red]ERROR[/]: [gray]{message}[/]");
    
    public static MessageTemplate Tip(string message) => new($"[blue]TIP[/]: [gray]{message}[/]");

    public static MessageTemplate Title(string message) => new($"[bold]{message}[/]");
}