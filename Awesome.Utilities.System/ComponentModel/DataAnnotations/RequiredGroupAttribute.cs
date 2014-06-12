using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    ///     An attribute to validate that at least one property in a group is valid.
    /// </summary>
    public class RequiredGroupAttribute : RequiredAttribute
    {
        /// <summary>
        /// Gets the name of the group.
        /// </summary>
        public string GroupName { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether to allow multiple required elements in the same group to be set.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow multiple]; otherwise, <c>false</c>.
        /// </value>
        public bool AllowMultiple { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredGroupAttribute"/> class.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        public RequiredGroupAttribute(string groupName)
        {
            this.GroupName = groupName;
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
            var properties = validationContext.ObjectType.GetProperties().Where(p => p.GetCustomAttributes(typeof(RequiredGroupAttribute), true).Cast<RequiredGroupAttribute>().Where(c => c.GroupName == this.GroupName).Any()).ToList();

            int count = properties.Count(p => base.IsValid(p.GetValue(validationContext.ObjectInstance, null)));

            bool valid = this.AllowMultiple ? (count > 0) : count == 1;

            return valid ? null : new ValidationResult(this.FormatErrorMessage(validationContext.MemberName), properties.Select(p => p.Name));
        }
    }
}
