using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Globalization;

namespace Awesome.Utilities.Test.Globalization
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenCulture
    {
        [Test]
        public void When_using_as_Then_works()
        {
            Assert.That(CultureInfo.CurrentCulture, Is.Not.EqualTo(new CultureInfo("es-ES")));
            Assert.That(CultureInfo.CurrentUICulture, Is.Not.EqualTo(new CultureInfo("es-ES")));

            using (Culture.As(new CultureInfo("es-ES")))
            {
                Assert.That(CultureInfo.CurrentCulture, Is.EqualTo(new CultureInfo("es-ES")));
                Assert.That(CultureInfo.CurrentUICulture, Is.EqualTo(new CultureInfo("es-ES")));
            }

            Assert.That(CultureInfo.CurrentCulture, Is.Not.EqualTo(new CultureInfo("es-ES")));
            Assert.That(CultureInfo.CurrentUICulture, Is.Not.EqualTo(new CultureInfo("es-ES")));
        }

        [TestCase("en-CA", true, "en-CA", false)]
        [TestCase("en-us", true, "en-US", false)]
        [TestCase("EN", true, "en", false)]
        [TestCase("en_CA", true, "en-CA", false)]
        [TestCase("asdfsdf", false, "", false)]
        [TestCase("", true, "", true)]
        [TestCase(null, true, "", true)]
        [TestCase("", false, "", false)]
        [TestCase(null, false, "", false)]
        public void When_parsing_Then_works(string toParse, bool expected, string expectedName, bool allowInvariant)
        {
            CultureInfo result;
            bool actual = Culture.TryParse(toParse, out result, allowInvariant);

            Assert.That(actual, Is.EqualTo(expected));
            if (expected)
            {
                Assert.That(result.Name, Is.EqualTo(expectedName));
            }
        }
    }
}
