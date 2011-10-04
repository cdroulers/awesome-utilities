using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Web.Mvc.UI
{
    /// <summary>
    ///     Extensions to allow easier creation of some things directly in views without a new Blah()
    /// </summary>
    public static class HtmlHelperUIExtensions
    {

        /// <summary>
        /// Generates the pages for the specified ResultPage.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self">The self.</param>
        /// <param name="pagedList">The paged list.</param>
        /// <param name="pageFunc">The page func.</param>
        /// <returns></returns>
        public static MvcHtmlString GeneratePages<T>(this HtmlHelper self, ResultPage<T> pagedList, Func<PageData, MvcHtmlString> pageFunc)
        {
            return self.GeneratePages<T>(pagedList, pageFunc, PageGenerator<T>.DefaultMaximumNumberOfPagesToShow.GetValueOrDefault(pagedList.LastPage));
        }

        /// <summary>
        /// Generates the pages for the specified ResultPage.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self">The self.</param>
        /// <param name="pagedList">The paged list.</param>
        /// <param name="pageFunc">The page func.</param>
        /// <param name="maximumNumberOfPagesToShow">The maximum number of pages to show.</param>
        /// <returns></returns>
        public static MvcHtmlString GeneratePages<T>(this HtmlHelper self, ResultPage<T> pagedList, Func<PageData, MvcHtmlString> pageFunc, int maximumNumberOfPagesToShow)
        {
            Validate.Is.NotNull(pagedList, "pagedList");
            Validate.Is.NotNull(pageFunc, "pageFunc");

            return new PageGenerator<T>(pagedList, pageFunc, maximumNumberOfPagesToShow).ToHtmlString();
        }

        /// <summary>
        /// Creates a submit button.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static MvcHtmlString Submit(this HtmlHelper self, string text = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                text = Control.TranslationDelegate("Save");
            }
            return new LiteralControl(Web.UI.HtmlTextWriterTag.Input, isSelfClosing: true).AddAttribute("type", "submit").AddAttribute("value", text).ToHtmlString();
        }

        /// <summary>
        /// Creates a submit button followed by a cancel link [rel='cancel'].
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="cancelUrl">The cancel URL.</param>
        /// <param name="text">The text.</param>
        /// <param name="cancelText">The cancel text.</param>
        /// <param name="cancelFirst">if set to <c>true</c> [cancel first].</param>
        /// <returns></returns>
        public static MvcHtmlString SubmitCancel(this HtmlHelper self, string cancelUrl, string text = null, string cancelText = null, bool cancelFirst = false)
        {
            Validate.Is.NotNullOrWhiteSpace(cancelUrl, "cancelUrl");

            if (string.IsNullOrEmpty(text))
            {
                text = Control.TranslationDelegate("Save");
            }
            if (string.IsNullOrEmpty(cancelText))
            {
                cancelText = Control.TranslationDelegate("Cancel");
            }
            var submit = new LiteralControl(Web.UI.HtmlTextWriterTag.Input, isSelfClosing: true).AddAttribute("type", "submit").AddAttribute("value", text);
            var cancel = new A(cancelText, cancelUrl) { Rel = "cancel" };
            return cancelFirst ? MvcHtmlString.Create(cancel.ToString() + submit.ToString()) : MvcHtmlString.Create(submit.ToString() + cancel.ToString());
        }
    }
}
