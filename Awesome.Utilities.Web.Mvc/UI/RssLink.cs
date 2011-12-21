using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mime;

namespace System.Web.Mvc.UI
{
    /// <summary>
    ///     An rss link
    /// </summary>
    public class RssLink : Link
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RssLink"/> class.
        /// </summary>
        /// <param name="href">The href.</param>
        /// <param name="title">The title.</param>
        public RssLink(string href, string title)
            : base(new ContentType("application/rss+xml"), href)
        {
            this.Title = title;
            this.Rel = RelValue.Alternate;
        }
    }
}
