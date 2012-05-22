using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Geolocation.Services;
using System.Geolocation;
using System.Threading;

namespace Awesome.Utilities.Test.Integration.Geolocation.Services
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenYahooMapsGeolocationService
    {
        private IGeolocationService geo;

        [SetUp]
        public void SetUp()
        {
            Thread.Sleep(250); // Yahoo has a rate limit per second. So slow down the tests.
            this.geo = new YahooMapsGeolocationService();
        }

        [TestCase("Saint-Anicet, QC, Canada", -74.361169d, 45.139360d)]
        [TestCase("Boston, MA, US", -71.056699d, 42.358635d)]
        [TestCase("Boisbriand, QC, Canada", -73.838882d, 45.61204d)]
        [TestCase("Montréal, QC, Canada", -73.554431d, 45.512303d)]
        [TestCase("5 Patti Page Way # B, Centerville Ma 02632, MA 02632-1434, USA", -70.346874d, 41.649885d)]
        public void When_getting_coordinates_Then_works(string address, double longitude, double latitude)
        {
            var actual = this.geo.GetCoordinates(address);

            ComparisonHelper.CompareCoordinates(actual, new Coordinates(longitude, latitude), 0.1);
        }

        [Test]
        public void When_getting_coordinates_that_arent_precise_enough_Then_throws()
        {
            var results = this.geo.GetAllAddressInformation("London");
            Assert.That(results, Has.Length.EqualTo(1)); // Yahoo only returns one result ever.
        }

        [TestCase("")]
        [TestCase("1870aec8-dded-4c28-a53c-40a24df51eb4")]
        public void When_getting_coordinates_that_have_no_results_Then_throws(string address)
        {
            Assert.Throws<AddressNotFoundException>(() => this.geo.GetCoordinates(address));
        }

        [Test]
        public void When_getting_all_information_Then_returns_multiple_results()
        {
            var results = this.geo.GetAllAddressInformation("Boston");
            Assert.That(results, Has.Length.EqualTo(1)); // Yahoo only returns one result ever.
        }

        [Test]
        public void When_getting_info_Then_works()
        {
            var info = this.geo.GetAddressInformation("304 Rockland, Ville Mont-Royal, QC, CA");

            Assert.That(info, Is.Not.Null);
            Assert.That(info.Type, Is.EqualTo("street_address"));
            Assert.That(info.FormattedAddress, Is.EqualTo("304 Rockland Rd, Mt Royal, QC  H3P, Canada").Or.EqualTo("304 Rockland Rd, Montreal, QC  H3P, Canada"));
            ComparisonHelper.CompareCoordinates(info.Coordinates, new Coordinates(-73.628517, 45.520726), 0.01);
            Assert.That(info.Components, Has.Length.EqualTo(7));
            Assert.That(info.Components[0], Is.EqualTo(new AddressInformationComponent("304 Rockland Rd", "304 Rockland Rd", new string[] { "street_address" })));
            Assert.That(info.Components[1], Is.EqualTo(new AddressInformationComponent("H3P", "H3P", new string[] { "postal_code" })));
            Assert.That(info.Components[3], Is.EqualTo(new AddressInformationComponent("Mt Royal", "Mt Royal", new string[] { "locality" })).Or.EqualTo(new AddressInformationComponent("Mont-Royal", "Mont-Royal", new string[] { "locality" })));
            Assert.That(info.Components[4], Is.EqualTo(new AddressInformationComponent("Montreal", "", new string[] { "administrative_area_level_2" })).Or.EqualTo(new AddressInformationComponent("Montréal", "", new string[] { "administrative_area_level_2" })));
            Assert.That(info.Components[5], Is.EqualTo(new AddressInformationComponent("Quebec", "QC", new string[] { "administrative_area_level_1" })));
            Assert.That(info.Components[6], Is.EqualTo(new AddressInformationComponent("Canada", "CA", new string[] { "country" })));
        }
    }
}
