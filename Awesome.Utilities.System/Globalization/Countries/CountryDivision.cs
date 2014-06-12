using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace System.Globalization.Countries
{
    /// <summary>
    ///     A country's divisions.
    ///     e.g. Canada => Province, US => State
    /// </summary>
    [DebuggerDisplay("CountryDivision {TwoLetterCode} - {EnglishName}")]
    public abstract class CountryDivision
    {
        /// <summary>
        ///     The country this province belongs to.
        /// </summary>
        public readonly Country Country;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryDivision" /> class.
        /// </summary>
        /// <param name="country">The country.</param>
        protected CountryDivision(Country country)
        {
            this.Country = country;
        }

        /// <summary>
        /// Gets the type of the division.
        /// </summary>
        public abstract string Type { get; }

        /// <summary>
        /// Gets the two letter code of the country division
        /// </summary>
        public abstract string TwoLetterCode { get; }

        /// <summary>
        /// Gets the English name of the country division
        /// </summary>
        public abstract string EnglishName { get; }

        /// <summary>
        /// Gets the name of the country division in it's native language
        /// </summary>
        public abstract string NativeName { get; }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <param name="cultureInfo">The culture info. Should use current UI culture if not specified</param>
        /// <returns>The display name</returns>
        public string GetDisplayName(CultureInfo cultureInfo = null)
        {
            return Properties.Languages.ResourceManager.GetString(this.Country.TwoLetterCode + "_" + this.TwoLetterCode, cultureInfo ?? CultureInfo.CurrentUICulture);
        }

        /// <summary>
        /// Returns true if they are equal.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>True if they are equal.</returns>
        public bool Equals(CountryDivision other)
        {
            return this.TwoLetterCode == other.TwoLetterCode && this.Type == other.Type && this.EnglishName == other.EnglishName && this.NativeName == other.NativeName;
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
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == typeof(CountryDivision) && this.Equals((CountryDivision)obj);
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
                int result = this.TwoLetterCode != null ? this.TwoLetterCode.GetHashCode() : 0;
                result = (result * 397) ^ (this.Type != null ? this.Type.GetHashCode() : 0);
                result = (result * 397) ^ (this.EnglishName != null ? this.EnglishName.GetHashCode() : 0);
                result = (result * 397) ^ (this.NativeName != null ? this.NativeName.GetHashCode() : 0);
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
            return string.Format("Type: {0}, TwoLetterCode: {1}, EnglishName: {2}, NativeName: {3}", this.Type, this.TwoLetterCode, this.EnglishName, this.NativeName);
        }

        internal static ReadOnlyCollection<CountryDivision> GetCanadaDivisions(Country country)
        {
            return new ReadOnlyCollection<CountryDivision>(new List<CountryDivision>()
            {
                new Province(country, "ON", "Ontario", "Ontario"),
                new Province(country, "QC", "Quebec", "Québec"),
                new Province(country, "NS", "Nova Scotia", "Nova Scotia"),
                new Province(country, "NB", "New Brunswick", "New Brunswick"),
                new Province(country, "MB", "Manitoba", "Manitoba"),
                new Province(country, "BC", "British Columbia", "British Columbia"),
                new Province(country, "PE", "Prince Edward Island", "Prince Edward Island"),
                new Province(country, "SK", "Saskatchewan", "Saskatchewan"),
                new Province(country, "AB", "Alberta", "Alberta"),
                new Province(country, "NL", "Newfoundland and Labrador", "Newfoundland and Labrador"),
                new Territory(country, "NT", "Northwest Territories", "Northwest Territories"),
                new Territory(country, "YT", "Yukon", "Yukon"),
                new Territory(country, "NU", "Nunavut", "Nunavut"),
            });
        }

        internal static ReadOnlyCollection<CountryDivision> GetUnitedStatesDivisions(Country country)
        {
            return new ReadOnlyCollection<CountryDivision>(new List<CountryDivision>()
            {
                new State(country, "AL", "Alabama", "Alabama"),
                new State(country, "AK", "Alaska", "Alaska"),
                new State(country, "AZ", "Arizona", "Arizona"),
                new State(country, "AR", "Arkansas", "Arkansas"),
                new State(country, "CA", "California", "California"),
                new State(country, "CO", "Colorado", "Colorado"),
                new State(country, "CT", "Connecticut", "Connecticut"),
                new State(country, "DE", "Delaware", "Delaware"),
                new State(country, "FL", "Florida", "Florida"),
                new State(country, "GA", "Georgia", "Georgia"),
                new State(country, "HI", "Hawaii", "Hawaii"),
                new State(country, "ID", "Idaho", "Idaho"),
                new State(country, "IL", "Illinois", "Illinois"),
                new State(country, "IN", "Indiana", "Indiana"),
                new State(country, "IA", "Iowa", "Iowa"),
                new State(country, "KS", "Kansas", "Kansas"),
                new State(country, "KY", "Kentucky", "Kentucky"),
                new State(country, "LA", "Louisiana", "Louisiana"),
                new State(country, "ME", "Maine", "Maine"),
                new State(country, "MD", "Maryland", "Maryland"),
                new State(country, "MA", "Massachusetts", "Massachusetts"),
                new State(country, "MI", "Michigan", "Michigan"),
                new State(country, "MN", "Minnesota", "Minnesota"),
                new State(country, "MS", "Mississippi", "Mississippi"),
                new State(country, "MO", "Missouri", "Missouri"),
                new State(country, "MT", "Montana", "Montana"),
                new State(country, "NE", "Nebraska", "Nebraska"),
                new State(country, "NV", "Nevada", "Nevada"),
                new State(country, "NH", "New Hampshire", "New Hampshire"),
                new State(country, "NJ", "New Jersey", "New Jersey"),
                new State(country, "NM", "New Mexico", "New Mexico"),
                new State(country, "NY", "New York", "New York"),
                new State(country, "NC", "North Carolina", "North Carolina"),
                new State(country, "ND", "North Dakota", "North Dakota"),
                new State(country, "OH", "Ohio", "Ohio"),
                new State(country, "OK", "Oklahoma", "Oklahoma"),
                new State(country, "OR", "Oregon", "Oregon"),
                new State(country, "PA", "Pennsylvania", "Pennsylvania"),
                new State(country, "RI", "Rhode Island", "Rhode Island"),
                new State(country, "SC", "South Carolina", "South Carolina"),
                new State(country, "SD", "South Dakota", "South Dakota"),
                new State(country, "TN", "Tennessee", "Tennessee"),
                new State(country, "TX", "Texas", "Texas"),
                new State(country, "UT", "Utah", "Utah"),
                new State(country, "VT", "Vermont", "Vermont"),
                new State(country, "VA", "Virginia", "Virginia"),
                new State(country, "WA", "Washington", "Washington"),
                new State(country, "WV", "West Virginia", "West Virginia"),
                new State(country, "WI", "Wisconsin", "Wisconsin"),
                new State(country, "WY", "Wyoming", "Wyoming"),
                new District(country, "DC", "District of Columbia", "District of Columbia"),
                new Territory(country, "GU", "Guam", "Guam"),
                new Territory(country, "VI", "Virgin Islands", "Virgin Islands"),
                new Territory(country, "PR", "Puerto Rico", "Puerto Rico"),
                new Territory(country, "AS", "American Samoa", "American Samoa"),
            });
        }
    }
}
