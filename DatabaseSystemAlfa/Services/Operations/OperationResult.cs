using DatabaseSystemAlfa.Libraries.Tools.Console.Message;

namespace DatabaseSystemAlfa.Services.Operations;

/// <summary>
///     Represents the result of an operation, including a primary message and an optional additional message.
/// </summary>
public record OperationResult(
    MessageBase Message,
    MessageBase? AdditionalMsg = null
);