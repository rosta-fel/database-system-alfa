using System.Reflection;
using DatabaseSystemAlfa.Configuration;
using DatabaseSystemAlfa.Configuration.Structs;
using Microsoft.Extensions.Configuration;
using Spectre.Console;

AppSettings? appSettings = null;
string configFileName = "app-settings.json";

try
{
    IConfigurationRoot configurationRoot = AppConfigurator.Build(configFileName);
    appSettings = new AppSettings(configurationRoot);
    
    AnsiConsole.MarkupLine("[green]INFO:[/] Config file was loaded successfully!\n");
}
catch (Exception e)
{
    AnsiConsole.MarkupLine("[red]{0}[/]", e.Message);
}

if (appSettings == null)
{
    while (true)
    {
        AnsiConsole.MarkupLine("[yellow]WARN:[/] Config file need to be setup manually:");
        
        string host = AnsiConsole.Ask<string>("Enter server database [green]host[/]:");
        string user = AnsiConsole.Ask<string>("Enter database [green]user[/]:");
        string password = AnsiConsole.Prompt(
            new TextPrompt<string>("[grey][[Optional]][/] Enter database user [green]password[/]:")
                .AllowEmpty()
                .PromptStyle("red")
                .Secret());
        string name = AnsiConsole.Ask<string>("Enter database [green]name[/]:");
        int port = AnsiConsole.Ask<int>("Enter database [green]port[/]:");
        
        try
        {
            DatabaseConfig databaseConfig = new DatabaseConfig(host, user, password, name, port);
            appSettings = new AppSettings(databaseConfig);
            
            AnsiConsole.MarkupLine("\n[green]Config file was created successfully![/]\n");
            
            break;
        }
        catch (Exception e)
        {
            AnsiConsole.MarkupLine("\n[red]{0}[/]\n", e.Message);
        }
    }
    
    try
    {
        if (AnsiConsole.Confirm("Want to [yellow]save the configuration?[/]"))
        {
            AppSettings.SaveTo(configFileName, appSettings);
            AnsiConsole.MarkupLine("\n[green]Config file was saved successfully![/]\n");
        }
    }
    catch (Exception e)
    {
        AnsiConsole.MarkupLine("\n[red]{0}[/]\n", e.Message);
    }
}