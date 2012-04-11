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

        [Test]
        public void When_getting_value_or_default_with_value_Then_works()
        {
            var dict = new Dictionary<int, bool>()
            {
                { 1, true },
                { 2, false }
            };

            var value = dict.GetValueOrDefault(1);
            Assert.That(value, Is.EqualTo(true));
        }

        [Test]
        public void When_getting_value_or_default_without_value_Then_returns_default_T()
        {
            var dict = new Dictionary<int, bool>()
            {
                { 1, true },
                { 2, false }
            };

            var value = dict.GetValueOrDefault(3);
            Assert.That(value, Is.EqualTo(false));
        }

        [Test]
        public void When_getting_value_or_default_without_value_Then_returns_specified_default()
        {
            var dict = new Dictionary<int, bool>()
            {
                { 1, true },
                { 2, false }
            };

            var value = dict.GetValueOrDefault(3, true);
            Assert.That(value, Is.EqualTo(true));
        }
    }
}
