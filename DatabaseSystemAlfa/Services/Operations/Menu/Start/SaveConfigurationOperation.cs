using DatabaseSystemAlfa.Libraries.Configuration;
using DatabaseSystemAlfa.Libraries.Tools.Console.Message;

namespace DatabaseSystemAlfa.Services.Operations.Menu.Start;

public class SaveConfigurationOperation(AppSettings appSettings) : IOperation
{
    public OperationResult Execute()
    {
        try
        {
            Configurator.SerializeToJson(appSettings, true);
            return new OperationResult(MessageTemplate.Success("The configuration was successfully saved in the root folder of the executable."));
        }
        catch (Exception e)
        {
            return new OperationResult(MessageTemplate.Error(e.Message));
        }
    }
}