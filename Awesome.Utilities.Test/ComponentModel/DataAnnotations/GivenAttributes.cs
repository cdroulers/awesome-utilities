using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Net.Mail;

namespace Awesome.Utilities.Test.ComponentModel.DataAnnotations
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenAttributes
    {
        [Test]
        public void When_validating_url_with_invalid_Then_validates_to_false()
        {
            var instance = new TestClass()
            {
                Uri = new Uri("http://lol.com/"),
                MailAddress = new MailAddress("lol@example.org")
            };
            var results = new Collection<ValidationResult>();
            var context = new ValidationContext(instance, null, null);
            bool valid = Validator.TryValidateObject(instance, context, results, true);

            Assert.That(valid, Is.EqualTo(false));
            Assert.That(results, Has.Count.EqualTo(2));
        }
        [Test]
        public void When_validating_url_Then_works()
        {
            var instance = new TestClass()
            {
                Uri = new Uri("http://lo"),
                MailAddress = new MailAddress("lol@exa")
            };
            var results = new Collection<ValidationResult>();
            var context = new ValidationContext(instance, null, null);
            bool valid = Validator.TryValidateObject(instance, context, results, true);

            Assert.That(valid, Is.EqualTo(true));
            Assert.That(results, Has.Count.EqualTo(0));
        }

        public class TestClass
        {
            [UriLength(10)]
            public Uri Uri { get; set; }
            [MailAddressLength(10)]
            public MailAddress MailAddress { get; set; }
        }
    }
}
