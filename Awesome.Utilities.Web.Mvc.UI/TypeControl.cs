using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mime;
using System.Web.UI;

namespace Awesome.Utilities.Web.Mvc.UI
{
    /// <summary>
    ///     Represents a control that has a type attribute (e.g. style, script)
    /// </summary>
    public abstract class TypeControl : Control
    {
        /// <summary>
        ///     Gets or sets the type of the script element
        /// </summary>
        /// <value>The type.</value>
        public ContentType Type
        {
            get { return this.attributes.ContainsKey("type") ? new ContentType(this.attributes["type"]) : null; }
            set { this.NullableAttribute("type", value.MediaType); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeControl"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>        
        protected TypeControl(HtmlTextWriterTag tag)
            : this(tag, new ContentType())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeControl"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="type">The type.</param>
        protected TypeControl(HtmlTextWriterTag tag, ContentType type)
            : this(tag, type, Control.IsSelfClosingDefault)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeControl"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="type">The type.</param>
        /// <param name="isSelfClosing">if set to <c>true</c> [is self closing].</param>
        protected TypeControl(HtmlTextWriterTag tag, ContentType type, bool isSelfClosing)
            : base(tag, isSelfClosing)
        {
            this.Type = type;
        }
    }
}
