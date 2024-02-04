using DatabaseSystemAlfa.Libraries.Configuration.Settings;
using DatabaseSystemAlfa.Libraries.Tools.Console.Message;
using DatabaseSystemAlfa.Libraries.Tools.Console.Message.Extensions;

namespace DatabaseSystemAlfa.Services.Operations.Menu.Start;

/// <summary>
///     Represents an operation to set up the application configuration settings.
/// </summary>
public class SetupConfigurationOperation : IOperation
{
    private AppSettings _appSettings;
    private readonly OperationEvents _oEvents;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SetupConfigurationOperation" /> class.
    /// </summary>
    /// <param name="appSettings">The application settings to be set up.</param>
    /// <param name="oEvents">The operation events to handle user input.</param>
    public SetupConfigurationOperation(AppSettings appSettings, OperationEvents oEvents)
    {
        _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
        _oEvents = oEvents ?? throw new ArgumentNullException(nameof(oEvents));
    }

    /// <summary>
    ///     Gets user input to set up the application configuration settings.
    /// </summary>
    /// <param name="settings">The application settings to be updated.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="settings" /> is <c>null</c>.</exception>
    private void GetUserInput(ref AppSettings settings)
    {
        if (settings == null) throw new ArgumentNullException(nameof(settings));

        var server = _oEvents.RequestOnInputString("Enter database {0}:", "server");
        var database = _oEvents.RequestOnInputString("Enter {0}:", "database");
        var uid = _oEvents.RequestOnInputString("Enter database {0}:", "user ID");
        var password = _oEvents.RequestOnOptionalAndSecretInputString("Enter user {0}:", "password");
        var port = _oEvents.RequestOnInputInt("Enter {0}:", "port");

        settings.DbConnectionSettings = new DbConnectionSettings(server, database, uid, password, port);
    }

    /// <summary>
    ///     Executes the operation to set up the application configuration settings.
    /// </summary>
    /// <returns>The result of the operation encapsulated in an <see cref="OperationResult" /> object.</returns>
    public OperationResult Execute()
    {
        try
        {
            GetUserInput(ref _appSettings);
            return new OperationResult(MessageTemplate.Success("The settings have been successfully set")
                .PrependNewLine());
        }
        catch (Exception e)
        {
            return new OperationResult(MessageTemplate.Error(e.Message));
        }
    }
}