using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace System.Web.Mvc.UI
{
    /// <summary>
    ///     HTML Anchor
    /// </summary>
    public class A : Control
    {
        /// <summary>
        ///     Gets or sets the text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///     Gets or sets the href.
        /// </summary>
        public string Href
        {
            get { return this.NullableAttribute("href"); }
            set { this.NullableAttribute("href", value); }
        }

        /// <summary>
        ///     Gets or sets the hreflang.
        /// </summary>
        /// <value>The hreflang.</value>
        public string HrefLang
        {
            get { return this.NullableAttribute("hreflang"); }
            set { this.NullableAttribute("hreflang", value); }
        }

        /// <summary>
        ///     Gets or sets the rel.
        /// </summary>
        /// <value>The rel.</value>
        public string Rel
        {
            get { return this.NullableAttribute("rel"); }
            set { this.NullableAttribute("rel", value); }
        }

        /// <summary>
        /// Gets or sets the rev.
        /// </summary>
        /// <value>The rev.</value>
        public string Rev
        {
            get { return this.NullableAttribute("rev"); }
            set { this.NullableAttribute("rev", value); }
        }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>The target.</value>
        public string Target
        {
            get { return this.NullableAttribute("target"); }
            set { this.NullableAttribute("target", value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="A"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="href">The href.</param>
        public A(string text, string href)
            : base(HtmlTextWriterTag.A, false)
        {
            this.Text = text;
            this.Href = href;
            this.IsIndented = false;
        }

        /// <summary>
        /// Render the contents of the control
        /// </summary>
        /// <param name="htmlTextWriter">The writer to write to</param>
        protected override void RenderContents(HtmlTextWriter htmlTextWriter)
        {
            htmlTextWriter.Write(this.Text);
            base.RenderContents(htmlTextWriter);
        }
    }
}
