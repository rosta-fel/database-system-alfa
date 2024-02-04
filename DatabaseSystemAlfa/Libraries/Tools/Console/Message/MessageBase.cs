namespace DatabaseSystemAlfa.Libraries.Tools.Console.Message
{
    /// <summary>
    /// An abstract base class for messages with common display functionality.
    /// </summary>
    public abstract class MessageBase : IDisplayHandler
    {
        private readonly string _message;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBase"/> class with a specified message.
        /// </summary>
        /// <param name="message">The content of the message.</param>
        protected MessageBase(string message)
        {
            _message = message;
        }

        /// <summary>
        /// An event that is triggered when a display of the message is requested.
        /// </summary>
        public static event EventHandler<string>? OnDisplayRequestedEvent;

        /// <summary>
        /// Displays the message by invoking the display event.
        /// </summary>
        public void Display()
        {
            RequestOnDisplay(_message);
        }

        /// <summary>
        /// Requests to display the message by invoking the display event.
        /// </summary>
        /// <param name="messageInput">The message content to be displayed.</param>
        private void RequestOnDisplay(string messageInput)
        {
            OnDisplayRequestedEvent?.Invoke(this, messageInput);
        }

        /// <summary>
        /// Returns the string representation of the message.
        /// </summary>
        /// <returns>The content of the message.</returns>
        public override string ToString() => _message;
    }
}