using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Geolocation;
using NUnit.Framework;

namespace Awesome.Utilities.Test.Integration
{
    public static class ComparisonHelper
    {
        public static void CompareCoordinates(Coordinates expected, Coordinates actual, double within = 0)
        {
            Assert.That(expected.Longitude, Is.EqualTo(actual.Longitude).Within(within));
            Assert.That(expected.Latitude, Is.EqualTo(actual.Latitude).Within(within));
        }
    }
}
