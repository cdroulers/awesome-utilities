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
        public static void CompareCoordinates(Coordinates actual, Coordinates expected, double within = 0)
        {
            Assert.That(actual.Longitude, Is.EqualTo(expected.Longitude).Within(within));
            Assert.That(actual.Latitude, Is.EqualTo(expected.Latitude).Within(within));
        }
    }
}
