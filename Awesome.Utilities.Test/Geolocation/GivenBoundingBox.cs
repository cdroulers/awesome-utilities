using System;
using System.Collections.Generic;
using System.Geolocation;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Awesome.Utilities.Test.Geolocation
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenBoundingBox
    {
        //-72.147697, 45.264858
        [TestCase(-72.867697d, 44.164858d, -71.757697d, 46.184858d, -72.147697d, 45.264858d, true)]
        [TestCase(-72.867697d, 44.164858d, -71.757697d, 46.184858d, -76.147697d, 45.264858d, false)]
        [TestCase(-72.867697d, 44.164858d, -71.757697d, 46.184858d, -76.147697d, 41.264858d, false)]
        public void When_checking_within_Then_works(double firstLong, double firstLat, double secondLong, double secondLat, double withinLong, double withinLat, bool expected)
        {
            var first = new Coordinates(firstLong, firstLat);
            var second = new Coordinates(secondLong, secondLat);
            var box = new BoundingBox(first, second);

            var within = new Coordinates(withinLong, withinLat);
            var actual = box.Contains(within);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
