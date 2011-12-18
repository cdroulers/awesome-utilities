using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

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

        protected ComparisonAttribute(string propertyName)
            : base()
        {
            this.PropertyName = propertyName;
        }

        protected abstract bool Compare(IComparable otherValue, object thisValue);

        public override string FormatErrorMessage(string name)
        {
            if (this.ErrorMessage == null && this.ErrorMessageResourceName == null)
            {
                this.ErrorMessage = Properties.Strings.Validation_Comparison;
            }
            return string.Format(CultureInfo.CurrentUICulture, this.ErrorMessageString, name, (this.PropertyDisplayName ?? this.PropertyName));
        }

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
                return new ValidationResult(this.FormatErrorMessage(validationContext.MemberName), new string[] { validationContext.MemberName });
            }

            return null;
        }
    }
}
