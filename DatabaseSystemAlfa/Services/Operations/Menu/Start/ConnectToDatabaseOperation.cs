using DatabaseSystemAlfa.Libraries.Tools.Console.Message;
using MySql.Data.MySqlClient;
using Spectre.Console;

namespace DatabaseSystemAlfa.Services.Operations.Menu.Start;

public class ConnectToDatabaseOperation(AppSettings appSettings, Status connStatus, int maxConnRetries)
    : IOperation
{
    public OperationResult Execute()
    {
        DatabaseSingleton.Instance.Initialize(appSettings.GetConnectionString());

        MySqlException lastMySqlException = null!;
        OperationResult? result = null;

        connStatus.Start("Connecting to database...", ctx =>
        {
            for (int retry = 1; retry <= maxConnRetries; retry++)
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
                    ctx.Status($"(Error) {retry}/{maxConnRetries} Attempting to connect...");

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