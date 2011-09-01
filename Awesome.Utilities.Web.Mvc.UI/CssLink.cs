using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mime;

namespace Awesome.Utilities.Web.Mvc.UI
{
    /// <summary>
    ///     A css link
    /// </summary>
    public class CssLink : Link
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CssLink"/> class.
        /// </summary>
        /// <param name="href">The href.</param>
        public CssLink(string href) : this(href, MediaValue.None) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CssLink"/> class.
        /// </summary>
        /// <param name="href">The href.</param>
        /// <param name="mediaValue">The media value.</param>
        public CssLink(string href, MediaValue mediaValue)
            : base(new ContentType("text/css"), href)
        {
            this.Rel = RelValue.Stylesheet;
            this.Media = mediaValue;
        }
    }
}
