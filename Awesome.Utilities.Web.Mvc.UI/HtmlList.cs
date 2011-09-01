using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Awesome.Utilities.Web.Mvc.UI
{
    /// <summary>
    ///     A generic html list.
    /// </summary>
    /// <typeparam name="T">The type of the object</typeparam>
    public class HtmlList<T> : Control
    {
        /// <summary>
        ///     The kind of this list
        /// </summary>
        public HtmlListKind Kind { get; set; }

        /// <summary>
        ///     The items that are in this list
        /// </summary>
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        ///     The function that creates the data in the li items
        /// </summary>
        public Func<T, string> Func { get; set; }

        /// <summary>
        ///     Gets or sets the class func.
        /// </summary>
        /// <value>The class func.</value>
        public Func<T, string> ClassFunc { get; set; }

        /// <summary>
        ///     Creates an html list
        /// </summary>
        /// <param name="kind">The kind.</param>
        /// <param name="items">The items.</param>
        /// <param name="func">The func.</param>
        public HtmlList(HtmlListKind kind, IEnumerable<T> items, Func<T, string> func) : this(kind, items, func, null) { }

        /// <summary>
        ///     Creates an html list
        /// </summary>
        /// <param name="kind">The kind of list to create</param>
        /// <param name="items">The items of the list</param>
        /// <param name="func">The method to apply to each item</param>
        /// <param name="classFunc">The class func.</param>
        public HtmlList(HtmlListKind kind, IEnumerable<T> items, Func<T, string> func, Func<T, string> classFunc)
            : base(kind == HtmlListKind.Ordered ? HtmlTextWriterTag.Ol : HtmlTextWriterTag.Ul, false)
        {
            this.Kind = kind;
            this.Items = items;
            this.Func = func;
            this.ClassFunc = classFunc;
        }

        /// <summary>
        ///     Renders the items
        /// </summary>
        /// <param name="htmlTextWriter">The HtmlTextWriter to output to</param>
        protected override void RenderContents(HtmlTextWriter htmlTextWriter)
        {
            IEnumerator enumerator = this.Items.GetEnumerator();
            while (enumerator.MoveNext())
            {
                T current = (T)enumerator.Current;
                if (this.ClassFunc != null)
                {
                    string cssClass = this.ClassFunc(current);
                    if (!string.IsNullOrEmpty(cssClass))
                    {
                        htmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Class, cssClass);
                    }
                }
                htmlTextWriter.RenderBeginTag("li");

                htmlTextWriter.Write(this.Func(current) ?? string.Empty);

                htmlTextWriter.WriteEndTag("li");
                htmlTextWriter.WriteLine();
            }
        }
    }
}
