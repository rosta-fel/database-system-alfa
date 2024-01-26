namespace DatabaseSystemAlfa.Library.Tools.Console.Message.Extensions;

public static class StyledMessageExtensions
{
    public static StyledMessage PrependNewLine(this StyledMessage message)
    {
        return new StyledMessage(Environment.NewLine + message);
    }
    
    public static StyledMessage AppendNewLine(this StyledMessage message)
    {
        return new StyledMessage(message + Environment.NewLine);
    }
    
    public static StyledMessage SurroundWithNewLines(this StyledMessage message)
    {
        return new StyledMessage(Environment.NewLine + message + Environment.NewLine);
    }
}