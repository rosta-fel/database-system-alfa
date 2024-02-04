namespace DatabaseSystemAlfa.Libraries.Tools.Console.Message.Extensions
{
    /// <summary>
    /// Provides extension methods for the <see cref="MessageTemplate"/> class.
    /// </summary>
    public static class MessageTemplateExtensions
    {
        /// <summary>
        /// Prepends a new line to the existing message template.
        /// </summary>
        /// <param name="messageTemplate">The original message template.</param>
        /// <returns>A new message template with a new line added at the beginning.</returns>
        public static MessageTemplate PrependNewLine(this MessageTemplate messageTemplate)
        {
            return new MessageTemplate(Environment.NewLine + messageTemplate);
        }

        /// <summary>
        /// Appends a new line to the existing message template.
        /// </summary>
        /// <param name="messageTemplate">The original message template.</param>
        /// <returns>A new message template with a new line added at the end.</returns>
        public static MessageTemplate AppendNewLine(this MessageTemplate messageTemplate)
        {
            return new MessageTemplate(messageTemplate + Environment.NewLine);
        }

        /// <summary>
        /// Surrounds the existing message template with new lines.
        /// </summary>
        /// <param name="messageTemplate">The original message template.</param>
        /// <returns>A new message template surrounded by new lines.</returns>
        public static MessageTemplate SurroundWithNewLines(this MessageTemplate messageTemplate)
        {
            return new MessageTemplate(Environment.NewLine + messageTemplate + Environment.NewLine);
        }

        /// <summary>
        /// Marks the existing message template as optional.
        /// </summary>
        /// <param name="messageTemplate">The original message template.</param>
        /// <returns>A new message template marked as optional.</returns>
        public static MessageTemplate MarkAsOptional(this MessageTemplate messageTemplate)
        {
            return new MessageTemplate($"[gray][[Optional]][/] {messageTemplate}");
        }
    }
}
