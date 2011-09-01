using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;

namespace Awesome.Utilities.Web.Mvc.UI
{
    /// <summary>
    ///     represents a css style element
    /// </summary>
    public class CssStyle : Style
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CssStyle"/> class.
        /// </summary>
        public CssStyle(string content)
            : base(new ContentType("text/css"))
        {
            this.Content = content;
        }
    }
}
