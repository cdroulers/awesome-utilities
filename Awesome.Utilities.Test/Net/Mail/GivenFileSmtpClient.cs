using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using NUnit.Framework;

namespace Awesome.Utilities.Test.Net.Mail
{
    [TestFixture]
    public class GivenFileSmtpClient
    {
        [Test]
        public void When_sending_Then_writes_to_file()
        {
            var client = new FileSmtpClient("emails", getFileNameFunc: m => HttpUtility.UrlEncode(m.Subject) + ".txt");

            var message = new MailMessage(new MailAddress("from <from@example.org>"), new MailAddress("to <to@example.org>"));
            message.Subject ="test1";
            message.Body = "body test1";
            message.CC.Add(new MailAddress("cc <cc@example.org>"));
            message.Headers.Add("X-Awesome", "true");

            client.Send(message);

            var text = File.ReadAllText("emails\\test1.txt");

            Assert.That(text, Is.StringContaining("from@example.org"));
            Assert.That(text, Is.StringContaining("to@example.org"));
            Assert.That(text, Is.StringContaining("cc@example.org"));
            Assert.That(text, Is.StringContaining("X-Awesome = true"));

            Console.WriteLine("OUTPUT: " + Environment.NewLine + text);
        }
    }
}
