using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

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
        /// <typeparam name="K">The key type.</typeparam>
        /// <typeparam name="V">The value type.</typeparam>
        /// <param name="self">The self.</param>
        /// <param name="keyFunc">The key function.</param>
        /// <param name="valueFunc">The value function.</param>
        /// <returns>A name value collection from the dictionary.</returns>
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
        /// <typeparam name="K">The key type.</typeparam>
        /// <typeparam name="V">The value type.</typeparam>
        /// <param name="self">The self.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value from the dictionary or a default value.</returns>
        public static V GetValueOrDefault<K, V>(this IDictionary<K, V> self, K key, V defaultValue = default(V))
        {
            V value;
            return self.TryGetValue(key, out value) ? value : defaultValue;
        }
    }
}
