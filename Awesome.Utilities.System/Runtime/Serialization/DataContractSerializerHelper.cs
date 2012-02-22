using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace System.Runtime.Serialization
{
    /// <summary>
    ///     Data Contract serializer helper
    /// </summary>
    public static class DataContractSerializerHelper
    {
        /// <summary>
        /// Serializes the specified graph.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graph">The graph.</param>
        /// <param name="omitXmlDeclaration">if set to <c>true</c> [omit XML declaration].</param>
        /// <param name="indent">if set to <c>true</c> [indent].</param>
        /// <returns></returns>
        public static string Serialize<T>(T graph, bool omitXmlDeclaration = false, bool indent = true)
        {
            var data = new DataContractSerializer(typeof(T));

            using (var memory = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(memory, new XmlWriterSettings
                {
                    OmitXmlDeclaration = omitXmlDeclaration,
                    NamespaceHandling = NamespaceHandling.OmitDuplicates,
                    Indent = indent
                }))
                {
                    data.WriteObject(writer, graph);
                }
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
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        public static T Deserialize<T>(string xml)
        {
            var data = new DataContractSerializer(typeof(T));

            using (var memory = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                return (T)data.ReadObject(memory);
            }
        }
    }
}
