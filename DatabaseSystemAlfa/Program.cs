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
        MessageBase.OnDisplayRequestedEvent += (_, msg) => AnsiConsole.MarkupLine(msg);
        PromptBase<string>.OnAskRequestedEvent += AnsiConsole.Ask<string>;
        PromptBase<int>.OnAskRequestedEvent += AnsiConsole.Ask<int>;
        PromptBase<string>.OnPromptRequestedEvent += AnsiConsole.Prompt;
        
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
            MessageTemplate.Warning("Config file need to be setup manually").Display();

            MessageTemplate.Italic("The configuration template was auto-generated in the executable file's root folder")
                .PrependNewLine().Display();
        }

        OperationEvents setupConfEvents = new OperationEvents();
        setupConfEvents.OnInputStringRequestedEvent += PromptTemplate<string>.HighlightAsk;
        setupConfEvents.OnOptionalAndSecretInputStringRequestedEvent += PromptTemplate<string>.OptionalAndSecret;
        setupConfEvents.OnInputIntRequestedEvent += PromptTemplate<int>.HighlightAsk;

        var menuOperations = new Dictionary<string, IOperation>
        {
            { "Connect to database", new ConnectToDatabaseOperation(_appSettings, AnsiConsole.Status(), 3) },
            { "Setup configuration", new SetupConfigurationOperation(_appSettings, setupConfEvents) },
            { "Save configuration", new SaveConfigurationOperation(_appSettings)},
            { "Exit", new ExitOperation() }
        };  

        do
        {
            var selectedMenuOperation = PromptTemplate<string>.Selection(
                MessageTemplate.Regular("Select menu operation:").PrependNewLine().ToString(),
                menuOperations.Keys);

            if (menuOperations.TryGetValue(selectedMenuOperation, out var selectedOperation))
            {
                AnsiConsole.Clear();
                
                OperationResult result = selectedOperation.Execute();
                result.Message.Display();
                result.AdditionalMsg?.Display();

                if (selectedMenuOperation.Equals("Exit")) return;
            }
            else
            {
                MessageTemplate.Error("Invalid operation selected").Display();
            }
        } while (!DatabaseSingleton.Instance.ConnectionIsOpen());
    }
}