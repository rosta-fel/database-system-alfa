using DatabaseSystemAlfa.Library.Configuration;
using DatabaseSystemAlfa.Library.Configuration.Structs;
using DatabaseSystemAlfa.Library.Tools.Console.Message;
using DatabaseSystemAlfa.Library.Tools.Console.Message.Extensions;
using Spectre.Console;

namespace DatabaseSystemAlfa;

public abstract class Program
{
    private const string ConfigFileName = "app-settings.json";
    
    public static void Main()
    {
        MessageBase.DisplayRequestedEvent += (_, message) => AnsiConsole.MarkupLine(message);

        AppSettings? appSettings = null;

        AnsiConsole.Write(
            new FigletText("Alfa3")
                .Centered()
                .Color(Color.HotPink));

        AnsiConsole.Write(
            new Rule("[red]Welcome to Database System Alfa[/]")
                .Centered()
                .RuleStyle("orchid dim"));

        AnsiConsole.WriteLine();

        try
        {
            var configurationRoot = Configurator.Build(ConfigFileName);
            appSettings = new AppSettings(configurationRoot);

            StyledMessage.Info("Config file was loaded successfully.").Display();
        }
        catch (Exception e)
        {
            StyledMessage.Error(e.Message).Display();
        }

        if (appSettings == null)
        {
            while (true)
            {
                StyledMessage.Warning("Config file need to be setup manually:")
                    .PrependNewLine()
                    .Display();

                var host = AnsiConsole.Ask<string>("Enter server database [green]host[/]:");
                var user = AnsiConsole.Ask<string>("Enter database [green]user[/]:");
                var password = AnsiConsole.Prompt(
                    new TextPrompt<string>("[grey][[Optional]][/] Enter database user [green]password[/]:")
                        .AllowEmpty()
                        .PromptStyle("red")
                        .Secret());
                var name = AnsiConsole.Ask<string>("Enter database [green]name[/]:");
                var port = AnsiConsole.Ask<int>("Enter database [green]port[/]:");

                try
                {
                    var databaseConfig = new DatabaseConfig(host, user, password, name, port);
                    appSettings = new AppSettings(databaseConfig);

                    StyledMessage.Success("Config file was created successfully!")
                        .SurroundWithNewLines()
                        .Display();

                    break;
                }
                catch (Exception e)
                {
                    StyledMessage.Error(e.Message)
                        .PrependNewLine()
                        .Display();
                }
            }

            try
            {
                if (AnsiConsole.Confirm("Want to [yellow]save the configuration?[/]"))
                {
                    AppSettings.SaveTo(ConfigFileName, appSettings);
                    StyledMessage.Success("Config file was saved successfully!")
                        .PrependNewLine()
                        .Display();
                    StyledMessage
                        .Info(
                            $"Configuration save path: \n{Path.Combine(Directory.GetCurrentDirectory(), ConfigFileName)}")
                        .AppendNewLine()
                        .Display();
                }
            }
            catch (Exception e)
            {
                StyledMessage.Error(e.Message);
            }
        }
    }
}