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
            return self.GeneratePages<T>(pagedList, pageFunc, pagedList.LastPage);
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
            return new PageGenerator<T>(pagedList, pageFunc, maximumNumberOfPagesToShow).ToHtmlString();
        }
    }
}
