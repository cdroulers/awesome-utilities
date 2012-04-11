using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace System.Collections.Specialized
{
    /// <summary>
    ///     Extensions for NameValueCollection
    /// </summary>
    public static class NameValueCollectionExtensions
    {
        /// <summary>
        /// Transforms a name value collection into a query string.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="prependQueryStringDelimiter">if set to <c>true</c> prepends the query string delimiter "?".</param>
        /// <returns></returns>
        public static string ToQueryString(this NameValueCollection self, bool prependQueryStringDelimiter = false)
        {
            return (prependQueryStringDelimiter ? "?" : string.Empty) + string.Join("&", self.AllKeys.Select(k => k + "=" + self[k]));
        }
    }
}
