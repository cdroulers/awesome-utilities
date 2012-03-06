namespace System.Net.Mail
{
    /// <summary>
    ///     Interface for mail sending.
    /// </summary>
    public interface ISmtpClient
    {
        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Send(MailMessage message);
        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="recipients">The recipients.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        void Send(string from, string recipients, string subject, string body);
    }
}
