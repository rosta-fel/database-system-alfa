namespace DatabaseSystemAlfa.Libraries.Configuration.Exceptions;

public class InvalidConfigException(string message, string arg) : Exception(string.Format(message, arg));