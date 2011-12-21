using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Web.Mvc.UI
{
    /// <summary>
    ///     Data used in the loop of a page generator
    /// </summary>
    public struct PageData
    {
        /// <summary>
        ///     The text for this particular link
        /// </summary>
        public readonly string Text;
        /// <summary>
        ///     The page the link is for
        /// </summary>
        public readonly int Page;

        /// <summary>
        /// Initializes a new instance of the <see cref="PageData"/> struct.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="page">The page.</param>
        internal PageData(string text, int page)
        {
            this.Text = text;
            this.Page = page;
        }
    }
}
