using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Units.Distances.Imperial;
using System.Units.Distances.Metric;
using NUnit.Framework;
using System.Units.Distances;

namespace Awesome.Utilities.Test.Units.Distances
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenDistance
    {
        [Test]
        public void When_building_Then_returns_proper_distance()
        {
            var actual = Distance.Build<Kilometers>(25);
            Assert.That(actual, Is.InstanceOf<Kilometers>());
            Assert.That(actual.Value, Is.EqualTo(25));
        }

        [Test]
        public void When_building_by_name_Then_returns_proper_distance()
        {
            var actual = Distance.BuildByName("Kilometers", 25);
            Assert.That(actual, Is.InstanceOf<Kilometers>());
            Assert.That(actual.Value, Is.EqualTo(25));

            var actual2 = Distance.BuildByName("Miles", 25);
            Assert.That(actual2, Is.InstanceOf<Miles>());
            Assert.That(actual2.Value, Is.EqualTo(25));
        }

        [Test]
        public void When_building_by_name_with_invalid_name_Then_throws_not_supported()
        {
            Assert.Throws<NotSupportedException>(() => Distance.BuildByName("LolWhat", 25));
        }

        [Test]
        public void When_equaling_Then_works()
        {
            var km = new Kilometers(1);
            var m = new Meters(1000);

            var actual = km.Equals(m);
            Assert.That(actual, Is.EqualTo(true));
        }

        [Test]
        public void When_adding_Then_works()
        {
            var km = new Kilometers(1);
            var mi = new Miles(1);

            var actual = km.Add(mi);
            Assert.That(actual.Value, Is.EqualTo(2.609344M).Within(0.000001));
        }

        [Test]
        public void When_substracting_Then_works()
        {
            var mi = new Miles(1);
            var km = new Kilometers(1);

            var actual = mi.Substract(km);
            Assert.That(actual.Value, Is.EqualTo(0.378628808).Within(0.000001));
        }

        [Test]
        public void When_multiplying_Then_works()
        {
            var km = new Kilometers(1.25M);

            var actual = km.Multiply(2);
            Assert.That(actual.Value, Is.EqualTo(2.5M));
        }

        [Test]
        public void When_dividing_Then_works()
        {
            var km = new Kilometers(5M);

            var actual = km.Divide(2);
            Assert.That(actual.Value, Is.EqualTo(2.5M));
        }

        [TestCase(12, typeof(Kilometers), 12)]
        [TestCase(1.564, typeof(Meters), 1564)]
        [TestCase(1.258, typeof(Decimeters), 12580)]
        [TestCase(1.56, typeof(Centimeters), 156000)]
        [TestCase(12, typeof(Millimeters), 12000000)]
        [TestCase(1, typeof(Miles), 0.6213711920454545)]
        [TestCase(1, typeof(Yards), 1093.613298)]
        [TestCase(1, typeof(Feet), 3280.839894)]
        [TestCase(1, typeof(Inches), 39370.078728)]
        public void When_converting_kilometers_Then_works(decimal distanceValue, Type type, decimal expected)
        {
            var distance = new Kilometers(distanceValue);
            var actual = distance.ConvertTo(type);

            Assert.That(actual.Value, Is.EqualTo(expected).Within(0.000001M));
        }

        [TestCase(12, typeof(Kilometers), .012)]
        [TestCase(1564, typeof(Meters), 1564)]
        [TestCase(12580, typeof(Decimeters), 125800)]
        [TestCase(1560, typeof(Centimeters), 156000)]
        [TestCase(12, typeof(Millimeters), 12000)]
        [TestCase(1, typeof(Miles), 0.000621371192)]
        [TestCase(1, typeof(Yards), 1.0936133)]
        [TestCase(1, typeof(Feet), 3.2808399)]
        [TestCase(1, typeof(Inches), 39.3700787)]
        public void When_converting_meters_Then_works(decimal distanceValue, Type type, decimal expected)
        {
            var distance = new Meters(distanceValue);
            var actual = distance.ConvertTo(type);

            Assert.That(actual.Value, Is.EqualTo(expected).Within(0.000001M));
        }

        [TestCase(12580, typeof(Kilometers), 1.258)]
        [TestCase(12, typeof(Meters), 1.2)]
        [TestCase(1564, typeof(Millimeters), 156400)]
        [TestCase(1565, typeof(Centimeters), 15650)]
        [TestCase(1200, typeof(Decimeters), 1200)]
        [TestCase(1, typeof(Miles), 0.0000621371192)]
        [TestCase(1, typeof(Yards), 0.10936133)]
        [TestCase(1, typeof(Feet), 0.32808399)]
        [TestCase(1, typeof(Inches), 3.93700787)]
        public void When_converting_decimeters_Then_works(decimal distanceValue, Type type, decimal expected)
        {
            var distance = new Decimeters(distanceValue);
            var actual = distance.ConvertTo(type);

            Assert.That(actual.Value, Is.EqualTo(expected).Within(0.000001M));
        }

        [TestCase(12, typeof(Kilometers), 0.00012)]
        [TestCase(12, typeof(Meters), 0.12)]
        [TestCase(156, typeof(Millimeters), 1560)]
        [TestCase(1565, typeof(Centimeters), 1565)]
        [TestCase(1200, typeof(Decimeters), 120)]
        [TestCase(1000, typeof(Miles), 0.00621371192)]
        [TestCase(1000, typeof(Yards), 10.936133)]
        [TestCase(1000, typeof(Feet), 32.808399)]
        [TestCase(1000, typeof(Inches), 393.700787)]
        public void When_converting_centimeters_Then_works(decimal distanceValue, Type type, decimal expected)
        {
            var distance = new Centimeters(distanceValue);
            var actual = distance.ConvertTo(type);

            Assert.That(actual.Value, Is.EqualTo(expected).Within(0.000001M));
        }

        [TestCase(12, typeof(Kilometers), 0.000012)]
        [TestCase(1564123, typeof(Meters), 1564.123)]
        [TestCase(1, typeof(Millimeters), 1)]
        [TestCase(1565, typeof(Centimeters), 156.5)]
        [TestCase(1200, typeof(Decimeters), 12)]
        [TestCase(1000, typeof(Miles), 0.000621371192)]
        [TestCase(1000, typeof(Yards), 1.0936133)]
        [TestCase(1000, typeof(Feet), 3.2808399)]
        [TestCase(1000, typeof(Inches), 39.3700787)]
        public void When_converting_millimeters_Then_works(decimal distanceValue, Type type, decimal expected)
        {
            var distance = new Millimeters(distanceValue);
            var actual = distance.ConvertTo(type);

            Assert.That(actual.Value, Is.EqualTo(expected).Within(0.000001M));
        }

        [TestCase(1, typeof(Kilometers), 1.609344, 0.000001)]
        [TestCase(1, typeof(Meters), 1609.344, 0.001)]
        [TestCase(1, typeof(Decimeters), 16093.44, 0.01)]
        [TestCase(1, typeof(Centimeters), 160934.4, 0.1)]
        [TestCase(1, typeof(Millimeters), 1609344, 1)]
        [TestCase(1, typeof(Miles), 1, 1)]
        [TestCase(1, typeof(Yards), 1760, 1)]
        [TestCase(1, typeof(Feet), 5280, 1)]
        [TestCase(1, typeof(Inches), 63360, 1)]
        public void When_converting_miles_Then_works(decimal distanceValue, Type type, decimal expected, decimal within)
        {
            var distance = new Miles(distanceValue);
            var actual = distance.ConvertTo(type);

            Assert.That(actual.Value, Is.EqualTo(expected).Within(within));
        }

        [TestCase(1, typeof(Kilometers), 0.0009144, 0.000001)]
        [TestCase(1, typeof(Meters), 0.9144, 0.0001)]
        [TestCase(1, typeof(Decimeters), 9.144, 0.001)]
        [TestCase(1, typeof(Centimeters), 91.44, 0.01)]
        [TestCase(1, typeof(Millimeters), 914.4, 0.1)]
        [TestCase(1, typeof(Miles), 1 / 1760.0, 0.01)]
        [TestCase(1, typeof(Yards), 1, 1)]
        [TestCase(1, typeof(Feet), 3, 1)]
        [TestCase(1, typeof(Inches), 36, 1)]
        public void When_converting_yards_Then_works(decimal distanceValue, Type type, decimal expected, decimal within)
        {
            var distance = new Yards(distanceValue);
            var actual = distance.ConvertTo(type);

            Assert.That(actual.Value, Is.EqualTo(expected).Within(within));
        }

        [TestCase(1, typeof(Kilometers), 0.0003048, 0.000001)]
        [TestCase(1, typeof(Meters), 0.3048, 0.0001)]
        [TestCase(1, typeof(Decimeters), 3.048, 0.001)]
        [TestCase(1, typeof(Centimeters), 30.48, 0.01)]
        [TestCase(1, typeof(Millimeters), 304.8, 0.1)]
        [TestCase(1, typeof(Miles), 1 / 5280.0, 0.01)]
        [TestCase(1, typeof(Yards), 1 / 3.0, 0.01)]
        [TestCase(1, typeof(Feet), 1, 1)]
        [TestCase(1, typeof(Inches), 12, 1)]
        public void When_converting_feet_Then_works(decimal distanceValue, Type type, decimal expected, decimal within)
        {
            var distance = new Feet(distanceValue);
            var actual = distance.ConvertTo(type);

            Assert.That(actual.Value, Is.EqualTo(expected).Within(within));
        }

        [TestCase(1, typeof(Kilometers), 0.0000254, 0.000001)]
        [TestCase(1, typeof(Meters), 0.0254, 0.0001)]
        [TestCase(1, typeof(Decimeters), 0.254, 0.001)]
        [TestCase(1, typeof(Centimeters), 02.54, 0.01)]
        [TestCase(1, typeof(Millimeters), 025.4, 0.1)]
        [TestCase(1, typeof(Miles), 1 / 63360.0, 0.0001)]
        [TestCase(1, typeof(Yards), 1 / 36.0, 0.001)]
        [TestCase(1, typeof(Feet), 1 / 12-0, 0.1)]
        [TestCase(1, typeof(Inches), 1, 1)]
        public void When_converting_inches_Then_works(decimal distanceValue, Type type, decimal expected, decimal within)
        {
            var distance = new Inches(distanceValue);
            var actual = distance.ConvertTo(type);

            Assert.That(actual.Value, Is.EqualTo(expected).Within(within));
        }
    }
}
