using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Globalization.Countries
{
    /// <summary>
    ///     A country on planet Earth.
    /// </summary>
    public interface ICountry
    {
        /// <summary>
        /// Gets the two letter code. http://en.wikipedia.org/wiki/ISO_3166-1_alpha-2
        /// </summary>
        string TwoLetterCode { get; }

        /// <summary>
        /// Gets the three letter code. http://en.wikipedia.org/wiki/ISO_3166-1_alpha-3
        /// </summary>
        string ThreeLetterCode { get; }

        /// <summary>
        /// Gets the English name.
        /// </summary>
        string EnglishName { get; }

        /// <summary>
        /// Gets the name of the country in it's native language
        /// </summary>
        string NativeName { get; }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <param name="cultureInfo">The culture info. Should use current UI culture if not specified</param>
        /// <returns></returns>
        string GetDisplayName(CultureInfo cultureInfo = null);
    }
}
