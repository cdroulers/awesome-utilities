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
    public class PageGenerator : Control
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
        ///     The first page in a paged list.
        /// </summary>
        public const int ValueOfFirstPage = 1;

        /// <summary>
        ///     Whatever item we are paging.
        /// </summary>
        public readonly IPageable Pageable;

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
        /// Initializes a new instance of the <see cref="PageGenerator"/> class.
        /// </summary>
        /// <param name="pageable">The pageable.</param>
        /// <param name="pageFunc">The page func.</param>
        public PageGenerator(IPageable pageable, Func<PageData, MvcHtmlString> pageFunc)
            : this(pageable, pageFunc, DefaultMaximumNumberOfPagesToShow.GetValueOrDefault(pageable.LastPage))
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PageGenerator"/> class.
        /// </summary>
        /// <param name="pageable">The pageable.</param>
        /// <param name="pageFunc">The page func.</param>
        /// <param name="maximumNumberOfPagesToShow">The maximum number of pages to show.</param>
        public PageGenerator(IPageable pageable, Func<PageData, MvcHtmlString> pageFunc, int maximumNumberOfPagesToShow)
            : base(HtmlTextWriterTag.Span, false)
        {
            this.ControlCssClass = "pager";
            this.Pageable = pageable;
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
            if (HideIfEmpty.GetValueOrDefault(false) && this.Pageable.LastPage <= 1)
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
            int min = this.Pageable.CurrentPage - diff;
            int max = this.Pageable.CurrentPage + diff;
            if (this.MaximumNumberOfPagesToShow >= this.Pageable.LastPage)
            {
                max = this.Pageable.LastPage;
                min = PageGenerator.ValueOfFirstPage;
            }
            if (min < PageGenerator.ValueOfFirstPage)
            {
                min = PageGenerator.ValueOfFirstPage;
                max = Math.Min(this.MaximumNumberOfPagesToShow, this.Pageable.LastPage);
            }
            if (max > this.Pageable.LastPage)
            {
                max = this.Pageable.LastPage;
                min = Math.Max(Math.Abs(this.Pageable.LastPage - this.MaximumNumberOfPagesToShow), PageGenerator.ValueOfFirstPage);
            }

            if (min > PageGenerator.ValueOfFirstPage)
            {
                htmlTextWriter.WriteLine(this.PageFunc(new PageData(Control.TranslationDelegate("FirstPage"), PageGenerator.ValueOfFirstPage)));
            }

            if (this.Pageable.CurrentPage > PageGenerator.ValueOfFirstPage)
            {
                htmlTextWriter.WriteLine(this.PageFunc(new PageData(Control.TranslationDelegate("PreviousPage"), this.Pageable.CurrentPage - 1)));
            }
            if (min > PageGenerator.ValueOfFirstPage)
            {
                htmlTextWriter.WriteLine(@"<span class=""first-page-separator"">...</span>");
            }

            for (int i = min; i <= max; i++)
            {
                string text = TextBefore + i.ToString() + TextAfter;
                if (this.Pageable.CurrentPage == i)
                {
                    htmlTextWriter.WriteLine(@"<span class=""current-page"">" + text + @"</span>");
                }
                else
                {
                    htmlTextWriter.WriteLine(this.PageFunc(new PageData(text, i)));
                }
            }

            if (max < this.Pageable.LastPage)
            {
                htmlTextWriter.WriteLine(@"<span class=""last-page-separator"">...</span>");
            }
            if (this.Pageable.CurrentPage < this.Pageable.LastPage)
            {
                htmlTextWriter.WriteLine(this.PageFunc(new PageData(Control.TranslationDelegate("NextPage"), this.Pageable.CurrentPage + 1)));
            }

            if (max < this.Pageable.LastPage)
            {
                htmlTextWriter.WriteLine(this.PageFunc(new PageData(Control.TranslationDelegate("LastPage"), this.Pageable.LastPage)));
            }
        }
    }
}
