using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            Thread.Sleep(250); // MapQuest has a rate limit per second. So slow down the tests.
            this.geo = new MapQuestGeolocationService("Fmjtd%7Cluu22hu8nl%2Cb5%3Do5-h0and"); // This is a testing key.
        }

        [TestCase("Saint-Anicet, QC, Canada", -74.362337d, 45.135493d)]
        [TestCase("Boston, MA, US", -71.056742d, 42.358894d)]
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

        [Test]
        public void When_getting_all_information_Then_returns_multiple_results()
        {
            var results = this.geo.GetAllAddressInformation("Boston");

            Assert.That(results, Has.Length.EqualTo(23));
        }

        [Test]
        public void When_getting_info_Then_works()
        {
            var infos = this.geo.GetAllAddressInformation("304 Ch Rockland, Mount-Royal, QC, Canada");

            var info = infos.First();

            Assert.That(info, Is.Not.Null);
            Assert.That(info.Type, Is.EqualTo("street_address"));
            Assert.That(info.FormattedAddress, Is.EqualTo("304 Ch Rockland, Mount-Royal, QC, Canada"));
            Assert.That(info.Coordinates, Is.EqualTo(new Coordinates(-73.628951, 45.520834)));
            Assert.That(info.Components, Has.Length.EqualTo(6));
            Assert.That(info.Components[0], Is.EqualTo(new AddressInformationComponent("304 Ch Rockland", "304 Ch Rockland", new string[] { "street_address" })));
            Assert.That(info.Components[1], Is.EqualTo(new AddressInformationComponent("H3P", "H3P", new string[] { "postal_code" })));
            Assert.That(info.Components[2], Is.EqualTo(new AddressInformationComponent("Montréal", "Montréal", new string[] { "city" })));
            Assert.That(info.Components[3], Is.EqualTo(new AddressInformationComponent("Montréal", "Montréal", new string[] { "county" })));
            Assert.That(info.Components[4], Is.EqualTo(new AddressInformationComponent("QC", "QC", new string[] { "state" })));
            Assert.That(info.Components[5], Is.EqualTo(new AddressInformationComponent("CA", "CA", new string[] { "country" })));
        }
    }
}
