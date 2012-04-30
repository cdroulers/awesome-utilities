using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Web.Mvc.UI
{
    /// <summary>
    ///     An HTML attribute for easy Razor syntax.
    /// </summary>
    public class HtmlAttribute : IHtmlString
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to display the attribute even if the value is empty.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [display empty value]; otherwise, <c>false</c>.
        /// </value>
        public bool DisplayEmptyValue { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlAttribute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="displayEmptyValue">if set to <c>true</c> [display empty value].</param>
        public HtmlAttribute(string name, string value, bool displayEmptyValue = false)
        {
            this.Name = name;
            this.Value = value;
            this.DisplayEmptyValue = displayEmptyValue;
        }

        /// <summary>
        /// Adds the specified value to the current value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public HtmlAttribute Add(string value)
        {
            return Add(value, true);
        }

        /// <summary>
        /// Adds the specified value if the condition is true
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <returns></returns>
        public HtmlAttribute Add(string value, bool condition)
        {
            if (!string.IsNullOrWhiteSpace(value) && condition)
                this.Value += " " + value;

            return this;
        }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(this.Value) || this.DisplayEmptyValue)
            {
                return string.Format("{0}=\"{1}\"", this.Name, this.Value);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Returns an HTML-encoded string.
        /// </summary>
        /// <returns>
        /// An HTML-encoded string.
        /// </returns>
        public string ToHtmlString()
        {
            return this.ToString();
        }
    }
}
