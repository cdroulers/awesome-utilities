using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Globalization.Currencies
{
    /// <summary>
    ///     Part of a money output.
    /// </summary>
    public enum CurrencyPart
    {
        /// <summary>
        ///     Currency symbol ($, £, etc.)
        /// </summary>
        Symbol,

        /// <summary>
        ///     Dollars or Euros or equivalent
        /// </summary>
        Amount,

        /// <summary>
        ///     Cents or Hundredths of Euro or equivalent.
        /// </summary>
        Decimals,
    }
}
