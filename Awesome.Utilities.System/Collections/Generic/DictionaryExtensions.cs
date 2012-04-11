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

        /// <summary>
        /// Gets the value for the specified key, or a specified default.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="self">The self.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static V GetValueOrDefault<K, V>(this IDictionary<K, V> self, K key, V defaultValue = default(V))
        {
            V value;
            return self.TryGetValue(key, out value) ? value : defaultValue;
        }
    }
}
