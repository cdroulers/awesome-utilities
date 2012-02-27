using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using NUnit.Framework;
using System.Globalization.Countries;
using System.Globalization.Countries.Implementations;
using System.Globalization;

namespace Awesome.Utilities.Test.Globalization.Countries
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenRegionInfoCountryRepository
    {
        private ICountryRepository countries;

        [SetUp]
        public void SetUp()
        {
            this.countries = new RegionInfoCountryRepository();
        }

        [TestCase("CA", "CAN", "Canada", "ᑲᓇᑕ", "fr-CA", "Canada")]
        [TestCase("US", "USA", "United States", "United States", "fr-CA", "United States")]
        [TestCase("DE", "DEU", "Germany", "Deutschland", "fr-CA", "Germany")]
        public void When_getting_by_ID_Then_returns_the_proper_country(string two, string three, string englishName, string nativeName, string culture, string cultureName)
        {
            var country = this.countries.GetByTwoLetterCode(two);

            Assert.That(country.TwoLetterCode, Is.EqualTo(two));
            Assert.That(country.ThreeLetterCode, Is.EqualTo(three));
            Assert.That(country.EnglishName, Is.EqualTo(englishName));
            Assert.That(country.NativeName, Is.EqualTo(nativeName));
            Assert.That(country.GetDisplayName(new CultureInfo(culture)), Is.EqualTo(cultureName));
        }

        [Test]
        public void When_getting_all_Then_returns_no_duplicates()
        {
            var results = this.countries.GetAll().ToList();

            Assert.That(results, Has.Count.EqualTo(128));
        }

        [TestCase("US", 55)]
        [TestCase("CA", 13)]
        [TestCase("DE", 0)]
        public void When_getting_by_two_letter_code_Then_returns_divisions(string code, int count)
        {
            var result = this.countries.GetByTwoLetterCode(code);

            Assert.That(result.Divisions, Has.Count.EqualTo(count));
        }
    }
}
