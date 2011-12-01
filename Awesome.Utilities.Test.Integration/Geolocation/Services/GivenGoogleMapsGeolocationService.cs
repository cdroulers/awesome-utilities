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
    public class GivenGoogleMapsGeolocationService
    {
        private IGeolocationService geo;

        [SetUp]
        public void SetUp()
        {
            this.geo = new GoogleMapsGeolocationService();
        }

        [TestCase("Saint-Anicet, QC, Canada", -74.3623250d, 45.1354550d)]
        [TestCase("Boston, MA, US", -71.05977320d, 42.35843080d)]
        public void When_getting_coordinates_Then_works(string address, double longitude, double latitude)
        {
            var actual = this.geo.GetCoordinates(address);

            Assert.That(actual, Is.EqualTo(new Coordinates(longitude, latitude)));
        }

        [TestCase("Montréal, QC, Canada", -73.55399249999999, 45.50866990)]
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
            var info = this.geo.GetAddressInformation("304 Rockland, Ville Mont-Royal, QC, CA");

            Assert.That(info, Is.Not.Null);
            Assert.That(info.FormattedAddress, Is.EqualTo("304 Chemin Rockland, Mont-Royal, QC H3P 2W8, Canada"));
            Assert.That(info.Components, Has.Length.EqualTo(7));
            Assert.That(info.Components[0], Is.EqualTo(new AddressInformationComponent("304", "304", new string[] { "street_number" })));
            Assert.That(info.Components[1], Is.EqualTo(new AddressInformationComponent("Chemin Rockland", "Chemin Rockland", new string[] { "route" })));
            Assert.That(info.Components[2], Is.EqualTo(new AddressInformationComponent("Mont-Royal", "Mont-Royal", new string[] { "locality", "political" })));
            Assert.That(info.Components[3], Is.EqualTo(new AddressInformationComponent("Communauté-Urbaine-de-Montréal", "Communauté-Urbaine-de-Montréal", new string[] { "administrative_area_level_2", "political" })));
            Assert.That(info.Components[4], Is.EqualTo(new AddressInformationComponent("Quebec", "QC", new string[] { "administrative_area_level_1", "political" })));
            Assert.That(info.Components[5], Is.EqualTo(new AddressInformationComponent("Canada", "CA", new string[] { "country", "political" })));
            Assert.That(info.Components[6], Is.EqualTo(new AddressInformationComponent("H3P 2W8", "H3P 2W8", new string[] { "postal_code" })));
        }
    }
}
