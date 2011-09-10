using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Web.Mvc
{
    /// <summary>
    ///     An attribute to put on action parameters that shouldn't be trimmed.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class NoTrimAttribute : CustomModelBinderAttribute
    {
        /// <summary>
        /// Gets the model binder.
        /// </summary>
        /// <returns>
        /// A reference to the interface of the model binder.
        /// </returns>
        public override IModelBinder GetBinder()
        {
            return new DefaultModelBinder();
        }
    }
}
