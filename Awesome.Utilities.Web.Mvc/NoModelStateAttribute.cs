using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc.Binders;

namespace System.Web.Mvc
{
    /// <summary>
    ///     Add this attribute to a class or parameter that shouldn't add an entry to the model state when binding.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class NoModelStateAttribute : CustomModelBinderAttribute
    {
        private readonly Type bindType;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoModelStateAttribute"/> class.
        /// </summary>
        /// <param name="bindType">Type of the bind.</param>
        public NoModelStateAttribute(Type bindType)
        {
            this.bindType = bindType;
        }

        /// <summary>
        /// Retrieves the associated model binder.
        /// </summary>
        /// <returns>
        /// A reference to an object that implements the <see cref="T:System.Web.Mvc.IModelBinder"/> interface.
        /// </returns>
        public override IModelBinder GetBinder()
        {
            return new NoModelStateModelBinder(ModelBinders.Binders.GetBinder(this.bindType, true));
        }
    }
}
