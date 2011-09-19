using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace System.Web.Mvc.UI
{
    /// <summary>
    ///     An image tag
    /// </summary>
    public class Image : Control
    {
        /// <summary>
        ///     The src attribute of the img tag
        /// </summary>
        public string Source
        {
            get { return this.NullableAttribute("src"); }
            set { this.NullableAttribute("src", value); }
        }

        /// <summary>
        ///     The alt attribute of the img tag
        /// </summary>
        public string AlternateText
        {
            get { return this.NullableAttribute("alt"); }
            set { this.NullableAttribute("alt", value); }
        }

        /// <summary>
        ///     Creates an image object
        /// </summary>
        /// <param name="source">The src image file</param>
        /// <param name="alternateText">The alternate text for this image</param>
        public Image(string source, string alternateText)
            : base(HtmlTextWriterTag.Img, true)
        {
            this.Source = source;
            this.AlternateText = alternateText;
        }
    }
}
