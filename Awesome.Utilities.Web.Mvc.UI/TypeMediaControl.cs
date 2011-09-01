using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Web.UI;

namespace Awesome.Utilities.Web.Mvc.UI
{
    /// <summary>
    ///     A control with a type and a media
    /// </summary>
    public abstract class TypeMediaControl : TypeControl
    {
        /// <summary>
        ///     Gets or sets the media.
        /// </summary>
        /// <value>The media.</value>
        public MediaValue Media
        {
            get { return this.EnumAttributeWithDefault("media", MediaValue.None); }
            set { this.EnumAttribute("media", value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeControl"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>        
        protected TypeMediaControl(HtmlTextWriterTag tag)
            : this(tag, new ContentType()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeControl"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="type">The type.</param>
        protected TypeMediaControl(HtmlTextWriterTag tag, ContentType type)
            : this(tag, type, Control.IsSelfClosingDefault) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeControl"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="type">The type.</param>
        /// <param name="isSelfClosing">if set to <c>true</c> [is self closing].</param>
        protected TypeMediaControl(HtmlTextWriterTag tag, ContentType type, bool isSelfClosing)
            : base(tag, type, isSelfClosing)
        {
            this.Type = type;
        }
    }
}
