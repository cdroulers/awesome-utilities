using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc.UI;
using NUnit.Framework;
using WebUI = System.Web.UI;

namespace Awesome.Utilities.Test.Web.Mvc.UI
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenLiteralControl : GivenHtmlBase
    {
        [Test]
        public void When_creating_literal_control_Then_renders_properly()
        {
            var control = new LiteralControl(WebUI.HtmlTextWriterTag.Input, isSelfClosing: true) { ID = "id", CssClass = "css-class", Title = "title" }.AddAttribute("type", "submit");

            string actual = control.ToString();
            var element = this.GetHtmlNode(actual);

            Assert.That(element.Name, Is.EqualTo("input"));
            Assert.That(string.IsNullOrEmpty(element.InnerText), Is.True, "Must be null or empty");
            Assert.That(element.Attributes["id"].Value, Is.EqualTo("id"));
            Assert.That(element.Attributes["class"].Value, Is.EqualTo("css-class"));
            Assert.That(element.Attributes["title"].Value, Is.EqualTo("title"));
            Assert.That(element.Attributes["type"].Value, Is.EqualTo("submit"));
        }

        [Test]
        public void When_creating_literal_control_and_remove_attribute_Then_renders_properly()
        {
            var control = new LiteralControl(WebUI.HtmlTextWriterTag.Input, isSelfClosing: true) { ID = "id", CssClass = "css-class", Title = "title" }.AddAttribute("type", "submit").RemoveAttribute("title");

            string actual = control.ToString();
            var element = this.GetHtmlNode(actual);

            Assert.That(element.Name, Is.EqualTo("input"));
            Assert.That(string.IsNullOrEmpty(element.InnerText), Is.True, "Must be null or empty");
            Assert.That(element.Attributes["id"].Value, Is.EqualTo("id"));
            Assert.That(element.Attributes["class"].Value, Is.EqualTo("css-class"));
            Assert.That(element.Attributes["title"], Is.Null);
            Assert.That(element.Attributes["type"].Value, Is.EqualTo("submit"));
        }

        [Test]
        public void When_creating_literal_control_and_not_indenting_Then_renders_properly()
        {
            var control = new LiteralControl(WebUI.HtmlTextWriterTag.Span) { Content = "wot", IsIndented = false };

            string actual = control.ToString();
            Assert.That(actual, Is.EqualTo("<span>wot</span>"));
        }
    }
}
