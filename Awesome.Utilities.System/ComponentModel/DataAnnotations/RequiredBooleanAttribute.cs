using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    ///     A validation attribute that ensures a value is set on a boolean property
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class RequiredBooleanAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets or sets the value that the property should have
        /// </summary>
        public bool ExpectedValue { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredBooleanAttribute"/> class.
        /// </summary>
        /// <param name="expectecValue">if set to <c>true</c> [expectec value].</param>
        public RequiredBooleanAttribute(bool expectecValue = true)
        {
            this.ExpectedValue = expectecValue;
        }

        /// <summary>
        /// Applies formatting to an error message, based on the data field where the error occurred.
        /// </summary>
        /// <param name="name">The name to include in the formatted message.</param>
        /// <returns>
        /// An instance of the formatted error message.
        /// </returns>
        public override string FormatErrorMessage(string name)
        {
            if (this.ErrorMessage == null && this.ErrorMessageResourceName == null)
            {
                this.ErrorMessage = this.ExpectedValue ? Properties.Strings.Validation_RequiredBooleanTrue : Properties.Strings.Validation_RequiredBooleanFalse;
            }
            return string.Format(CultureInfo.CurrentUICulture, this.ErrorMessageString, name, this.ExpectedValue);
        }

        /// <summary>
        /// Determines whether the specified value of the object is valid.
        /// </summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <returns>
        /// true if the specified value is valid; otherwise, false.
        /// </returns>
        public override bool IsValid(object value)
        {
            return this.ExpectedValue == (bool)value;
        }
    }
}
