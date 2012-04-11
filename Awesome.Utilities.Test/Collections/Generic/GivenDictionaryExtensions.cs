using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Awesome.Utilities.Test.Collections.Generic
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenDictionaryExtensions
    {
        [Test]
        public void When_making_dictionary_into_namevaluecollection_Then_works()
        {
            var dict = new Dictionary<int, bool>()
            {
                { 1, true },
                { 2, false }
            };

            var nvc = dict.ToNameValueCollection(key => (key + 1).ToString(), value => (!value).ToString());

            Assert.That(nvc.Keys, Has.Count.EqualTo(2));
            Assert.That(nvc["2"], Is.EqualTo("False"));
            Assert.That(nvc["3"], Is.EqualTo("True"));
        }
    }
}
