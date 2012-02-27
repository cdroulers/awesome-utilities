using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace System.Runtime.Serialization.Json
{
    /// <summary>
    ///     Data Contract serializer helper
    /// </summary>
    public static class DataContractJsonSerializerHelper
    {
        /// <summary>
        /// Serializes the specified graph.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graph">The graph.</param>
        /// <param name="indent">if set to <c>true</c> [indent].</param>
        /// <returns></returns>
        public static string Serialize<T>(T graph, bool indent = true)
        {
            var data = new DataContractJsonSerializer(typeof(T));

            using (var memory = new MemoryStream())
            {
                data.WriteObject(memory, graph);
                memory.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(memory))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Deserializes the specified graph.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">The json.</param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            var data = new DataContractJsonSerializer(typeof(T));

            using (var memory = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                return (T)data.ReadObject(memory);
            }
        }
    }
}
