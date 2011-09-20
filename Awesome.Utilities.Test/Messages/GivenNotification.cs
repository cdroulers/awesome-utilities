using System;
using System.Collections.Generic;
using System.Linq;
using System.Messages;
using System.Text;
using NUnit.Framework;

namespace Awesome.Utilities.Test.Messages
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenNotification
    {
        private IDictionary<string, object> store;
        private Notification notification;

        [SetUp]
        public void SetUp()
        {
            this.store = new Dictionary<string, object>();
            this.notification = new Notification(this.store);
        }

        [Test]
        public void When_setting_notice_Then_it_returns_the_data_after()
        {
            this.notification.Success = "Text";
            Assert.That((string)this.notification.Success, Is.EqualTo("Text"));
            Assert.That(this.notification.Success.ToString(), Is.EqualTo("Text"));
        }

        [Test]
        public void When_setting_info_Then_it_returns_the_data_after()
        {
            this.notification.Info = "Text";
            Assert.That((string)this.notification.Info, Is.EqualTo("Text"));
            Assert.That(this.notification.Info.ToString(), Is.EqualTo("Text"));
        }

        [Test]
        public void When_setting_warning_Then_it_returns_the_data_after()
        {
            this.notification.Warning = "Text";
            Assert.That((string)this.notification.Warning, Is.EqualTo("Text"));
            Assert.That(this.notification.Warning.ToString(), Is.EqualTo("Text"));
        }

        [Test]
        public void When_setting_error_Then_it_returns_the_data_after()
        {
            this.notification.Error = "Text";
            Assert.That((string)this.notification.Error, Is.EqualTo("Text"));
            Assert.That(this.notification.Error.ToString(), Is.EqualTo("Text"));
        }

        [TestCase("", "", "error", "", true)]
        [TestCase("", "warning", "", "", true)]
        [TestCase("notice", "", "", "", true)]
        [TestCase("", "", "", "info", true)]
        [TestCase("", "", "", "", false)]
        public void When_setting_any_flash_and_getting_Any_Then_it_returns_the_right_stuff(string notice, string warning, string error, string info, bool expected)
        {
            this.notification.Success = notice;
            this.notification.Warning = warning;
            this.notification.Error = error;
            this.notification.Info = info;
            Assert.That(this.notification.Any(), Is.EqualTo(expected));
        }

        [Test]
        public void When_setting_any_flash_Then_IsEmpty_works()
        {
            this.notification.Error = "Text";
            Assert.That(this.notification.Error.IsEmpty, Is.EqualTo(false));
            this.notification.Error = "";
            Assert.That(this.notification.Error.IsEmpty, Is.EqualTo(true));
        }

        [TestCase("What", "What", false, "WhatWhat")]
        [TestCase("What", "What", true, "What{newline}What")]
        public void When_adding_to_flash_Then_it_concatenates_the_data(string data1, string data2, bool newLines, string expected)
        {
            this.notification.Error.Add(data1, newLines);
            this.notification.Error.Add(data2, newLines);

            expected = expected.Replace("{newline}", Environment.NewLine);

            Assert.That((string)this.notification.Error, Is.EqualTo(expected));
            Assert.That(this.notification.Error.ToString(), Is.EqualTo(expected));
        }
    }
}
