using DatabaseSystemAlfa.Libraries.Configuration;
using DatabaseSystemAlfa.Libraries.Tools.Console.Message;

namespace DatabaseSystemAlfa.Services.Operations.Menu.Start;

/// <summary>
///     Represents an operation to save the application settings configuration.
/// </summary>
public class SaveConfigurationOperation : IOperation
{
    private readonly AppSettings _appSettings;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SaveConfigurationOperation" /> class.
    /// </summary>
    /// <param name="appSettings">The application settings to be saved.</param>
    public SaveConfigurationOperation(AppSettings appSettings)
    {
        _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
    }

    /// <summary>
    ///     Executes the operation to save the application settings configuration.
    /// </summary>
    /// <returns>The result of the operation encapsulated in an <see cref="OperationResult" /> object.</returns>
    public OperationResult Execute()
    {
        try
        {
            Configurator.SerializeToJson(_appSettings, true);
            return new OperationResult(
                MessageTemplate.Success(
                    "The configuration was successfully saved in the root folder of the executable."));
        }
        catch (Exception e)
        {
            return new OperationResult(MessageTemplate.Error(e.Message));
        }
    }
}