using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace System.Web.Mvc.UI
{
    /// <summary>
    ///     Any kind of control, with content in it.
    /// </summary>
    public class LiteralControl : Control
    {
        /// <summary>
        ///     Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        public string Content { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LiteralControl"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="content">The content.</param>
        /// <param name="isSelfClosing">if set to <c>true</c> [is self closing].</param>
        public LiteralControl(HtmlTextWriterTag tag, string content = null, bool isSelfClosing = Control.IsSelfClosingDefault)
            : base(tag, isSelfClosing)
        {
            this.Content = content;
        }

        /// <summary>
        /// Render the contents of the control
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
