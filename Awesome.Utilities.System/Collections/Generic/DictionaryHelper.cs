﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace System.Collections.Generic
{
    /// <summary>
    ///     Helper methods for dictionaries.
    /// </summary>
    public static class DictionaryHelper
    {
        /// <summary>
        /// Builds a dictionary from an anonymous object.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static IDictionary<string, object> BuildDictionaryFromAnonymousObject(object obj)
        {
            var result = new Dictionary<string, object>();
            if (obj != null)
            {
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(obj);
                foreach (PropertyDescriptor prop in props)
                {
                    object val = prop.GetValue(obj);
                    result.Add(prop.Name, val);
                }
            }
            return result;
        }
    }
}