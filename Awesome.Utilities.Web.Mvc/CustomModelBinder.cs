using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Web.Mvc
{
    /// <summary>
    ///     A Model binder that can override most of the actions.
    /// </summary>
    /// <remarks>
    ///     Left this in root because it is more generic. Other, more specific binders should be in the Binders namespace.
    /// </remarks>
    public abstract class CustomModelBinder : IModelBinder
    {
        /// <summary>
        ///     The model binder to override.
        /// </summary>
        private readonly IModelBinder decorated;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomModelBinder"/> class.
        /// </summary>
        /// <param name="decorated">The decorated.</param>
        protected CustomModelBinder(IModelBinder decorated)
        {
            this.decorated = decorated;
        }

        /// <summary>
        /// Called just before binding with the decorated binder.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="bindingContext">The binding context.</param>
        public abstract void BeforeBind(ControllerContext controllerContext, ModelBindingContext bindingContext);
        /// <summary>
        /// Called just after binding with the decorated binder.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="bindingContext">The binding context.</param>
        /// <param name="result">The result of the bind as a reference.</param>
        public abstract void AfterBind(ControllerContext controllerContext, ModelBindingContext bindingContext, ref object result);

        /// <summary>
        /// Binds the model to a value by using the specified controller context and binding context.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="bindingContext">The binding context.</param>
        /// <returns>
        /// The bound value.
        /// </returns>
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            this.BeforeBind(controllerContext, bindingContext);
            object result = this.decorated.BindModel(controllerContext, bindingContext);
            this.AfterBind(controllerContext, bindingContext, ref result);
            return result;
        }
    }
}
