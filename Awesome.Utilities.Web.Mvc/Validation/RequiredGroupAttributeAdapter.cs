using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc.Validation
{
    /// <summary>
    ///     An adapter for RequiredGroupAttribute
    /// </summary>
    public class RequiredGroupAttributeAdapter : DataAnnotationsModelValidator<RequiredGroupAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredGroupAttributeAdapter"/> class.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="context">The context.</param>
        /// <param name="attribute">The attribute.</param>
        public RequiredGroupAttributeAdapter(ModelMetadata metadata, ControllerContext context, RequiredGroupAttribute attribute)
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
                ValidationType = "requiredgroup"
            };

            rule.ValidationParameters["groupname"] = this.Attribute.GroupName;
            rule.ValidationParameters["allowmultiple"] = this.Attribute.AllowMultiple.ToString().ToLowerInvariant();

            return new[] { rule };
        }
    }
}
