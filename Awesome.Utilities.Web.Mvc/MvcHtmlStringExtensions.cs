using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Web.Mvc
{
    /// <summary>
    ///     Extensions for MvcHtmlString
    /// </summary>
    public static class MvcHtmlStringExtensions
    {
        /// <summary>
        /// Adds the specified self.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="other">The other.</param>
        /// <param name="addSpace">if set to <c>true</c> [add space].</param>
        /// <returns></returns>
        public static MvcHtmlString Add(this MvcHtmlString self, MvcHtmlString other, bool addSpace = false)
        {
            return self.Add(other.ToString(), addSpace);
        }

        /// <summary>
        /// Adds the specified self.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="other">The other.</param>
        /// <param name="addSpace">if set to <c>true</c> [add space].</param>
        /// <returns></returns>
        public static MvcHtmlString Add(this MvcHtmlString self, string other, bool addSpace = false)
        {
            return MvcHtmlString.Create(self.ToString() + (addSpace ? " " : "") + other);
        }
    }
}
