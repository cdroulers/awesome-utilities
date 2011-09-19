using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Web.Mvc
{
    /// <summary>
    /// A binder for strings that trims them
    /// </summary>
    public class TrimmedStringBinder : DefaultModelBinder
    {
        /// <summary>
        /// Binds the model.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="bindingContext">The binding context.</param>
        /// <returns>The bounded value.</returns>
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(string))
            {
                throw new ArgumentException("This binder only works for string.");
            }

            string value = base.BindModel(controllerContext, bindingContext) as string;

            return value == null ? null : value.Trim();
        }
    }
}
