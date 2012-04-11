using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace System.Collections.Generic
{
    /// <summary>
    ///     Extensions for Dictionaries
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Transforms a dictionary into a key value collection using the specified functions.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="self">The self.</param>
        /// <param name="keyFunc">The key func.</param>
        /// <param name="valueFunc">The value func.</param>
        /// <returns></returns>
        public static NameValueCollection ToNameValueCollection<K, V>(this IDictionary<K, V> self, Func<K, string> keyFunc = null, Func<V, string> valueFunc = null)
        {
            keyFunc = keyFunc ?? (k => k.ToString());
            valueFunc = valueFunc ?? (v => v.ToString());
            var result = new NameValueCollection();
            foreach (var kv in self)
            {
                result[keyFunc(kv.Key)] = valueFunc(kv.Value);
            }
            return result;
        }
    }
}
