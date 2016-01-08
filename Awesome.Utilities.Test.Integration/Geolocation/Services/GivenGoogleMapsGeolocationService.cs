using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class GivenGoogleMapsGeolocationService
    {
        private IGeolocationService geo;

        [SetUp]
        public void SetUp()
        {
            Thread.Sleep(250); // Google has a rate limit per second. So slow down the tests.
            this.geo = new GoogleMapsGeolocationService(language: new CultureInfo("fr-CA"));
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


            this.geo = new GoogleMapsGeolocationService(ignoreCloseMatches: true, language: new CultureInfo("fr-CA"));
            actual = this.geo.GetCoordinates(address);
            Assert.That(actual, Is.EqualTo(new Coordinates(longitude, latitude)));
        }

        [Test]
        public void When_getting_coordinates_that_arent_precise_enough_Then_throws()
        {
            var exc = Assert.Throws<MultipleCoordinatesException>(() => this.geo.GetCoordinates("London")); // There are like 4 different locations for that

            Assert.That(exc.Addresses, Has.Length.EqualTo(4));
        }

        [Test]
        public void When_getting_university_coordinates_Then_works()
        {
            this.geo = new GoogleMapsGeolocationService(ignoreCloseMatches: true, language: new CultureInfo("fr-CA"));

            this.geo.GetCoordinates("Florida International University - Modesto A. Maidique Campus, Miami, FL");
        }

        [Test]
        public void When_getting_coordinates_that_have_close_match_with_param_to_false_Then_throws()
        {
            var exc = Assert.Throws<MultipleCoordinatesException>(() => this.geo.GetCoordinates("London"));

            Assert.That(exc.Addresses, Has.Length.EqualTo(4));
        }

        [Test]
        public void When_getting_coordinates_that_have_close_match_with_param_to_true_Then_returns()
        {
            this.geo = new GoogleMapsGeolocationService(ignoreCloseMatches: true, language: new CultureInfo("fr-CA"));
            var actual = this.geo.GetCoordinates("Boisbriand, QC, Canada");
            Assert.That(actual, Is.EqualTo(new Coordinates(-73.83837330, 45.61263380)));
        }

        [Test]
        public void When_getting_coordinates_that_have_close_match_with_param_to_true_but_same_type_Then_throws()
        {
            this.geo = new GoogleMapsGeolocationService(ignoreCloseMatches: true, language: new CultureInfo("fr-CA"));
            var exc = Assert.Throws<MultipleCoordinatesException>(() => this.geo.GetCoordinates("London")); // There are like 4 different localities for that

            Assert.That(exc.Addresses, Has.Length.EqualTo(4));
        }

        [Test]
        public void When_getting_coordinates_that_have_no_results_Then_throws()
        {
            Assert.Throws<AddressNotFoundException>(() => this.geo.GetCoordinates(""));


            this.geo = new GoogleMapsGeolocationService(ignoreCloseMatches: true, language: new CultureInfo("fr-CA"));
            Assert.Throws<AddressNotFoundException>(() => this.geo.GetCoordinates(""));
        }

        [Test]
        public void When_getting_all_information_Then_returns_multiple_results()
        {
            var results = this.geo.GetAllAddressInformation("Boston");
            Assert.That(results, Has.Length.EqualTo(6));


            this.geo = new GoogleMapsGeolocationService(ignoreCloseMatches: true, language: new CultureInfo("fr-CA"));
            results = this.geo.GetAllAddressInformation("Boston");
            Assert.That(results, Has.Length.EqualTo(6));
        }

        [Test]
        public void When_getting_info_Then_works()
        {
            var info = this.geo.GetAddressInformation("304 Rockland, Ville Mont-Royal, QC, CA");

            Assert.That(info, Is.Not.Null);
            Assert.That(info.Type, Is.EqualTo("street_address"));
            Assert.That(info.FormattedAddress, Is.EqualTo("304 Chemin Rockland, Mont-Royal, QC H3P 2W6, Canada"));
            ComparisonHelper.CompareCoordinates(info.Coordinates, new Coordinates(-73.62904170, 45.52085170), 0.0001);
            Assert.That(info.Components, Has.Length.EqualTo(7));
            Assert.That(info.Components[0], Is.EqualTo(new AddressInformationComponent("304", "304", new string[] { "street_number" })));
            Assert.That(info.Components[1], Is.EqualTo(new AddressInformationComponent("Chemin Rockland", "Chemin Rockland", new string[] { "route" })));
            Assert.That(info.Components[2], Is.EqualTo(new AddressInformationComponent("Mont-Royal", "Mont-Royal", new string[] { "locality", "political" })));
            Assert.That(info.Components[3], Is.EqualTo(new AddressInformationComponent("Communauté-Urbaine-de-Montréal", "Communauté-Urbaine-de-Montréal", new string[] { "administrative_area_level_2", "political" })));
            Assert.That(info.Components[4], Is.EqualTo(new AddressInformationComponent("Québec", "QC", new string[] { "administrative_area_level_1", "political" })));
            Assert.That(info.Components[5], Is.EqualTo(new AddressInformationComponent("Canada", "CA", new string[] { "country", "political" })));
            Assert.That(info.Components[6], Is.EqualTo(new AddressInformationComponent("H3P 2W6", "H3P 2W6", new string[] { "postal_code" })));
        }
    }
}
