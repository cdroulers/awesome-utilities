using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace System.Globalization.Countries
{
    /// <summary>
    ///     A country on planet Earth.
    /// </summary>
    [DebuggerDisplay("Country {TwoLetterCode} - {EnglishName}")]
    public abstract class Country
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Country"/> class.
        /// </summary>
        /// <param name="twoLetterIsoCode">The two letter iso code.</param>
        protected Country(string twoLetterIsoCode)
        {
            Validate.Is.Not.Null(twoLetterIsoCode, "twoLetterIsoCode");
            switch (twoLetterIsoCode)
            {
                case "US":
                    this.divisions = CountryDivision.GetUnitedStatesDivisions(this);
                    break;
                case "CA":
                    this.divisions = CountryDivision.GetCanadaDivisions(this);
                    break;
                default:
                    this.divisions = new ReadOnlyCollection<CountryDivision>(new List<CountryDivision>());
                    break;
            }
        }

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


        private readonly ReadOnlyCollection<CountryDivision> divisions;
        /// <summary>
        /// Gets the divisions of this country.
        /// </summary>
        public ReadOnlyCollection<CountryDivision> Divisions
        {
            get { return this.divisions; }
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <param name="cultureInfo">The culture info. Should use current UI culture if not specified</param>
        /// <returns></returns>
        public string GetDisplayName(CultureInfo cultureInfo = null)
        {
            return Properties.Languages.ResourceManager.GetString(this.TwoLetterCode, cultureInfo ?? CultureInfo.CurrentUICulture);
        }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(Country other)
        {
            return this.TwoLetterCode == other.TwoLetterCode && this.ThreeLetterCode == other.ThreeLetterCode && this.EnglishName == other.EnglishName && this.NativeName == other.NativeName &&
                this.Divisions.SequenceEqual(other.Divisions);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Country)) return false;
            return Equals((Country)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int result = (TwoLetterCode != null ? TwoLetterCode.GetHashCode() : 0);
                result = (result * 397) ^ (ThreeLetterCode != null ? ThreeLetterCode.GetHashCode() : 0);
                result = (result * 397) ^ (EnglishName != null ? EnglishName.GetHashCode() : 0);
                result = (result * 397) ^ (NativeName != null ? NativeName.GetHashCode() : 0);
                result = (result * 397) ^ (Divisions != null ? Divisions.GetHashCode() : 0);
                return result;
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("TwoLetterCode: {0}, ThreeLetterCode: {1}, EnglishName: {2}, NativeName: {3}, Divisions: {4}", this.TwoLetterCode, this.ThreeLetterCode, this.EnglishName, this.NativeName, this.Divisions.Count);
        }
    }
}
