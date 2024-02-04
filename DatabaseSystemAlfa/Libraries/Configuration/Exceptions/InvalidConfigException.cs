namespace DatabaseSystemAlfa.Libraries.Configuration.Exceptions;

/// <summary>
///     Represents an exception thrown when encountering invalid or missing configuration.
/// </summary>
public class InvalidConfigException : Exception
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="InvalidConfigException" /> class with a specific error message.
    /// </summary>
    /// <param name="message">The error message that describes the reason for the exception.</param>
    /// <param name="arg">Additional information about the invalid configuration.</param>
    public InvalidConfigException(string message, string arg) : base(string.Format(message, arg))
    {
    }
}