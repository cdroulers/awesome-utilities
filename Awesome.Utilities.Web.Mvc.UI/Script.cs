using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Web.UI;

namespace Awesome.Utilities.Web.Mvc.UI
{
    /// <summary>
    ///     Represent the html script element
    /// </summary>
    public class Script : TypeControl
    {
        /// <summary>
        ///     Gets or sets the source of the script element
        /// </summary>
        /// <value>The source.</value>
        public string Source
        {
            get { return this.NullableAttribute("src"); }
            set { this.NullableAttribute("src", value); }
        }

        /// <summary>
        ///     Gets or sets the content of the script element
        /// </summary>
        /// <value>The content.</value>
        public string Content { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Script"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public Script(ContentType type) : this(type, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Script"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="source">The source.</param>
        public Script(ContentType type, string source)
            : base(HtmlTextWriterTag.Script, type)
        {
            this.Source = source;
        }

        /// <summary>
        ///     The basic rendering of the Control (i.e. the opening tag, call render content, close tag)
        /// </summary>
        /// <param name="htmlTextWriter">The writer to write to.</param>
        protected override void Render(HtmlTextWriter htmlTextWriter)
        {
            if (string.IsNullOrEmpty(this.Content))
            {
                this.IsIndented = false;
            }
            base.Render(htmlTextWriter);
        }
        /// <summary>
        ///     Render the contents of the control
        /// </summary>
        /// <param name="htmlTextWriter">The writer to write to</param>
        protected override void RenderContents(HtmlTextWriter htmlTextWriter)
        {
            if (!string.IsNullOrEmpty(this.Content))
            {
                htmlTextWriter.WriteLine(this.Content);
            }
            base.RenderContents(htmlTextWriter);
        }
    }
}
