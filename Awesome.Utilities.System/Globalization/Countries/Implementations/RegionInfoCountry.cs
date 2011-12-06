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

        public string TwoLetterCode
        {
            get { return this.regionInfo.TwoLetterISORegionName; }
        }

        public string ThreeLetterCode
        {
            get { return this.regionInfo.ThreeLetterISORegionName; }
        }

        public string EnglishName
        {
            get { return this.regionInfo.EnglishName; }
        }

        public string NativeName
        {
            get { return this.regionInfo.NativeName; }
        }

        public string GetDisplayName(CultureInfo cultureInfo = null)
        {
            return Properties.Languages.ResourceManager.GetString(this.TwoLetterCode, cultureInfo ?? CultureInfo.CurrentUICulture);
        }
    }
}
