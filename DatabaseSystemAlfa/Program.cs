using DatabaseSystemAlfa.Library.Configuration;
using DatabaseSystemAlfa.Library.Tools.Console.Message;
using Microsoft.Extensions.Configuration;
using Spectre.Console;

namespace DatabaseSystemAlfa;

public abstract class Program
{
    private static AppSettings? _appSettings;
    private const string ConfigFileName = "app-settings.json";
    
    public static void Main()
    {
        MessageBase.DisplayRequestedEvent += (_, msg) => AnsiConsole.MarkupLine(msg);
        Configurator.ConfigFileName = ConfigFileName;
        
        try
        {
            IConfiguration configuration = Configurator.InitBuilder().Build();
            
            _appSettings = new AppSettings(configuration);
            StyledMessage.Info("Configuration file was loaded successfully").Display();
        }
        catch (Exception e)
        {
            StyledMessage.Error(e.Message).Display();
            StyledMessage.Warning("Config file need to be setup manually!") .Display();
            
            _appSettings = new AppSettings();
            Configurator.SerializeToJson(_appSettings, true);
        }
    }
}