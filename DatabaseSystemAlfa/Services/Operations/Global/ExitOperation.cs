using DatabaseSystemAlfa.Libraries.Tools.Console.Message;

namespace DatabaseSystemAlfa.Services.Operations.Global;

public class ExitOperation : IOperation
{
    public OperationResult Execute()
    {
        try
        {
            DatabaseSingleton.Instance.CloseConnection();
            return new OperationResult(MessageTemplate.Success("Successfully concluded pre-exit operations. Application is shutting down..."));
        }
        catch (Exception e)
        {
            return new OperationResult(MessageTemplate.Error($"{e.Message} (Force application shutdown)"));
        }
    }
}