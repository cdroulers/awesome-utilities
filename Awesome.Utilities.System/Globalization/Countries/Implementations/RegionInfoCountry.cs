using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Globalization.Countries.Implementations
{
    /// <summary>
    ///     A country implementation with RegionInfo
    /// </summary>
    public class RegionInfoCountry : ICountry
    {
        private readonly RegionInfo regionInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionInfoCountry"/> class.
        /// </summary>
        /// <param name="regionInfo">The region info.</param>
        public RegionInfoCountry(RegionInfo regionInfo)
        {
            Validate.Is.NotNull(regionInfo, "regionInfo");
            this.regionInfo = regionInfo;
        }

        /// <summary>
        /// Gets the two letter code. http://en.wikipedia.org/wiki/ISO_3166-1_alpha-2
        /// </summary>
        public string TwoLetterCode
        {
            get { return this.regionInfo.TwoLetterISORegionName; }
        }

        /// <summary>
        /// Gets the three letter code. http://en.wikipedia.org/wiki/ISO_3166-1_alpha-3
        /// </summary>
        public string ThreeLetterCode
        {
            get { return this.regionInfo.ThreeLetterISORegionName; }
        }

        /// <summary>
        /// Gets the English name.
        /// </summary>
        public string EnglishName
        {
            get { return this.regionInfo.EnglishName; }
        }

        /// <summary>
        /// Gets the name of the country in it's native language
        /// </summary>
        public string NativeName
        {
            get { return this.regionInfo.NativeName; }
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
    }
}
