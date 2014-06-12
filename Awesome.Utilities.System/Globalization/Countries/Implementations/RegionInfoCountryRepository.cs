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
        private readonly IList<Country> countries = new List<Country>();

        /// <summary>
        /// Gets all the countries.
        /// </summary>
        /// <returns>
        /// A list of all countries.
        /// </returns>
        public IEnumerable<Country> GetAll()
        {
            if (!this.countries.Any())
            {
                foreach (var info in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
                {
                    var regionInfo = new RegionInfo(info.Name);
                    if (!this.countries.Any(r => r.EnglishName == regionInfo.EnglishName))
                    {
                        this.countries.Add(new RegionInfoCountry(regionInfo));
                    }
                }
            }

            return this.countries;
        }

        /// <summary>
        /// Gets a country by two letter code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>
        /// The country with the specified two letter code.
        /// </returns>
        /// <exception cref="NotFoundException">If the code cannot be found.</exception>
        public Country GetByTwoLetterCode(string code)
        {
            var found = this.GetAll().FirstOrDefault(c => c.TwoLetterCode == code);
            if (found == null)
            {
                throw new NotFoundException(typeof(Country), code, "TwoLetterCode", string.Format(Properties.Strings.ICountry_NotFoundTwoLetterCode, code));
            }

            return found;
        }

        /// <summary>
        /// Gets a country by three letter code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>
        /// The country with the specified three letter code.
        /// </returns>
        /// <exception cref="NotFoundException">If the code cannot be found.</exception>
        public Country GetByThreeLetterCode(string code)
        {
            var found = this.GetAll().FirstOrDefault(c => c.ThreeLetterCode == code);
            if (found == null)
            {
                throw new NotFoundException(typeof(Country), code, "ThreeLetterCode", string.Format(Properties.Strings.ICountry_NotFoundThreeLetterCode, code));
            }

            return found;
        }
    }
}
