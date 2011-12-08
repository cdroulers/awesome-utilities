using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Globalization.Countries.Implementations
{
    /// <summary>
    ///     Region info implementation of a country repository.
    /// </summary>
    public class RegionInfoCountryRepository : ICountryRepository
    {
        /// <summary>
        /// Gets all the countries.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ICountry> GetAll()
        {
            var results = new List<ICountry>();
            foreach (var info in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                var regionInfo = new RegionInfo(info.Name);
                if (!results.Any(r => r.EnglishName == regionInfo.EnglishName))
                {
                    results.Add(new RegionInfoCountry(regionInfo));
                }
            }
            return results;
        }

        /// <summary>
        /// Gets a country by two letter code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public ICountry GetByTwoLetterCode(string code)
        {
            var found = this.GetAll().FirstOrDefault(c => c.TwoLetterCode == code);
            if (found == null)
            {
                throw new NotFoundException(typeof(ICountry), code, "TwoLetterCode", string.Format(Properties.Strings.ICountry_NotFoundTwoLetterCode, code));
            }
            return found;
        }

        /// <summary>
        /// Gets a country by three letter code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public ICountry GetByThreeLetterCode(string code)
        {
            var found = this.GetAll().FirstOrDefault(c => c.ThreeLetterCode == code);
            if (found == null)
            {
                throw new NotFoundException(typeof(ICountry), code, "ThreeLetterCode", string.Format(Properties.Strings.ICountry_NotFoundThreeLetterCode, code));
            }
            return found;
        }
    }
}
