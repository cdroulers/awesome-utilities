using System.IO;
using System.Text;

namespace System.Net.Mail
{
    /// <summary>
    ///     An SMTP client that actually just writes files to disk.
    /// </summary>
    public class FileSmtpClient : ISmtpClient
    {
        private readonly string directory;
        private readonly Func<MailMessage, string> getFileNameFunc;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSmtpClient"/> class.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="getFileNameFunc">A function to create a file name in the directory for each message sent.</param>
        public FileSmtpClient(string directory = null, Func<MailMessage, string> getFileNameFunc = null)
        {
            this.directory = directory ?? "emails";
            this.getFileNameFunc = getFileNameFunc ?? (m => string.Format(@"{0}.txt", DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss")));
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Send(MailMessage message)
        {
            if (!Directory.Exists(this.directory))
            {
                Directory.CreateDirectory(this.directory);
            }

            var builder = new StringBuilder();
            foreach (var header in message.Headers.AllKeys)
            {
                builder.AppendLine(string.Format("HEADER: {0} = {1}", header, message.Headers[header]));
            }

            builder.AppendLine("From: " + message.From);
            foreach (var to in message.To)
            {
                builder.AppendLine("To: " + to);
            }

            foreach (var cc in message.CC)
            {
                builder.AppendLine("CC: " + cc);
            }

            foreach (var bcc in message.Bcc)
            {
                builder.AppendLine("Bcc: " + bcc);
            }
            
            builder.AppendLine();
            builder.AppendLine("Subject: " + message.Subject);
            builder.AppendLine("Body: ");
            builder.AppendLine(message.Body);

            File.WriteAllText(Path.Combine(this.directory, this.getFileNameFunc(message)), builder.ToString());
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="from">The sender of the email.</param>
        /// <param name="recipients">The recipients.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        public void Send(string from, string recipients, string subject, string body)
        {
            this.Send(new MailMessage(from, recipients, subject, body));
        }
    }
}
