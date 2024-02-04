using DatabaseSystemAlfa.Libraries.Configuration.Settings;
using DatabaseSystemAlfa.Libraries.Tools.Console.Message;
using DatabaseSystemAlfa.Libraries.Tools.Console.Message.Extensions;

namespace DatabaseSystemAlfa.Services.Operations.Menu.Start;

public class SetupConfigurationOperation(AppSettings appSettings, OperationEvents oEvents) : IOperation
{
    private void GetUserInput(ref AppSettings settings)
    {
        if (settings == null) throw new ArgumentNullException(nameof(settings));
        
        string server = oEvents.RequestOnInputString("Enter database {0}:", "server");
        string database = oEvents.RequestOnInputString("Enter {0}:", "database");
        string uid = oEvents.RequestOnInputString("Enter database {0}:", "user ID");
        string password = oEvents.RequestOnOptionalAndSecretInputString("Enter user {0}:", "password");
        int port = oEvents.RequestOnInputInt("Enter {0}:", "port");

        settings.DbConnectionSettings = new DbConnectionSettings(server, database, uid, password, port);
    }
    
    public OperationResult Execute()
    {
        try
        {
            GetUserInput(ref appSettings);
            return new OperationResult(MessageTemplate.Success("The settings have been successfully set").PrependNewLine());
        }
        catch (Exception e)
        {
            return new OperationResult(MessageTemplate.Error(e.Message));
        }
    }
}