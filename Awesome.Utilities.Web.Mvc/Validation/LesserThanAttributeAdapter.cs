using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc.Validation
{
    /// <summary>
    ///     An adapter for LesserThanAttribute
    /// </summary>
    public class LesserThanAttributeAdapter : DataAnnotationsModelValidator<LesserThanAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LesserThanAttribute"/> class.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="context">The context.</param>
        /// <param name="attribute">The attribute.</param>
        public LesserThanAttributeAdapter(ModelMetadata metadata, ControllerContext context, LesserThanAttribute attribute)
            : base(metadata, context, attribute)
        {
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            var rule = new ModelClientValidationRule()
            {
                ErrorMessage = this.ErrorMessage,
                ValidationType = "lesserthan"
            };

            rule.ValidationParameters["property"] = this.Attribute.PropertyName;

            return new[] { rule };
        }
    }
}
