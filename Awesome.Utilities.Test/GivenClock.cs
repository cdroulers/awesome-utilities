using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Threading;

namespace Awesome.Utilities.Test
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenClock
    {
        [Test]
        public void When_pausing_Then_works()
        {
            DateTime outNow = Clock.Now;
            using (Clock.Pause())
            {
                DateTime now = Clock.Now;
                DateTime utcNow = Clock.UtcNow;

                Thread.Sleep(1000);

                Assert.That(now, Is.EqualTo(Clock.Now));
                Assert.That(utcNow, Is.EqualTo(Clock.UtcNow));
            }

            Assert.That(outNow, Is.Not.EqualTo(Clock.Now));
        }


        [Test]
        public void When_transforming_Then_works()
        {
            using (Clock.Pause())
            {
                var result = Clock.UtcNow;

                Assert.That(result.Kind, Is.EqualTo(DateTimeKind.Utc));

                Clock.Transform = d => { return new DateTime(d.Ticks, DateTimeKind.Unspecified); };

                var result2 = Clock.UtcNow;

                Assert.That(result2.Kind, Is.EqualTo(DateTimeKind.Unspecified));
                Assert.That(result2, Is.EqualTo(result));

                Clock.Transform = null;
            }
        }
    }
}
