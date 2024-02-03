namespace DatabaseSystemAlfa.Libraries.Tools.Console.Message.Extensions;

public static class MessageTemplateExtensions
{
    public static MessageTemplate PrependNewLine(this MessageTemplate messageTemplate)
    {
        return new MessageTemplate(Environment.NewLine + messageTemplate);
    }
    
    public static MessageTemplate AppendNewLine(this MessageTemplate messageTemplate)
    {
        return new MessageTemplate(messageTemplate + Environment.NewLine);
    }
    
    public static MessageTemplate SurroundWithNewLines(this MessageTemplate messageTemplate)
    {
        return new MessageTemplate(Environment.NewLine + messageTemplate + Environment.NewLine);
    }
}