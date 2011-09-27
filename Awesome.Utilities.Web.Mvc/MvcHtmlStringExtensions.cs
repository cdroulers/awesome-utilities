using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Web.Mvc
{
    public static class MvcHtmlStringExtensions
    {
        public static MvcHtmlString Add(this MvcHtmlString self, MvcHtmlString other, bool addSpace = false)
        {
            return self.Add(other.ToString(), addSpace);
        }

        public static MvcHtmlString Add(this MvcHtmlString self, string other, bool addSpace = false)
        {
            return MvcHtmlString.Create(self.ToString() + (addSpace ? " " : "") + other);
        }
    }
}
