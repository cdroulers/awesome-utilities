using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Globalization.Currencies
{
    /// <summary>
    ///     Helper methods for dealing with money.
    /// </summary>
    public static class CurrencyHelper
    {
        /// <summary>
        /// Splits the specified value as money.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public static KeyValuePair<CurrencyPart, string>[] Split(decimal value, CultureInfo culture = null)
        {
            culture = culture ?? CultureInfo.CurrentUICulture;
            string s = value.ToString("C", culture);
            var results = new List<KeyValuePair<CurrencyPart, string>>();

            string number = s.Replace(culture.NumberFormat.CurrencySymbol, string.Empty).Trim();
            int indexOfCurrency = s.IndexOf(culture.NumberFormat.CurrencySymbol);
            int indexOfDecimalSeparator = s.IndexOf(culture.NumberFormat.CurrencyDecimalSeparator);

            if (indexOfCurrency < indexOfDecimalSeparator)
            {
                results.Add(new KeyValuePair<CurrencyPart,string>(CurrencyPart.Symbol, s.Replace(number, string.Empty)));
                var parts = number.Split(new[] { culture.NumberFormat.CurrencyDecimalSeparator }, StringSplitOptions.RemoveEmptyEntries);
                results.Add(new KeyValuePair<CurrencyPart, string>(CurrencyPart.Amount, parts[0]));
                results.Add(new KeyValuePair<CurrencyPart, string>(CurrencyPart.Decimals, parts[1]));
            }
            else
            {
                var parts = number.Split(new[] { culture.NumberFormat.CurrencyDecimalSeparator }, StringSplitOptions.RemoveEmptyEntries);
                results.Add(new KeyValuePair<CurrencyPart, string>(CurrencyPart.Amount, parts[0]));
                results.Add(new KeyValuePair<CurrencyPart, string>(CurrencyPart.Decimals, parts[1]));
                results.Add(new KeyValuePair<CurrencyPart, string>(CurrencyPart.Symbol, s.Replace(number, string.Empty)));
            }

            return results.ToArray();
        }
    }
}
