using DatabaseSystemAlfa.Libraries.Tools.Console.Message;
using MySql.Data.MySqlClient;
using Spectre.Console;

namespace DatabaseSystemAlfa.Services.Operations.Menu.Start;

/// <summary>
///     Represents an operation to connect to the database using the provided application settings.
/// </summary>
public class ConnectToDatabaseOperation : IOperation
{
    private readonly AppSettings _appSettings;
    private readonly Status _connStatus;
    private readonly int _maxConnRetries;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ConnectToDatabaseOperation" /> class.
    /// </summary>
    /// <param name="appSettings">The application settings containing database connection information.</param>
    /// <param name="connStatus">The status indicator to display connection progress.</param>
    /// <param name="maxConnRetries">The maximum number of retries for connecting to the database.</param>
    public ConnectToDatabaseOperation(AppSettings appSettings, Status connStatus, int maxConnRetries)
    {
        _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
        _connStatus = connStatus ?? throw new ArgumentNullException(nameof(connStatus));
        _maxConnRetries = maxConnRetries;
    }

    /// <summary>
    ///     Executes the operation to connect to the database.
    /// </summary>
    /// <returns>The result of the operation encapsulated in an <see cref="OperationResult" /> object.</returns>
    public OperationResult Execute()
    {
        DatabaseSingleton.Instance.Initialize(_appSettings.GetConnectionString());

        MySqlException lastMySqlException = null!;
        OperationResult? result = null;

        _connStatus.Start("Connecting to database...", ctx =>
        {
            for (var retry = 1; retry <= _maxConnRetries; retry++)
                try
                {
                    DatabaseSingleton.Instance.Connection?.Open();
                    result = new OperationResult(
                        MessageTemplate.Success("Successfully created connection with the database"));
                    return;
                }
                catch (MySqlException mySqlException)
                {
                    lastMySqlException = mySqlException;

                    if (retry == 1) ctx.Spinner(Spinner.Known.GrowVertical);
                    ctx.Status($"(Error) {retry}/{_maxConnRetries} Attempting to connect...");

                    Thread.Sleep(1000 * retry);
                }
        });

        if (result is not null) return result;

        DatabaseSingleton.Instance.CloseConnection();

        return new OperationResult(
            MessageTemplate.Error(lastMySqlException.Message),
            MessageTemplate.Tip(
                "Ensure the database server is running, and verify that the connection configuration is correct"
            ));
    }
}