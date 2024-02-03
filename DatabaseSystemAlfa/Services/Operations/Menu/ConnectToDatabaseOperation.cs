using MySql.Data.MySqlClient;
using Spectre.Console;

namespace DatabaseSystemAlfa.Services.Operations.Menu;

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
            {
                try
                {
                    DatabaseSingleton.Instance.Connection?.Open();
                    result = new OperationResult(true, "Successfully created connection with the database");
                    return;
                }
                catch (MySqlException mySqlException)
                {
                    lastMySqlException = mySqlException;
                
                    if (retry == 1) ctx.Spinner(Spinner.Known.GrowVertical);
                    ctx.Status($"(Error) {retry}/{maxConnRetries} Attempting to connect...");

                    Thread.Sleep(1000 * retry);
                }
            }
        });
    
        DatabaseSingleton.Instance.CloseConnection();
        
        return result ??= new OperationResult(
            false,
            lastMySqlException.Message,
            "Ensure the database server is running, and verify that the connection configuration is correct"
        );
    }
}