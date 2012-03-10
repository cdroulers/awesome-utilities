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
    }
}
