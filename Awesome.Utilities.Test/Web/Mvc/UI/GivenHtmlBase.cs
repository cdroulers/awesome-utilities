using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace Awesome.Utilities.Test.Web.Mvc.UI
{
    public abstract class GivenHtmlBase
    {
        protected HtmlNode GetHtmlNode(string html)
        {
            Trace.WriteLine("Loading the following HTML: " + Environment.NewLine + html);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            return doc.DocumentNode.FirstChild;
        }
    }
}
