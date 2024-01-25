using DatabaseSystemAlfa.Configuration;
using DatabaseSystemAlfa.Configuration.Structs;
using Microsoft.Extensions.Configuration;
using Spectre.Console;

AppSettings appSettings;

try
{
    IConfigurationRoot configurationRoot = AppConfigurator.Build("app-settings.json");
    appSettings = new AppSettings(configurationRoot);
}
catch (Exception e)
{
    AnsiConsole.Markup("[red]{0}[/]", e.Message);
}