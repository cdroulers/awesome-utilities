using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Web.UI;

namespace Awesome.Utilities.Web.Mvc.UI
{
    /// <summary>
    ///     Represents the link html element
    /// </summary>
    public class Link : TypeMediaControl
    {
        /// <summary>
        ///     Gets or sets the href.
        /// </summary>
        /// <value>The href.</value>
        public string Href
        {
            get { return this.NullableAttribute("href"); }
            set { this.NullableAttribute("href", value); }
        }

        /// <summary>
        ///     Gets or sets the rel.
        /// </summary>
        /// <value>The rel.</value>
        public RelValue Rel
        {
            get { return this.EnumAttributeWithDefault("rel", RelValue.None); }
            set { this.EnumAttribute("rel", value); }
        }

        /// <summary>
        /// Gets or sets the rev.
        /// </summary>
        /// <value>The rev.</value>
        public RelValue Rev
        {
            get { return this.EnumAttributeWithDefault("rev", RelValue.None); }
            set { this.EnumAttribute("rev", value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Link"/> class.
        /// </summary>
        public Link() : this(new ContentType()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Link"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public Link(ContentType type) : this(type, string.Empty) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Link"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="href">The href.</param>
        public Link(ContentType type, string href)
            : base(HtmlTextWriterTag.Link, type, true)
        {
            this.Href = href;
        }
    }
}
