using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Web.Mvc;
using System.Web.Mvc.UI;

namespace Awesome.Utilities.Test.Web.Mvc.UI
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenHtmlHelperExtensions : GivenHtmlBase
    {
        private HtmlHelper html;

        [SetUp]
        public void SetUp()
        {
            html = Helpers.CreateHtmlHelper();
        }

        [Test]
        public void When_creating_submit_Then_works()
        {
            var result = html.Submit("Lol");
            string actual = result.ToString();
            var element = this.GetHtmlNode(actual);

            Assert.That(element.Name, Is.EqualTo("input"));
            Assert.That(element.Attributes["value"].Value, Is.EqualTo("Lol"));
            Assert.That(element.Attributes["type"].Value, Is.EqualTo("submit"));
        }

        [Test]
        public void When_creating_submit_with_no_text_Then_works()
        {
            Control.TranslationDelegate = s => s + "Wot";

            var result = html.Submit();
            string actual = result.ToString();
            var element = this.GetHtmlNode(actual);

            Assert.That(element.Name, Is.EqualTo("input"));
            Assert.That(element.Attributes["value"].Value, Is.EqualTo("SaveWot"));
            Assert.That(element.Attributes["type"].Value, Is.EqualTo("submit"));

            Control.TranslationDelegate = s => s;
        }

        [TestCase(false, 0, 1)]
        [TestCase(true, 1, 0)]
        public void When_creating_submit_cancel_Then_works(bool invert, int submitIndex, int cancelIndex)
        {
            var result = html.SubmitCancel("/teams", "Lol", "Wot", cancelFirst: invert);
            string actual = result.ToString().Replace("><", ">><<"); // Small hack to split easily
            var parts = actual.Split(new string[] { "><" }, StringSplitOptions.RemoveEmptyEntries);
            Assert.That(parts, Has.Length.EqualTo(2));

            var submit = this.GetHtmlNode(parts[submitIndex]);
            Assert.That(submit.Name, Is.EqualTo("input"));
            Assert.That(submit.Attributes["value"].Value, Is.EqualTo("Lol"));
            Assert.That(submit.Attributes["type"].Value, Is.EqualTo("submit"));

            var cancel = this.GetHtmlNode(parts[cancelIndex]);
            Assert.That(cancel.Name, Is.EqualTo("a"));
            Assert.That(cancel.Attributes["href"].Value, Is.EqualTo("/teams"));
            Assert.That(cancel.Attributes["rel"].Value, Is.EqualTo("cancel"));
            Assert.That(cancel.InnerText, Is.EqualTo("Wot"));
        }

        [Test]
        public void When_creating_submit_cancel_with_no_text_Then_works()
        {
            Control.TranslationDelegate = s => s + "Wot";

            var result = html.SubmitCancel("/teams");
            string actual = result.ToString().Replace("><", ">><<"); // Small hack to split easily
            var parts = actual.Split(new string[] { "><" }, StringSplitOptions.RemoveEmptyEntries);
            Assert.That(parts, Has.Length.EqualTo(2));

            var submit = this.GetHtmlNode(parts[0]);
            Assert.That(submit.Name, Is.EqualTo("input"));
            Assert.That(submit.Attributes["value"].Value, Is.EqualTo("SaveWot"));
            Assert.That(submit.Attributes["type"].Value, Is.EqualTo("submit"));

            var cancel = this.GetHtmlNode(parts[1]);
            Assert.That(cancel.Name, Is.EqualTo("a"));
            Assert.That(cancel.Attributes["href"].Value, Is.EqualTo("/teams"));
            Assert.That(cancel.Attributes["rel"].Value, Is.EqualTo("cancel"));
            Assert.That(cancel.InnerText, Is.EqualTo("CancelWot"));

            Control.TranslationDelegate = s => s;
        }
    }
}
