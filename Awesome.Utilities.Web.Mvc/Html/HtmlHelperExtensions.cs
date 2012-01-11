using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Web.Mvc.Html
{
    /// <summary>
    ///     Extensions for an HtmlHelper
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Gets the name of the input. Should be used only inside Editor or display templates.
        /// </summary>
        public static string GetCurrentInputName(this HtmlHelper self)
        {
            return self.Hidden("").ToString().Split(new string[] { "name=\"" }, StringSplitOptions.RemoveEmptyEntries)[1].Split('"')[0];
        }
        
        /// <summary>
        /// Clears the model state for the current property. Should be used only inside Editor or display templates.
        /// </summary>
        public static void ClearCurrentModelState(this HtmlHelper self)
        {
            self.ViewData.ModelState[self.GetCurrentInputName()] = new ModelState();
        }
    }
}
