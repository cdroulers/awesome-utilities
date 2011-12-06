using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Globalization.Countries
{
    /// <summary>
    ///     A repository of countries
    /// </summary>
    public interface ICountryRepository
    {
        /// <summary>
        /// Gets all the countries.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ICountry> GetAll();

        /// <summary>
        /// Gets a country by two letter code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        ICountry GetByTwoLetterCode(string code);

        /// <summary>
        /// Gets a country by three letter code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        ICountry GetByThreeLetterCode(string code);
    }
}
