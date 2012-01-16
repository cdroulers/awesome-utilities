using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc.Validation
{
    /// <summary>
    ///     An adapter for RequiredBooleanAttribute
    /// </summary>
    public class RequiredBooleanAttributeAdapter : DataAnnotationsModelValidator<RequiredBooleanAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredBooleanAttributeAdapter"/> class.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="context">The context.</param>
        /// <param name="attribute">The attribute.</param>
        public RequiredBooleanAttributeAdapter(ModelMetadata metadata, ControllerContext context, RequiredBooleanAttribute attribute)
            : base(metadata, context, attribute)
        {
        }

        /// <summary>
        /// Retrieves a collection of client validation rules.
        /// </summary>
        /// <returns>
        /// A collection of client validation rules.
        /// </returns>
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            var rule = new ModelClientValidationRule()
            {
                ErrorMessage = this.ErrorMessage,
                ValidationType = "requiredboolean"
            };

            rule.ValidationParameters["value"] = this.Attribute.ExpectedValue.ToString().ToLowerInvariant();

            return new[] { rule };
        }
    }
}
