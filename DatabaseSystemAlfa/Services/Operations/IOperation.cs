namespace DatabaseSystemAlfa.Services.Operations;

/// <summary>
///     Represents an interface for various operations within the application.
/// </summary>
public interface IOperation
{
    /// <summary>
    ///     Executes the operation.
    /// </summary>
    /// <returns>The result of the operation encapsulated in an <see cref="OperationResult" /> object.</returns>
    OperationResult Execute();
}