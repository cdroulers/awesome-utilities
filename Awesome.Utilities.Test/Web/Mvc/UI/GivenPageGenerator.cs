using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc.UI;
using NUnit.Framework;
using System.Web.Mvc;
using HtmlAgilityPack;

namespace Awesome.Utilities.Test.Web.Mvc.UI
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenPageGenerator : GivenHtmlBase
    {
        [TestCase(1, 3, 9, null, 2, 3)]
        [TestCase(2, 3, 9, 1, 3, 3)]
        [TestCase(3, 3, 9, 2, null, 3)]
        [TestCase(2, 3, 12, 1, 3, 4)]
        public void When_rendering_page_generator_Then_builds_the_right_pages(int currentPage, int perPage, int total, int? previous, int? next, int last)
        {
            var temp = new List<string>() { "one", "two", "three" };
            var results = new ResultPage<string>(temp, currentPage, perPage, total);
            var control = new PageGenerator<string>(results, p => new A(p.Text, "http://wot.com/?page=" + p.Page).ToHtmlString()) { CssClass = "loller-skates" };

            string actual = control.ToString();
            var element = this.GetHtmlNode(actual);

            Assert.That(element.Name, Is.EqualTo("span"));
            Assert.That(element.Attributes["class"].Value, Is.EqualTo("loller-skates pager"));

            var nodes = element.ChildNodes.Where(c => c.NodeType != HtmlNodeType.Text).ToList();

            HtmlNode previousPageNode = null;
            HtmlNode nextPageNode = null;
            if (currentPage > ResultPage<string>.ValueOfFirstPage)
            {
                previousPageNode = nodes[0];
                nodes.RemoveAt(0);
                Assert.That(previousPageNode.InnerText, Is.EqualTo("PreviousPage"));
            }
            if (currentPage < results.LastPage)
            {
                nextPageNode = nodes[nodes.Count - 1];
                nodes.RemoveAt(nodes.Count - 1);
                Assert.That(nextPageNode.InnerText, Is.EqualTo("NextPage"));
            }

            Assert.That(nodes.Count, Is.EqualTo(results.LastPage));
            for (int i = 0; i < results.LastPage; i++)
            {
                var node = nodes[i];
                int value = i + 1;
                Assert.That(node.InnerText, Is.EqualTo("[" + value + "]"));
                if (value == currentPage)
                {
                    Assert.That(node.Name, Is.EqualTo("span"));
                    Assert.That(node.Attributes["class"].Value, Is.EqualTo("current-page"));
                }
                else
                {
                    Assert.That(node.Name, Is.EqualTo("a"));
                    Assert.That(node.Attributes["href"].Value, Is.EqualTo("http://wot.com/?page=" + value));
                }
            }
        }

        [Test]
        public void When_rendering_page_generator_Then_first_and_last_page_render_correctly()
        {
            int maxPerPage = 3;

            var temp = new List<string>() { "one", "two", "three" };
            var results = new ResultPage<string>(temp, 3, 3, 15);
            var control = new PageGenerator<string>(results, p => new A(p.Text, "http://wot.com/?page=" + p.Page).ToHtmlString(), maxPerPage);

            string actual = control.ToString();
            var element = this.GetHtmlNode(actual);

            Assert.That(element.Name, Is.EqualTo("span"));
            Assert.That(element.Attributes["class"].Value, Is.EqualTo("pager"));

            var nodes = element.ChildNodes.Where(c => c.NodeType != HtmlNodeType.Text).ToList();

            HtmlNode firstPageNode = nodes[0];
            nodes.RemoveAt(0);
            Assert.That(firstPageNode.Name, Is.EqualTo("a"));
            Assert.That(firstPageNode.InnerText, Is.EqualTo("FirstPage"));

            HtmlNode previousPageNode = nodes[0];
            nodes.RemoveAt(0);
            Assert.That(previousPageNode.Name, Is.EqualTo("a"));
            Assert.That(previousPageNode.InnerText, Is.EqualTo("PreviousPage"));

            HtmlNode firstPageSeparatorNode = nodes[0];
            nodes.RemoveAt(0);
            Assert.That(firstPageSeparatorNode.Name, Is.EqualTo("span"));
            Assert.That(firstPageSeparatorNode.InnerText, Is.EqualTo("..."));
            Assert.That(firstPageSeparatorNode.Attributes["class"].Value, Is.EqualTo("first-page-separator"));

            HtmlNode lastPageNode = nodes[nodes.Count - 1];
            nodes.RemoveAt(nodes.Count - 1);
            Assert.That(lastPageNode.Name, Is.EqualTo("a"));
            Assert.That(lastPageNode.InnerText, Is.EqualTo("LastPage"));

            HtmlNode nextPageNode = nodes[nodes.Count - 1];
            nodes.RemoveAt(nodes.Count - 1);
            Assert.That(nextPageNode.Name, Is.EqualTo("a"));
            Assert.That(nextPageNode.InnerText, Is.EqualTo("NextPage"));

            HtmlNode lastPageSeparatorNode = nodes[nodes.Count - 1];
            nodes.RemoveAt(nodes.Count - 1);
            Assert.That(lastPageSeparatorNode.Name, Is.EqualTo("span"));
            Assert.That(lastPageSeparatorNode.InnerText, Is.EqualTo("..."));
            Assert.That(lastPageSeparatorNode.Attributes["class"].Value, Is.EqualTo("last-page-separator"));

            Assert.That(nodes.Count, Is.EqualTo(maxPerPage));
            for (int i = 0; i < maxPerPage; i++)
            {
                var node = nodes[i];
                int value = i + 2;
                Assert.That(node.InnerText, Is.EqualTo("[" + value + "]"));
                if (value == results.CurrentPage)
                {
                    Assert.That(node.Name, Is.EqualTo("span"));
                    Assert.That(node.Attributes["class"].Value, Is.EqualTo("current-page"));
                }
                else
                {
                    Assert.That(node.Name, Is.EqualTo("a"));
                    Assert.That(node.Attributes["href"].Value, Is.EqualTo("http://wot.com/?page=" + value));
                }
            }
        }
    }
}
