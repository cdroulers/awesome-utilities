using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Collections.Specialized;

namespace Awesome.Utilities.Test.Collections.Specialized
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenNameValueCollectionExtensions
    {
        [Test]
        public void When_creating_query_string_from_namevaluecollection_Then_builds_properly()
        {
            var nvc = new NameValueCollection();
            nvc["One"] = "Lol";
            nvc["Two"] = "Wat";
            nvc["Three"] = "Wot";

            var qs = nvc.ToQueryString();
            Assert.That(qs, Is.EqualTo("One=Lol&Two=Wat&Three=Wot"));
            qs = nvc.ToQueryString(prependQueryStringDelimiter: true);
            Assert.That(qs, Is.EqualTo("?One=Lol&Two=Wat&Three=Wot"));
        }
    }
}
