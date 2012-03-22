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
    }
}
