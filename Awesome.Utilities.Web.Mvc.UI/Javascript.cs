using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;

namespace Awesome.Utilities.Web.Mvc.UI
{
    /// <summary>
    ///     Represents a javascript script element
    /// </summary>
    public class Javascript : Script
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Javascript"/> class.
        /// </summary>
        public Javascript() : this(null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Javascript"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public Javascript(string source) : base(new ContentType("text/javascript"), source) { }

        /// <summary>
        /// Build an HTML javascript thinger with content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static Javascript WithContent(string content)
        {
            return new Javascript() { Content = content };
        }
    }
}
