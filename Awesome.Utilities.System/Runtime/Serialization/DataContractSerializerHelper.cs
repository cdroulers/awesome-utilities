using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace System.Runtime.Serialization
{
    /// <summary>
    ///     Data Contract serializer helper
    /// </summary>
    public class DataContractSerializerHelper
    {
        /// <summary>
        /// Gets or sets a value indicating whether to omit the XML declaration.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [omit XML declaration]; otherwise, <c>false</c>.
        /// </value>
        public bool OmitXmlDeclaration { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DataContractSerializerHelper"/> is indent.
        /// </summary>
        /// <value>
        ///   <c>true</c> if indent; otherwise, <c>false</c>.
        /// </value>
        public bool Indent { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContractSerializerHelper"/> class.
        /// </summary>
        /// <param name="omitXmlDeclaration">if set to <c>true</c> [omit XML declaration].</param>
        /// <param name="indent">if set to <c>true</c> [indent].</param>
        public DataContractSerializerHelper(bool omitXmlDeclaration = false, bool indent = true)
        {
            this.OmitXmlDeclaration = omitXmlDeclaration;
            this.Indent = indent;
        }

        /// <summary>
        /// Serializes the specified graph.
        /// </summary>
        /// <typeparam name="T">The type to serialize</typeparam>
        /// <param name="graph">The graph.</param>
        /// <returns>A serialized string</returns>
        public virtual string Serialize<T>(T graph)
        {
            var data = new DataContractSerializer(typeof(T));

            using (var memory = new MemoryStream())
            {
                var settings = new XmlWriterSettings
                {
                    OmitXmlDeclaration = this.OmitXmlDeclaration,
                    NamespaceHandling = NamespaceHandling.OmitDuplicates,
                    Indent = this.Indent
                };
                using (var writer = XmlWriter.Create(memory, settings))
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
        /// <typeparam name="T">The type to serialize</typeparam>
        /// <param name="xml">The XML.</param>
        /// <returns>A deserialized object.</returns>
        public virtual T Deserialize<T>(string xml)
        {
            var data = new DataContractSerializer(typeof(T));

            using (var memory = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                return (T)data.ReadObject(memory);
            }
        }
    }
}
