using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Web.Mvc.Binders
{
    /// <summary>
    ///     A custom model binder that removes the model state from the list after binding.
    /// </summary>
    public class NoModelStateModelBinder : CustomModelBinder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoModelStateModelBinder"/> class.
        /// </summary>
        /// <param name="decorated">The decorated.</param>
        public NoModelStateModelBinder(IModelBinder decorated)
            : base(decorated)
        {
        }

        /// <summary>
        /// Called just before binding with the decorated binder.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="bindingContext">The binding context.</param>
        public override void BeforeBind(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
        }

        /// <summary>
        /// Called just after binding with the decorated binder.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="bindingContext">The binding context.</param>
        /// <param name="result">The result of the bind as a reference.</param>
        public override void AfterBind(ControllerContext controllerContext, ModelBindingContext bindingContext, ref object result)
        {
            bindingContext.ModelState.Remove(bindingContext.ModelName);
        }
    }
}
