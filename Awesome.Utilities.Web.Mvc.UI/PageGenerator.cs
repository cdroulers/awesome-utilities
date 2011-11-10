using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace System.Web.Mvc.UI
{
    /// <summary>
    /// HTML pager generator.
    /// </summary>
    public abstract class PageGenerator : Control
    {
        /// <summary>
        /// Gets or sets the hide if empty.
        /// </summary>
        public static bool? HideIfEmpty { get; set; }
        /// <summary>
        /// Gets or sets the default maximum number of pages to show.
        /// </summary>
        public static int? DefaultMaximumNumberOfPagesToShow { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageGenerator"/> class.
        /// </summary>
        protected PageGenerator()
            : base(HtmlTextWriterTag.Span, false)
        {
        }
    }

    /// <summary>
    ///     A generator of pages.
    /// </summary>
    public class PageGenerator<T> : PageGenerator
    {
        /// <summary>
        /// Gets or sets the Items.
        /// </summary>
        public readonly ResultPage<T> Items;
        /// <summary>
        /// Gets or sets the PageFunc.
        /// </summary>
        public readonly Func<PageData, MvcHtmlString> PageFunc;
        /// <summary>
        /// Gets or sets the MaximumNumberOfPagesToShow.
        /// </summary>
        public readonly int MaximumNumberOfPagesToShow;
        /// <summary>
        /// Gets or sets the text before.
        /// </summary>
        public static string TextBefore { get; set; }
        /// <summary>
        /// Gets or sets the text after.
        /// </summary>
        public static string TextAfter { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageGenerator&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="pageFunc">The page func.</param>
        public PageGenerator(ResultPage<T> items, Func<PageData, MvcHtmlString> pageFunc)
            : this(items, pageFunc, DefaultMaximumNumberOfPagesToShow.GetValueOrDefault(items.LastPage))
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PageGenerator&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="pageFunc">The page func.</param>
        /// <param name="maximumNumberOfPagesToShow">The maximum number of pages to show.</param>
        public PageGenerator(ResultPage<T> items, Func<PageData, MvcHtmlString> pageFunc, int maximumNumberOfPagesToShow)
            : base()
        {
            this.ControlCssClass = "pager";
            this.Items = items;
            this.PageFunc = pageFunc;
            this.MaximumNumberOfPagesToShow = maximumNumberOfPagesToShow;
        }

        /// <summary>
        /// Sets the static variable for text after and before!
        /// </summary>
        /// <param name="before">The before.</param>
        /// <param name="after">The after.</param>
        public static void Text(string before, string after)
        {
            TextBefore = before;
            TextAfter = after;
        }

        /// <summary>
        /// The basic rendering of the Control (i.e. the opening tag, call render content, close tag)
        /// </summary>
        /// <param name="htmlTextWriter">The writer to write to.</param>
        protected override void Render(HtmlTextWriter htmlTextWriter)
        {
            if (HideIfEmpty.GetValueOrDefault(false) && this.Items.LastPage <= 1)
            {
                return;
            }
            base.Render(htmlTextWriter);
        }

        /// <summary>
        /// Render the contents of the control
        /// </summary>
        /// <param name="htmlTextWriter">The writer to write to</param>
        protected override void RenderContents(HtmlTextWriter htmlTextWriter)
        {
            int diff = this.MaximumNumberOfPagesToShow / 2;
            int min = this.Items.CurrentPage - diff;
            int max = this.Items.CurrentPage + diff;
            if (this.MaximumNumberOfPagesToShow >= this.Items.LastPage)
            {
                max = this.Items.LastPage;
                min = ResultPage<T>.ValueOfFirstPage;
            }
            if (min < ResultPage<T>.ValueOfFirstPage)
            {
                min = ResultPage<T>.ValueOfFirstPage;
                max = Math.Min(this.MaximumNumberOfPagesToShow, this.Items.LastPage);
            }
            if (max > this.Items.LastPage)
            {
                max = this.Items.LastPage;
                min = Math.Max(Math.Abs(this.Items.LastPage - this.MaximumNumberOfPagesToShow), ResultPage<T>.ValueOfFirstPage);
            }

            if (min > ResultPage<T>.ValueOfFirstPage)
            {
                htmlTextWriter.WriteLine(this.PageFunc(new PageData(Control.TranslationDelegate("FirstPage"), ResultPage<T>.ValueOfFirstPage)));
            }

            if (this.Items.CurrentPage > ResultPage<T>.ValueOfFirstPage)
            {
                htmlTextWriter.WriteLine(this.PageFunc(new PageData(Control.TranslationDelegate("PreviousPage"), this.Items.CurrentPage - 1)));
            }
            if (min > ResultPage<T>.ValueOfFirstPage)
            {
                htmlTextWriter.WriteLine(@"<span class=""first-page-separator"">...</span>");
            }

            for (int i = min; i <= max; i++)
            {
                string text = TextBefore + i.ToString() + TextAfter;
                if (this.Items.CurrentPage == i)
                {
                    htmlTextWriter.WriteLine(@"<span class=""current-page"">" + text + @"</span>");
                }
                else
                {
                    htmlTextWriter.WriteLine(this.PageFunc(new PageData(text, i)));
                }
            }

            if (max < this.Items.LastPage)
            {
                htmlTextWriter.WriteLine(@"<span class=""last-page-separator"">...</span>");
            }
            if (this.Items.CurrentPage < this.Items.LastPage)
            {
                htmlTextWriter.WriteLine(this.PageFunc(new PageData(Control.TranslationDelegate("NextPage"), this.Items.CurrentPage + 1)));
            }

            if (max < this.Items.LastPage)
            {
                htmlTextWriter.WriteLine(this.PageFunc(new PageData(Control.TranslationDelegate("LastPage"), this.Items.LastPage)));
            }
        }
    }
}
