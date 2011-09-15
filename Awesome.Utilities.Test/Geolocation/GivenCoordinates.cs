﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Geolocation;

namespace Awesome.Utilities.Test.Geolocation
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenCoordinates
    {
        [TestCase(-74.361168d, 45.139359d, -71.060303d, 42.358299d, 407.26903212238057d)] // Saint-Anicet, QC, Canada to Boston, MA, US
        public void When_calculating_distance_between_Then_works(double firstLong, double firstLat, double secondLong, double secondLat, double expected)
        {
            var first = new Coordinates(firstLong, firstLat);
            var second = new Coordinates(secondLong, secondLat);

            var distance = first.DistanceBetween(second);

            Assert.That(distance, Is.EqualTo(expected));
        }

        [TestCase(-74.361168d, 45.139359d, 25d, -74.6799023942368d, 44.914528598520313d, -74.042433605763208d, 45.364189401479685d)]
        public void When_getting_bounding_box_Then_works(double longitude, double latitude, double radius, double minLong, double minLat, double maxLong, double maxLat)
        {
            var coordinates = new Coordinates(longitude, latitude);

            var actual = coordinates.BoundingBox(radius);

            Assert.That(actual.Item1, Is.EqualTo(new Coordinates(minLong, minLat)));
            Assert.That(actual.Item2, Is.EqualTo(new Coordinates(maxLong, maxLat)));
        }
    }
}
