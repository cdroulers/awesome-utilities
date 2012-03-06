namespace System.Net.Mail
{
    /// <summary>
    ///     A wrapper around the base SmtpClient
    /// </summary>
    public class SmtpClientWrapper : ISmtpClient
    {
        private readonly SmtpClient smtpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmtpClientWrapper"/> class.
        /// </summary>
        /// <param name="smtpClient">The SMTP client.</param>
        public SmtpClientWrapper(SmtpClient smtpClient)
        {
            this.smtpClient = smtpClient;
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Send(MailMessage message)
        {
            this.smtpClient.Send(message);
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="recipients">The recipients.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        public void Send(string from, string recipients, string subject, string body)
        {
            this.smtpClient.Send(from, recipients, subject, body);
        }
    }
}
