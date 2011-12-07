using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Awesome.Utilities.Test
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenUriExtensions
    {
        [TestCase("http://example.org/folder?query=string", "lol=what", "http://example.org/folder?lol=what")]
        [TestCase("http://example.org/folder?query=string", "?lol=what", "http://example.org/folder?lol=what")]
        [TestCase("http://example.org/folder", "lol=what", "http://example.org/folder?lol=what")]
        [TestCase("http://example.org/folder", "?lol=what", "http://example.org/folder?lol=what")]
        public void When_changing_query_string_Then_returns_new_uri(string baseUri, string query, string expected)
        {
            var uri = new Uri(baseUri);
            var actual = uri.ChangeQueryString(query);

            Assert.That(actual, Is.EqualTo(new Uri(expected)));
        }

        [TestCase("http://example.org/folder?query=string", "folder/lol", "http://example.org/folder/lol?query=string")]
        [TestCase("http://example.org/folder?query=string", "/folder/lol", "http://example.org/folder/lol?query=string")]
        [TestCase("http://example.org/?query=string", "folder/lol", "http://example.org/folder/lol?query=string")]
        [TestCase("http://example.org/?query=string", "/folder/lol", "http://example.org/folder/lol?query=string")]
        public void When_changing_path_Then_returns_new_uri(string baseUri, string path, string expected)
        {
            var uri = new Uri(baseUri);
            var actual = uri.ChangePath(path);

            Assert.That(actual, Is.EqualTo(new Uri(expected)));
        }
    }
}
