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

        [TestCase("Montréal, QC, Canada", -73.554398d, 45.512291d)]
        public void When_getting_coordinates_with_natural_feature_result_Then_works(string address, double longitude, double latitude)
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

        [Test]
        public void When_getting_info_Then_works()
        {
            var info = this.geo.GetAddressInformation("304 Rockland, Ville Mont-Royal, QC, CAN");

            Assert.That(info, Is.Not.Null);
            Assert.That(info.FormattedAddress, Is.EqualTo("304 Rockland, Ville Mont-Royal, QC, CAN"));
            Assert.That(info.Coordinates, Is.EqualTo(new Coordinates(-73.61154, 45.51418)));
            Assert.That(info.Components, Has.Length.EqualTo(6));
            Assert.That(info.Components[0], Is.EqualTo(new AddressInformationComponent("[500-600] Avenue Rockland", "[500-600] Avenue Rockland", new string[] { "street_number" })));
            Assert.That(info.Components[1], Is.EqualTo(new AddressInformationComponent("H2V 2Z3", "H2V 2Z3", new string[] { "postal_code" })));
            Assert.That(info.Components[2], Is.EqualTo(new AddressInformationComponent("Mont Royal", "Mont Royal", new string[] { "city" })));
            Assert.That(info.Components[3], Is.EqualTo(new AddressInformationComponent("", "", new string[] { "county" })));
            Assert.That(info.Components[4], Is.EqualTo(new AddressInformationComponent("QC", "QC", new string[] { "state" })));
            Assert.That(info.Components[5], Is.EqualTo(new AddressInformationComponent("CA", "CA", new string[] { "country" })));
        }
    }
}
