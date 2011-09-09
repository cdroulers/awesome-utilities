using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using NUnit.Framework;
using System.Web.Mvc.UI;
using System.Diagnostics;

namespace Awesome.Utilities.Test.Web.Mvc.UI
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenControl : GivenHtmlBase
    {
        [TestCase("Name[en]", "Name_en_")]
        [TestCase("Name:wot", "Name:wot")]
        [TestCase("Name:Wot.What_/", "Name:Wot.What__")]
        public void When_creating_safe_ID_Then_proper_characters_are_escaped(string s, string expected)
        {
            var actual = Control.CreateSafeID(s);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void When_rendering_A_Then_has_proper_attributes()
        {
            var control = new A("Text", "http://example.org/") { ID = "id", CssClass = "css-class", Title = "title", Target = "_blank" };

            string actual = control.ToString();
            var element = this.GetHtmlNode(actual);

            Assert.That(element.Name, Is.EqualTo("a"));
            Assert.That(element.InnerText, Is.EqualTo("Text"));
            Assert.That(element.Attributes["id"].Value, Is.EqualTo("id"));
            Assert.That(element.Attributes["href"].Value, Is.EqualTo("http://example.org/"));
            Assert.That(element.Attributes["class"].Value, Is.EqualTo("css-class"));
            Assert.That(element.Attributes["title"].Value, Is.EqualTo("title"));
            Assert.That(element.Attributes["target"].Value, Is.EqualTo("_blank"));
        }

        [Test]
        public void When_rendering_CssLink_Then_has_proper_attributes()
        {
            var control = new CssLink("http://example.org/site.css", MediaValue.Screen) { };

            string actual = control.ToString();
            var element = this.GetHtmlNode(actual);

            Assert.That(element.Name, Is.EqualTo("link"));
            Assert.That(element.InnerText, Is.Empty);
            Assert.That(element.Attributes["type"].Value, Is.EqualTo("text/css"));
            Assert.That(element.Attributes["href"].Value, Is.EqualTo("http://example.org/site.css"));
            Assert.That(element.Attributes["rel"].Value, Is.EqualTo("stylesheet"));
            Assert.That(element.Attributes["media"].Value, Is.EqualTo("screen"));
        }

        [Test]
        public void When_rendering_RssLink_Then_has_proper_attributes()
        {
            var control = new RssLink("http://example.org/site.rss", "RSS for the children!") { };

            string actual = control.ToString();
            var element = this.GetHtmlNode(actual);

            Assert.That(element.Name, Is.EqualTo("link"));
            Assert.That(element.InnerText, Is.Empty);
            Assert.That(element.Attributes["type"].Value, Is.EqualTo("application/rss+xml"));
            Assert.That(element.Attributes["href"].Value, Is.EqualTo("http://example.org/site.rss"));
            Assert.That(element.Attributes["title"].Value, Is.EqualTo("RSS for the children!"));
            Assert.That(element.Attributes["rel"].Value, Is.EqualTo("alternate"));
        }

        [Test]
        public void When_rendering_Javascript_Then_has_proper_attributes()
        {
            var control = new Javascript("http://example.org/site.js") { };

            string actual = control.ToString();
            var element = this.GetHtmlNode(actual);

            Assert.That(element.Name, Is.EqualTo("script"));
            Assert.That(element.InnerText, Is.Empty);
            Assert.That(element.Attributes["type"].Value, Is.EqualTo("text/javascript"));
            Assert.That(element.Attributes["src"].Value, Is.EqualTo("http://example.org/site.js"));
        }

        [Test]
        public void When_rendering_Javascript_with_content_Then_has_proper_attributes()
        {
            var control = Javascript.WithContent("alert('lol');");

            string actual = control.ToString();
            var element = this.GetHtmlNode(actual);

            Assert.That(element.Name, Is.EqualTo("script"));
            Assert.That(element.InnerText.Trim(), Is.EqualTo("alert('lol');"));
            Assert.That(element.Attributes["type"].Value, Is.EqualTo("text/javascript"));
            Assert.That(element.Attributes["src"], Is.Null);
        }

        [Test]
        public void When_rendering_CssStyle_Then_has_proper_attributes()
        {
            var control = new CssStyle("body { color: red; }");

            string actual = control.ToString();
            var element = this.GetHtmlNode(actual);

            Assert.That(element.Name, Is.EqualTo("style"));
            Assert.That(element.InnerText.Trim(), Is.EqualTo("body { color: red; }"));
            Assert.That(element.Attributes["type"].Value, Is.EqualTo("text/css"));
            Assert.That(element.Attributes["src"], Is.Null);
        }

        [Test]
        public void When_rendering_UnorderedList_Then_is_built_properly()
        {
            var dates = new DateTime[] { new DateTime(2011, 09, 01), new DateTime(2011, 09, 02), new DateTime(2011, 09, 03) };
            var control = new UnorderedList<DateTime>(dates, d => d.ToString("yyyy-MM-dd"), d => "class-" + d.Day);

            string actual = control.ToString();
            var element = this.GetHtmlNode(actual);

            var nodes = element.ChildNodes.Where(c => c.NodeType == HtmlNodeType.Element).ToList();

            Assert.That(element.Name, Is.EqualTo("ul"));
            for (int i = 0; i < dates.Length; i++)
            {
                var node = nodes[i];
                Assert.That(node.Name, Is.EqualTo("li"));
                Assert.That(node.Attributes["class"].Value, Is.EqualTo("class-" + dates[i].Day));
                Assert.That(node.InnerText.Trim(), Is.EqualTo(dates[i].ToString("yyyy-MM-dd")));
            }
        }
    }
}
