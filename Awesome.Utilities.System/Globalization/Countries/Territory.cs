using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace System.Globalization.Countries
{
    /// <summary>
    ///     A territory is a country division.
    /// </summary>
    public class Territory : CountryDivision
    {
        private readonly string twoLetterCode;
        private readonly string englishName;
        private readonly string nativeName;

        /// <summary>
        /// Initializes a new instance of the <see cref="Territory"/> class.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <param name="twoLetterCode">The two letter code.</param>
        /// <param name="englishName">Name of the english.</param>
        /// <param name="nativeName">Name of the native.</param>
        public Territory(Country country, string twoLetterCode, string englishName, string nativeName)
            : base(country)
        {
            this.twoLetterCode = twoLetterCode;
            this.englishName = englishName;
            this.nativeName = nativeName;
        }

        /// <summary>
        /// Gets the type of the division.
        /// </summary>
        public override string Type
        {
            get { return "territory"; }
        }

        /// <summary>
        /// Gets the two letter code of the country division
        /// </summary>
        public override string TwoLetterCode
        {
            get { return this.twoLetterCode; }
        }

        /// <summary>
        /// Gets the English name of the country division
        /// </summary>
        public override string EnglishName
        {
            get { return this.englishName; }
        }

        /// <summary>
        /// Gets the name of the country division in it's native language
        /// </summary>
        public override string NativeName
        {
            get { return this.nativeName; }
        }
    }
}
