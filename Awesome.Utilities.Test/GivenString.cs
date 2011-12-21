using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Awesome.Utilities.Test
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenString
    {
        [TestCase("LolWut", "Lol", 3, null, true)]
        [TestCase("What is going on?", "What is going o", 15, null, true)]
        [TestCase("What is going on?", "What is g+++", 12, "+++", true)]
        [TestCase("What is going on?", "What is going on?", 20, null, true)]
        [TestCase("Hahaha, you suck.", "Hahaha,", 8, null, true)]
        [TestCase("Hahaha, you suck.", "Hahaha, ", 8, null, false)]
        public void When_truncating_Then_works(string toTruncate, string expected, int length, string suffix, bool trim)
        {
            string actual = toTruncate.Truncate(length, suffix, trim);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase("0123456789", "0123", 0, 3)]
        [TestCase("0123456789", "456", 4, 6)]
        public void When_substringing_start_end_Then_works(string toSubstring, string expected, int start, int end)
        {
            string actual = toSubstring.SubstringStartEnd(start, end);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase("Je suis une école de pensée très jolie.", "Je suis une ecole de pensee tres jolie.")]
        public void When_removing_diacritics_Then_works(string toRemove, string expected)
        {
            string actual = toRemove.RemoveDiacritics();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase("I am an <b>HTML</b> result.", "I am an HTML result.")]
        [TestCase("I am an <b>HTML result.", "I am an HTML result.")]
        public void When_removing_html_Then_works(string toRemove, string expected)
        {
            string actual = toRemove.RemoveHtml();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase("I am a super special slug.", "i-am-a-super-special-slug")]
        [TestCase("Je suis une slug géniale!!!!", "je-suis-une-slug-geniale")]
        public void When_transforming_to_slug_Then_works(string toSlug, string expected)
        {
            string actual = toSlug.ToSlug(25);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
