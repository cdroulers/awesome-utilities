using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;

namespace Awesome.Utilities.Web.Mvc.UI
{
    /// <summary>
    ///     Represents a style element
    /// </summary>
    public class Style : TypeMediaControl
    {
        /// <summary>
        ///     Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        public string Content { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Style"/> class.
        /// </summary>
        public Style() : this(new ContentType()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Style"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public Style(ContentType type) : this(type, string.Empty) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Style"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="content">The content.</param>
        public Style(ContentType type, string content)
            : base(System.Web.UI.HtmlTextWriterTag.Style, type)
        {
            this.Content = content;
        }

        /// <summary>
        /// Render the contents of the control
        /// </summary>
        /// <param name="htmlTextWriter">The writer to write to</param>
        protected override void RenderContents(System.Web.UI.HtmlTextWriter htmlTextWriter)
        {
            htmlTextWriter.WriteLine(this.Content);
            base.RenderContents(htmlTextWriter);
        }
    }
}
