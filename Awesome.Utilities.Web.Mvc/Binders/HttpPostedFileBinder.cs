using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Web.Mvc.Binders
{
    /// <summary>
    ///     A Binder for HTTP posted files. With options to look for Data URI files.
    /// </summary>
    public class HttpPostedFileBinder : DefaultModelBinder
    {
        /// <summary>
        ///     If set to true, will look for a Data URI uploaded file.
        /// </summary>
        public readonly bool AllowDataUriFiles;

        /// <summary>
        ///     The name of the Data URI field.
        /// </summary>
        public readonly string DataUriPropertyName;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpPostedFileBinder"/> class.
        /// </summary>
        /// <param name="allowDataUriFiles">if set to <c>true</c> [allow data URI files].</param>
        /// <param name="dataUriPropertyName">Name of the data URI property.</param>
        public HttpPostedFileBinder(bool allowDataUriFiles, string dataUriPropertyName)
        {
            this.AllowDataUriFiles = allowDataUriFiles;
            if (this.AllowDataUriFiles)
            {
                Validate.Is.Not.NullOrWhiteSpace(dataUriPropertyName, "dataUriPropertyName");
            }
            this.DataUriPropertyName = dataUriPropertyName;
        }

        /// <summary>
        /// Binds the model by using the specified controller context and binding context.
        /// </summary>
        /// <param name="controllerContext">The context within which the controller operates. The context information includes the controller, HTTP content, request context, and route data.</param>
        /// <param name="bindingContext">The context within which the model is bound. The context includes information such as the model object, model name, model type, property filter, and value provider.</param>
        /// <returns>
        /// The bound object.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="bindingContext "/>parameter is null.</exception>
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(HttpPostedFileBase))
            {
                throw new ArgumentException("This binder only works for HttpPostedFileBase.");
            }

            if (bindingContext.ValueProvider.GetValue(bindingContext.ModelName) != null)
            {
                var result = base.BindModel(controllerContext, bindingContext);

                if (result != null)
                {
                    return result;
                }
            }

            if (this.AllowDataUriFiles)
            {
                var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + "." + this.DataUriPropertyName);
                if (value != null)
                {
                    return DataUriPostedFile.Parse(value.AttemptedValue);
                }
            }

            return null;
        }
    }
}
