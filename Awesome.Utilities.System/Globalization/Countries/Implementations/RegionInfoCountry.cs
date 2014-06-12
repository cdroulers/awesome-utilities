using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Globalization.Countries.Implementations
{
    /// <summary>
    ///     A country implementation with RegionInfo
    /// </summary>
    public class RegionInfoCountry : Country
    {
        private readonly RegionInfo regionInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionInfoCountry"/> class.
        /// </summary>
        /// <param name="regionInfo">The region info.</param>
        public RegionInfoCountry(RegionInfo regionInfo)
            : base(regionInfo.TwoLetterISORegionName)
        {
            Validate.Is.Not.Null(regionInfo, "regionInfo");
            this.regionInfo = regionInfo;
        }

        /// <summary>
        /// Gets the two letter code. <see href="http://en.wikipedia.org/wiki/ISO_3166-1_alpha-2" />
        /// </summary>
        public override string TwoLetterCode
        {
            get { return this.regionInfo.TwoLetterISORegionName; }
        }

        /// <summary>
        /// Gets the three letter code. <see href="http://en.wikipedia.org/wiki/ISO_3166-1_alpha-3" />
        /// </summary>
        public override string ThreeLetterCode
        {
            get { return this.regionInfo.ThreeLetterISORegionName; }
        }

        /// <summary>
        /// Gets the English name.
        /// </summary>
        public override string EnglishName
        {
            get { return this.regionInfo.EnglishName; }
        }

        /// <summary>
        /// Gets the name of the country in it's native language
        /// </summary>
        public override string NativeName
        {
            get { return this.regionInfo.NativeName; }
        }
    }
}
