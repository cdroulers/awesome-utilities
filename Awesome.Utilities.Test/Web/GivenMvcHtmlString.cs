using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Web.Mvc;

namespace Awesome.Utilities.Test.Web
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenMvcHtmlString
    {
        [Test]
        public void When_adding_Then_concatenates()
        {
            var s = MvcHtmlString.Create("<p>lol</p>");

            var actual = s.Add(MvcHtmlString.Create("<span>wat</span>"));

            Assert.That(actual.ToString(), Is.EqualTo("<p>lol</p><span>wat</span>"));
        }

        [Test]
        public void When_adding_with_space_Then_concatenates_with_space()
        {
            var s = MvcHtmlString.Create("<p>lol</p>");

            var actual = s.Add(MvcHtmlString.Create("<span>wat</span>"), addSpace: true);

            Assert.That(actual.ToString(), Is.EqualTo("<p>lol</p> <span>wat</span>"));
        }

        [Test]
        public void When_adding_string_Then_concatenates()
        {
            var s = MvcHtmlString.Create("<p>lol</p>");

            var actual = s.Add("<span>wat</span>");

            Assert.That(actual.ToString(), Is.EqualTo("<p>lol</p><span>wat</span>"));
        }

        [Test]
        public void When_adding_string_with_space_Then_concatenates_with_space()
        {
            var s = MvcHtmlString.Create("<p>lol</p>");

            var actual = s.Add("<span>wat</span>", addSpace: true);

            Assert.That(actual.ToString(), Is.EqualTo("<p>lol</p> <span>wat</span>"));
        }
    }
}
