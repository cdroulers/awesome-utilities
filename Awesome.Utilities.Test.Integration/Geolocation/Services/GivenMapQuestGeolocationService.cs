using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Geolocation.Services;
using System.Geolocation;

namespace Awesome.Utilities.Test.Integration.Geolocation.Services
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenMapQuestGeolocationService
    {
        private IGeolocationService geo;

        [SetUp]
        public void SetUp()
        {
            this.geo = new MapQuestGeolocationService("Fmjtd%7Cluu22hu8nl%2Cb5%3Do5-h0and"); // This is a testing key.
        }

        [TestCase("Saint-Anicet, QC, Canada", -74.361168d, 45.139359d)]
        [TestCase("Boston, MA, US", -71.060303d, 42.358299d)]
        public void When_getting_coordinates_Then_works(string address, double longitude, double latitude)
        {
            var actual = this.geo.GetCoordinates(address);

            Assert.That(actual, Is.EqualTo(new Coordinates(longitude, latitude)));
        }

        [Test]
        public void When_getting_coordinates_that_arent_precise_enough_Then_throws()
        {
            Assert.Throws<MultipleCoordinatesException>(() => this.geo.GetCoordinates("London")); // There are like 10 different locations for that
        }

        [Test]
        public void When_getting_coordinates_that_have_no_results_Then_throws()
        {
            Assert.Throws<AddressNotFoundException>(() => this.geo.GetCoordinates(""));
        }
    }
}
