using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    ///     An attribute to validate that a value is lesser than another
    /// </summary>
    public class LesserThanAttribute : ComparisonAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LesserThanAttribute"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public LesserThanAttribute(string propertyName)
            : base(propertyName)
        {
            this.ErrorMessageResourceType = typeof(Properties.Strings);
            this.ErrorMessageResourceName = "Validation_LesserThan";
        }

        /// <summary>
        /// Compares the specified other value.
        /// </summary>
        /// <param name="otherValue">The other value.</param>
        /// <param name="thisValue">The this value.</param>
        /// <returns></returns>
        protected override bool Compare(IComparable otherValue, object thisValue)
        {
            return otherValue.CompareTo(thisValue) <= 0;
        }
    }
}
