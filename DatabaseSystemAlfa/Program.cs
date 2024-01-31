using DatabaseSystemAlfa.Library.Configuration;
using DatabaseSystemAlfa.Library.Tools.Console.Message;
using DatabaseSystemAlfa.Library.Tools.Console.Message.Extensions;
using Spectre.Console;

namespace DatabaseSystemAlfa;

public abstract class Program
{
    private static AppSettings? _appSettings;
    
    public static void Main()
    {
        MessageBase.DisplayRequestedEvent += (_, message) => AnsiConsole.MarkupLine(message);
        
        ShowUserWelcomeContent();

        try
        {
            _appSettings = new AppSettings(Configurator.Build());
            StyledMessage.Info("Config file was loaded successfully.").Display();
        }
        catch (Exception e)
        {
            StyledMessage.Error(e.Message).AppendNewLine().Display();
            StyledMessage.Warning("Config file need to be setup manually!").Display();

            _appSettings = new AppSettings();
        }
        
    }

    private static void ShowUserWelcomeContent()
    {
        AnsiConsole.Write(
            new FigletText("Alfa3")
                .Centered()
                .Color(Color.HotPink));

        AnsiConsole.Write(
            new Rule("[red]Welcome to Database System Alfa[/]")
                .Centered()
                .RuleStyle("orchid dim"));

        AnsiConsole.WriteLine();
    }
}