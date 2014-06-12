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
        /// <returns>A list of all countries.</returns>
        IEnumerable<Country> GetAll();

        /// <summary>
        /// Gets a country by two letter code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>The country with the specified two letter code.</returns>
        Country GetByTwoLetterCode(string code);

        /// <summary>
        /// Gets a country by three letter code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>The country with the specified three letter code.</returns>
        Country GetByThreeLetterCode(string code);
    }
}
