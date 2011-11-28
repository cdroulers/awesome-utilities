using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    ///     A set of extensions for the URI object.
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Changes the query string for the specified new value.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="newQueryString">The new query string, no need to put "?" in it.</param>
        /// <returns></returns>
        public static Uri ChangeQueryString(this Uri self, string newQueryString)
        {
            var builder = new UriBuilder(self);
            builder.Query = newQueryString.TrimStart('?');
            return builder.Uri;
        }

        /// <summary>
        /// Changes the path for the specified new value.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="newPath">The new query string, no need to put "?" in it.</param>
        /// <returns></returns>
        public static Uri ChangePath(this Uri self, string newPath)
        {
            var builder = new UriBuilder(self);
            builder.Path = newPath.TrimStart('/');
            return builder.Uri;
        }
    }
}
