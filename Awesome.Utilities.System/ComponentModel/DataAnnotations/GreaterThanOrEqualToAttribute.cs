using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    ///     An attribute to validate that a value is greater or equal to another
    /// </summary>
    public class GreaterThanOrEqualToAttribute : ComparisonAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GreaterThanOrEqualToAttribute"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public GreaterThanOrEqualToAttribute(string propertyName)
            : base(propertyName)
        {
            this.ErrorMessageResourceType = typeof(Properties.Strings);
            this.ErrorMessageResourceName = "Validation_GreaterThanOrEqualTo";
        }

        /// <summary>
        /// Compares the specified other value.
        /// </summary>
        /// <param name="otherValue">The other value.</param>
        /// <param name="thisValue">The this value.</param>
        /// <returns>True if the values compared in the defined way.</returns>
        protected override bool Compare(IComparable otherValue, object thisValue)
        {
            return otherValue.CompareTo(thisValue) > 0;
        }
    }
}
