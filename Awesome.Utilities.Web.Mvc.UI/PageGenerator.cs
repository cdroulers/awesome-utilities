using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace System.Web.Mvc.UI
{
    /// <summary>
    ///     A generator of pages.
    /// </summary>
    public class PageGenerator<T> : Control
    {
        public readonly ResultPage<T> Items;
        public readonly Func<PageData, MvcHtmlString> PageFunc;
        public readonly int MaximumNumberOfPagesToShow;
        public string TextBefore { get; set; }
        public string TextAfter { get; set; }

        public PageGenerator(ResultPage<T> items, Func<PageData, MvcHtmlString> pageFunc)
            : this(items, pageFunc, items.LastPage)
        {
        }
        public PageGenerator(ResultPage<T> items, Func<PageData, MvcHtmlString> pageFunc, int maximumNumberOfPagesToShow)
            : base(HtmlTextWriterTag.Span, false)
        {
            this.ControlCssClass = "pager";
            this.Items = items;
            this.PageFunc = pageFunc;
            this.MaximumNumberOfPagesToShow = maximumNumberOfPagesToShow;
            this.TextBefore = "[";
            this.TextAfter = "]";
        }

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
                string text = this.TextBefore + i.ToString() + this.TextAfter;
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
