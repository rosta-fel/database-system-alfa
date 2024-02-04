using DatabaseSystemAlfa.Libraries.Configuration;
using DatabaseSystemAlfa.Libraries.Tools.Console.Message;
using DatabaseSystemAlfa.Libraries.Tools.Console.Message.Extensions;
using DatabaseSystemAlfa.Libraries.Tools.Console.Prompt;
using DatabaseSystemAlfa.Services;
using DatabaseSystemAlfa.Services.Operations;
using DatabaseSystemAlfa.Services.Operations.Global;
using DatabaseSystemAlfa.Services.Operations.Menu.Database;
using DatabaseSystemAlfa.Services.Operations.Menu.Start;
using Spectre.Console;

namespace DatabaseSystemAlfa;

/// <summary>
///     Main class representing the entry point of the application.
/// </summary>
public abstract class Program
{
    private static AppSettings? _appSettings;
    private const string ConfigFileName = "app-settings.json";

    /// <summary>
    ///     The entry point method of the application.
    /// </summary>
    public static void Main()
    {
        // Set up event handlers for displaying messages and prompting user input.
        SetupEventHandlers();

        // Set the configuration file name.
        Configurator.ConfigFileName = ConfigFileName;

        try
        {
            // Attempt to load the configuration file.
            _appSettings = new AppSettings(Configurator.InitBuilder().Build());
            MessageTemplate.Info("Configuration file was loaded successfully").Display();
        }
        catch (Exception e)
        {
            // Handle the exception when loading the configuration file.
            MessageTemplate.Error(e.Message).Display();

            // Create a new AppSettings instance and serialize it to a new configuration file.
            _appSettings = new AppSettings();
            Configurator.SerializeToJson(_appSettings, true);
            MessageTemplate.Warning("Config file needs to be set up manually").Display();

            // Inform the user that a configuration template was auto-generated.
            MessageTemplate.Italic("The configuration template was auto-generated in the executable file's root folder")
                .PrependNewLine().Display();
        }

        // Define operations for the menu start.
        var menuStartOperations = new Dictionary<string, IOperation>
        {
            { "Connect to database", new ConnectToDatabaseOperation(_appSettings, AnsiConsole.Status(), 3) },
            {
                "Setup configuration", new SetupConfigurationOperation(_appSettings, new OperationEvents(
                    PromptTemplate<string>.HighlightAsk,
                    PromptTemplate<string>.OptionalAndSecret,
                    PromptTemplate<int>.HighlightAsk
                ))
            },
            { "Save configuration", new SaveConfigurationOperation(_appSettings) },
            { "Exit", new ExitOperation() }
        };

        // Handle menu start operations.
        HandleOperations("Select menu start operation", menuStartOperations, instance => !instance.ConnectionIsOpen());

        // Define operations for the menu database.
        var menuDatabaseOperations = new Dictionary<string, IOperation>
        {
            { "Get from table by parameter", new GetFromTableByParamOperation() },
            { "Create in table", new CreateInTableOperation() },
            { "Update in table", new UpdateInTableOperation() },
            { "Remove from table", new RemoveFromTableOperation() },
            { "Exit", new ExitOperation() }
        };

        // Handle menu database operations.
        HandleOperations("Select menu database operation", menuDatabaseOperations,
            instance => instance.ConnectionIsOpen());
    }

    /// <summary>
    ///     Sets up event handlers for displaying messages and prompting user input.
    /// </summary>
    private static void SetupEventHandlers()
    {
        // Subscribe to the event handlers for displaying messages and prompting user input.
        MessageBase.OnDisplayRequestedEvent += (_, msg) => AnsiConsole.MarkupLine(msg);
        PromptBase<string>.OnAskRequestedEvent += AnsiConsole.Ask<string>;
        PromptBase<int>.OnAskRequestedEvent += AnsiConsole.Ask<int>;
        PromptBase<string>.OnPromptRequestedEvent += AnsiConsole.Prompt;
    }

    /// <summary>
    ///     Handles the execution of operations based on user input and a specified condition.
    /// </summary>
    /// <param name="description">The description of the operation to display to the user.</param>
    /// <param name="operations">A dictionary of available operations.</param>
    /// <param name="condition">The condition that determines whether the loop should continue.</param>
    private static void HandleOperations(string description, Dictionary<string, IOperation> operations,
        Predicate<DatabaseSingleton> condition)
    {
        while (condition(DatabaseSingleton.Instance))
        {
            // Prompt the user to select an operation.
            var selectedOperation = PromptTemplate<string>.Selection(
                MessageTemplate.Regular($"{description}:").PrependNewLine().ToString(),
                operations.Keys);

            // Try to retrieve the selected operation from the dictionary.
            if (!operations.TryGetValue(selectedOperation, out var operation)) continue;

            // Clear the console before executing the operation.
            AnsiConsole.Clear();

            // Execute the selected operation.
            var result = operation.Execute();

            // Display the result message and additional message if present.
            result.Message.Display();
            result.AdditionalMsg?.Display();

            // Break the loop if the operation is an ExitOperation.
            if (operation is ExitOperation) break;
        }
    }
}