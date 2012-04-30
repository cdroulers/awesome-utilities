using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

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
            Validate.Is.Not.Null(pagedList, "pagedList");
            Validate.Is.Not.Null(pageFunc, "pageFunc");

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
            Validate.Is.Not.NullOrWhiteSpace(cancelUrl, "cancelUrl");

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

        /// <summary>
        /// Creates an HTML attribute object to easily build HTML attributes.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="displayEmptyValue">if set to <c>true</c> [display empty value].</param>
        /// <returns></returns>
        public static HtmlAttribute Attr(this HtmlHelper self, string name, string value, bool displayEmptyValue = false)
        {
            return new HtmlAttribute(name, value, displayEmptyValue);
        }

        /// <summary>
        /// Creates an HTML string containing all defined attributes in a dictionary
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static IHtmlString Attrs(this HtmlHelper self, object htmlAttributes)
        {
            if (htmlAttributes == null)
            {
                return MvcHtmlString.Empty;
            }
            return self.Attrs(new RouteValueDictionary(htmlAttributes));
        }

        /// <summary>
        /// Creates an HTML string containing all defined attributes in a dictionary
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static IHtmlString Attrs(this HtmlHelper self, IDictionary<string, object> htmlAttributes)
        {
            if (htmlAttributes == null)
            {
                return MvcHtmlString.Empty;
            }
            return self.Attrs(new RouteValueDictionary(htmlAttributes));
        }

        /// <summary>
        /// Creates an HTML string containing all defined attributes in a dictionary
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static IHtmlString Attrs(this HtmlHelper self, RouteValueDictionary htmlAttributes)
        {
            if (htmlAttributes == null)
            {
                return MvcHtmlString.Empty;
            }
            var builder = new StringBuilder();
            foreach (KeyValuePair<string, object> value in htmlAttributes)
            {
                builder.Append(new HtmlAttribute(value.Key.Replace("_", "-"), value.Value == null ? string.Empty : value.Value.ToString()) + " ");
            }
            return MvcHtmlString.Create(builder.ToString().Trim());
        }
    }
}
