using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace System.Globalization
{
    /// <summary>
    ///     A class to set the current culture temporarily.
    /// </summary>
    public static class Culture
    {
        /// <summary>
        /// Gets a value indicating whether this instance is set to something.
        /// </summary>
        public static bool IsSet { get; private set; }

        /// <summary>
        /// Gets the current culture.
        /// </summary>
        public static CultureInfo CurrentCulture
        {
            get { return CultureInfo.CurrentCulture; }
        }

        /// <summary>
        /// Gets the current UI culture.
        /// </summary>
        public static CultureInfo CurrentUICulture
        {
            get { return CultureInfo.CurrentUICulture; }
        }

        /// <summary>
        /// Sets the culture as the specified cultures until disposed.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <param name="uiCulture">The UI culture.</param>
        /// <returns>An IDisposable instance</returns>
        public static IDisposable As(CultureInfo culture, CultureInfo uiCulture = null)
        {
            Culture.IsSet = true;
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            var oldUICulture = Thread.CurrentThread.CurrentUICulture;
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = uiCulture ?? culture;
            return new DisposableAction(() =>
            {
                Culture.IsSet = false;
                Thread.CurrentThread.CurrentCulture = oldCulture;
                Thread.CurrentThread.CurrentUICulture = oldUICulture;
            });
        }

        /// <summary>
        /// Tries to create a CultureInfo from the string.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <param name="result">The result.</param>
        /// <param name="allowInvariant">if set to <c>true</c> [allow invariant].</param>
        /// <returns>
        /// true if it was parsed.
        /// </returns>
        public static bool TryParse(string culture, out CultureInfo result, bool allowInvariant = false)
        {
            try
            {
                if (!allowInvariant && string.IsNullOrWhiteSpace(culture))
                {
                    result = null;
                    return false;
                }

                result = new CultureInfo((culture ?? string.Empty).Replace("_", "-"));
                return true;
            }
            catch (CultureNotFoundException)
            {
                result = null;
                return false;
            }
        }
    }
}