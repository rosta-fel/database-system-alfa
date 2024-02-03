namespace DatabaseSystemAlfa.Services.Operations;

public record OperationResult(
    bool IsSuccess,
    string Message,
    string? TipMessage = null
    );