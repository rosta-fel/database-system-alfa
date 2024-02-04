using DatabaseSystemAlfa.Libraries.Configuration;
using DatabaseSystemAlfa.Libraries.Tools.Console.Message;
using DatabaseSystemAlfa.Libraries.Tools.Console.Message.Extensions;
using DatabaseSystemAlfa.Libraries.Tools.Console.Prompt;
using DatabaseSystemAlfa.Services;
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
            MessageTemplate.Info("Configuration file was loaded successfully").Display();
        }
        catch (Exception e)
        {
            MessageTemplate.Error(e.Message).Display();
            
            _appSettings = new AppSettings();
            Configurator.SerializeToJson(_appSettings, true);
            MessageTemplate.Info("The configuration template was auto-generated in the executable file's root folder").Display();
            
            MessageTemplate.Warning("Config file need to be setup manually!").Display();
        }
        
        Dictionary<string, IOperation> menuOperations = new Dictionary<string, IOperation>
        {
            { "Connect to database", new ConnectToDatabaseOperation(_appSettings, AnsiConsole.Status(), 3) },
            { "Setup configuration", new SetupConfigurationOperation() },
            { "Exit", new ExitOperation() }
        };

        do
        {
            string selectedMenuOperation = AnsiConsole.Prompt(PromptTemplate.Classic(
                MessageTemplate.Regular("Choose menu start option:").PrependNewLine().ToString(),
                menuOperations.Keys)
            );

            if (menuOperations.TryGetValue(selectedMenuOperation, out IOperation? selectedOperation))
            {
                AnsiConsole.Clear();
                OperationResult result = selectedOperation.Execute();
            
                if (result.IsSuccess)
                    MessageTemplate.Success(result.Message).Display();
                else
                    MessageTemplate.Error(result.Message).Display();
            
                if (!string.IsNullOrWhiteSpace(result.TipMessage))
                    MessageTemplate.Tip(result.TipMessage).Display();
            }
            else MessageTemplate.Error("Invalid operation selected").Display();
        }while(!DatabaseSingleton.Instance.ConnectionIsOpen());
    }
}