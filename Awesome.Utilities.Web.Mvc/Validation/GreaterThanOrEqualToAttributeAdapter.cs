using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc.Validation
{
    /// <summary>
    ///     An adapter for GreaterThanOrEqualToAttribute
    /// </summary>
    public class GreaterThanOrEqualToAttributeAdapter : DataAnnotationsModelValidator<GreaterThanOrEqualToAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GreaterThanOrEqualToAttributeAdapter"/> class.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="context">The context.</param>
        /// <param name="attribute">The attribute.</param>
        public GreaterThanOrEqualToAttributeAdapter(ModelMetadata metadata, ControllerContext context, GreaterThanOrEqualToAttribute attribute)
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
                ValidationType = "greaterthanorequalto"
            };

            rule.ValidationParameters["property"] = this.Attribute.PropertyName;

            return new[] { rule };
        }
    }
}
