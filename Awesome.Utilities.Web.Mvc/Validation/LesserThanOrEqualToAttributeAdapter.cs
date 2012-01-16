using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc.Validation
{
    /// <summary>
    ///     An adapter for LesserThanOrEqualToAttribute
    /// </summary>
    public class LesserThanOrEqualToAttributeAdapter : DataAnnotationsModelValidator<LesserThanOrEqualToAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LesserThanOrEqualToAttributeAdapter"/> class.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="context">The context.</param>
        /// <param name="attribute">The attribute.</param>
        public LesserThanOrEqualToAttributeAdapter(ModelMetadata metadata, ControllerContext context, LesserThanOrEqualToAttribute attribute)
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
                ValidationType = "lesserthanorequalto"
            };

            rule.ValidationParameters["property"] = this.Attribute.PropertyName;

            return new[] { rule };
        }
    }
}
