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

        [TestCase("hi-IN", 123456789.123, 1, "12,34,56,789", 2, "12", 0, "₹ ")]
        [TestCase("gu-IN", 123456789.123, 1, "12,34,56,789", 2, "12", 0, "₹ ")]
        [TestCase("kn-IN", 123456789.123, 1, "12,34,56,789", 2, "12", 0, "₹ ")]
        [TestCase("kok-IN", 123456789.123, 1, "12,34,56,789", 2, "12", 0, "₹ ")]
        [TestCase("mr-IN", 123456789.123, 1, "12,34,56,789", 2, "12", 0, "₹ ")]
        [TestCase("ta-IN", 123456789.123, 1, "12,34,56,789", 2, "12", 0, "₹ ")]
        [TestCase("te-IN", 123456789.123, 1, "12,34,56,789", 2, "12", 0, "₹ ")]
        [TestCase("hi", 123456789.123, 1, "12,34,56,789", 2, "12", 0, "₹ ")]
        [TestCase("hi-IN", 0, 1, "0", 2, "00", 0, "₹ ")]
        [TestCase("gu-IN", 0, 1, "0", 2, "00", 0, "₹ ")]
        [TestCase("kn-IN", 0, 1, "0", 2, "00", 0, "₹ ")]
        [TestCase("kok-IN", 0, 1, "0", 2, "00", 0, "₹ ")]
        [TestCase("mr-IN", 0, 1, "0", 2, "00", 0, "₹ ")]
        [TestCase("ta-IN", 0, 1, "0", 2, "00", 0, "₹ ")]
        [TestCase("te-IN", 0, 1, "0", 2, "00", 0, "₹ ")]
        [TestCase("hi", 0, 1, "0", 2, "00", 0, "₹ ")]

        [TestCase("is", 123456789.123, 0, "123.456.789", 1, "", 2, " ISK")]
        public void When_splitting_Then_returns_proper_tokens(string culture, decimal amount, int amountIndex, string amountValue, int decimalsIndex, string decimalsValue, int symbolIndex, string symbolValue)
        {
            var actual = CurrencyHelper.Split(amount, culture: new CultureInfo(culture));

            Assert.That(actual.Length, Is.EqualTo(3));
            Assert.That(actual[amountIndex], Is.EqualTo(new KeyValuePair<CurrencyPart, string>(CurrencyPart.Amount, amountValue)));
            Assert.That(actual[decimalsIndex], Is.EqualTo(new KeyValuePair<CurrencyPart, string>(CurrencyPart.Decimals, decimalsValue)));
            Assert.That(actual[symbolIndex], Is.EqualTo(new KeyValuePair<CurrencyPart, string>(CurrencyPart.Symbol, symbolValue)));
        }

        [Test]
        [Ignore("Mono fails here...")]
        public void When_splitting_with_all_cultures_Then_doesnt_throw()
        {
            foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                try
                {
                    var result = CurrencyHelper.Split(123.23M, culture);
                    Console.WriteLine("{0} => ({4}) {1} {2} {3}", culture.Name, result[0], result[1], result[2], (123.3M).ToString("C", culture));
                }
                catch (Exception e)
                {
                    Assert.Fail("Culture failure! " + culture.Name + " " + e.Message);
                }
            }
        }
    }
}
