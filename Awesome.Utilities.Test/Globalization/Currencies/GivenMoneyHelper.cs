using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Globalization.Currencies;
using System.Globalization;

namespace Awesome.Utilities.Test.Globalization.Currencies
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenMoneyHelper
    {
        [TestCase("fr-CA", 5.0, 0, "5", 1, "00", 2, " $")]
        [TestCase("fr-CA", 123456789.123, 0, "123 456 789", 1, "12", 2, " $")] // The spaces there are special spaces! Don't change them!
        [TestCase("en-US", 5.0, 1, "5", 2, "00", 0, "$")]
        [TestCase("en-US", 123456789.123, 1, "123,456,789", 2, "12", 0, "$")]
        [TestCase("en-GB", 5.0, 1, "5", 2, "00", 0, "£")]
        [TestCase("en-GB", 123456789.123, 1, "123,456,789", 2, "12", 0, "£")]
        [TestCase("de-DE", 5.0, 0, "5", 1, "00", 2, " €")]
        [TestCase("de-DE", 123456789.123, 0, "123.456.789", 1, "12", 2, " €")]
        public void When_splitting_Then_returns_proper_tokens(string culture, decimal amount, int amountIndex, string amountValue, int decimalsIndex, string decimalsValue, int symbolIndex, string symbolValue)
        {
            var actual = CurrencyHelper.Split(amount, culture: new CultureInfo(culture));

            Assert.That(actual.Length, Is.EqualTo(3));
            Assert.That(actual[amountIndex], Is.EqualTo(new KeyValuePair<CurrencyPart, string>(CurrencyPart.Amount, amountValue)));
            Assert.That(actual[decimalsIndex], Is.EqualTo(new KeyValuePair<CurrencyPart, string>(CurrencyPart.Decimals, decimalsValue)));
            Assert.That(actual[symbolIndex], Is.EqualTo(new KeyValuePair<CurrencyPart, string>(CurrencyPart.Symbol, symbolValue)));
        }
    }
}
