using DatabaseSystemAlfa.Libraries.Configuration;
using DatabaseSystemAlfa.Libraries.Tools.Console.Message;
using DatabaseSystemAlfa.Libraries.Tools.Console.Message.Extensions;
using DatabaseSystemAlfa.Services.Operations;
using DatabaseSystemAlfa.Services.Operations.Global;
using DatabaseSystemAlfa.Services.Operations.Menu;
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
            _appSettings = new AppSettings(Configurator.InitBuilder().Build());
            StyledMessage.Info("Configuration file was loaded successfully").Display();
        }
        catch (Exception e)
        {
            StyledMessage.Error(e.Message).Display();
            StyledMessage.Warning("Config file need to be setup manually!").Display();
            
            _appSettings = new AppSettings();
            Configurator.SerializeToJson(_appSettings, true);
        }
        
        Dictionary<string, IOperation> menuOperations = new Dictionary<string, IOperation>
        {
            { "Connect to database", new ConnectToDatabaseOperation(_appSettings, AnsiConsole.Status(), 3) },
            { "Setup configuration", new SetupConfigurationOperation() },
            { "Exit", new ExitOperation() }
        };

        string selectedMenuOperation = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(StyledMessage.Title("Choose menu start option:").PrependNewLine().ToString())
                .AddChoices(menuOperations.Keys));

        if (menuOperations.TryGetValue(selectedMenuOperation, out IOperation? selectedOperation))
        {
            AnsiConsole.Clear();
            OperationResult result = selectedOperation.Execute();
            
            if (result.IsSuccess)
                StyledMessage.Success(result.Message).Display();
            else
                StyledMessage.Error(result.Message).Display();
            
            if (!string.IsNullOrWhiteSpace(result.TipMessage))
                StyledMessage.Tip(result.TipMessage).Display();
        }
        else StyledMessage.Error("Invalid operation selected").Display();
    }
}