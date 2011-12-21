using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Globalization.Countries
{
    /// <summary>
    ///     A country on planet Earth.
    /// </summary>
    public abstract class Country
    {
        /// <summary>
        /// Gets the two letter code. http://en.wikipedia.org/wiki/ISO_3166-1_alpha-2
        /// </summary>
        public abstract string TwoLetterCode { get; }

        /// <summary>
        /// Gets the three letter code. http://en.wikipedia.org/wiki/ISO_3166-1_alpha-3
        /// </summary>
        public abstract string ThreeLetterCode { get; }

        /// <summary>
        /// Gets the English name.
        /// </summary>
        public abstract string EnglishName { get; }

        /// <summary>
        /// Gets the name of the country in it's native language
        /// </summary>
        public abstract string NativeName { get; }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <param name="cultureInfo">The culture info. Should use current UI culture if not specified</param>
        /// <returns></returns>
        public string GetDisplayName(CultureInfo cultureInfo = null)
        {
            return Properties.Languages.ResourceManager.GetString(this.TwoLetterCode, cultureInfo ?? CultureInfo.CurrentUICulture);
        }
    }
}
