using DatabaseSystemAlfa.Libraries.Tools.Console.Message;

namespace DatabaseSystemAlfa.Services.Operations;

public record OperationResult(
    MessageBase Message,
    MessageBase? AdditionalMsg = null
    );