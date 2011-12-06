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
        public IEnumerable<ICountry> GetAll()
        {
            foreach (var info in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                yield return new RegionInfoCountry(new RegionInfo(info.Name));
            }
        }

        public ICountry GetByTwoLetterCode(string code)
        {
            var found = this.GetAll().FirstOrDefault(c => c.TwoLetterCode == code);
            if (found == null)
            {
                throw new NotFoundException(typeof(ICountry), code, "TwoLetterCode", string.Format(Properties.Strings.ICountry_NotFoundTwoLetterCode, code));
            }
            return found;
        }

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
