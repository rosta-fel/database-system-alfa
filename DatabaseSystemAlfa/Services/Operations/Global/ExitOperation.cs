namespace DatabaseSystemAlfa.Services.Operations.Global;

public class ExitOperation : IOperation
{
    public OperationResult Execute()
    {
        try
        {
            DatabaseSingleton.Instance.CloseConnection();
            return new OperationResult(true, "Successful exit");
        }
        catch (Exception e)
        {
            return new OperationResult(false, e.Message);
        }
    }
}