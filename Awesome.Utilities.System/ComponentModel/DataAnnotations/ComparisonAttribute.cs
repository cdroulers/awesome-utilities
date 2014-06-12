using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    ///     An attribute to compare two properties
    /// </summary>
    public abstract class ComparisonAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets or sets the name of the property to compare it too.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the display name of the property, for error messages.
        /// </summary>
        public string PropertyDisplayName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparisonAttribute"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected ComparisonAttribute(string propertyName)
        {
            this.PropertyName = propertyName;
        }

        /// <summary>
        /// Compares the specified other value.
        /// </summary>
        /// <param name="otherValue">The other value.</param>
        /// <param name="thisValue">The this value.</param>
        /// <returns>True if the values compared in the defined way.</returns>
        protected abstract bool Compare(IComparable otherValue, object thisValue);

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
                this.ErrorMessage = Properties.Strings.Validation_Comparison;
            }

            return string.Format(CultureInfo.CurrentUICulture, this.ErrorMessageString, name, this.PropertyDisplayName ?? this.PropertyName);
        }

        /// <summary>
        /// Validates the specified value with respect to the current validation attribute.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>
        /// An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult"/> class.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectInstance.GetType().GetProperty(this.PropertyName);
            if (property == null)
            {
                throw new InvalidOperationException(string.Format("Property {0} doesn't exist on type {1}", this.PropertyName, validationContext.ObjectInstance.GetType().Name));
            }

            var otherValue = property.GetValue(validationContext.ObjectInstance, null) as IComparable;
            if (otherValue == null)
            {
                throw new InvalidOperationException(string.Format("Property {0} of type {1} is not IComparable", this.PropertyName, validationContext.ObjectInstance.GetType().Name));
            }

            if (this.Compare(otherValue, value))
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.MemberName), new[] { validationContext.MemberName });
            }

            return null;
        }
    }
}
