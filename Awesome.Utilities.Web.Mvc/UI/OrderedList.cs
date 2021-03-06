﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Web.Mvc.UI
{
    /// <summary>
    ///     An ordered list of items strongly typed
    /// </summary>
    /// <typeparam name="T">The type of the items</typeparam>
    public class OrderedList<T> : HtmlList<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderedList&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="func">The func.</param>
        public OrderedList(IEnumerable<T> items, Func<T, string> func) : this(items, func, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderedList&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="func">The func.</param>
        /// <param name="classFunc">The class func.</param>
        public OrderedList(IEnumerable<T> items, Func<T, string> func, Func<T, string> classFunc) : base(HtmlListKind.Ordered, items, func, classFunc) { }
    }
}
