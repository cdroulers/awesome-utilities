using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    ///     An attribute to check the length of a mail address
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class UriLengthAttribute : StringLengthAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UriLengthAttribute"/> class.
        /// </summary>
        /// <param name="maximumLength">The maximum length of a string.</param>
        public UriLengthAttribute(int maximumLength)
            : base(maximumLength)
        {
        }

        /// <summary>
        /// Determines whether a specified object is valid.
        /// </summary>
        /// <param name="value">The object to validate.</param>
        /// <returns>
        /// true if the specified object is valid; otherwise, false.
        /// </returns>
        public override bool IsValid(object value)
        {
            return base.IsValid(value is Uri ? (value as Uri).ToString() : null);
        }
    }
}
