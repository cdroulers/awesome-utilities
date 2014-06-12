using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace System.Runtime.Serialization
{
    /// <summary>
    ///     Easy access to serializers.
    /// </summary>
    public static class Serializers
    {
        private static AwfulSerializer awfulSerializer = new AwfulSerializer("  ");
        private static DataContractSerializerHelper dataContractSerializerHelper = new DataContractSerializerHelper();
        private static DataContractJsonSerializerHelper dataContractJsonSerializerHelper = new DataContractJsonSerializerHelper();

        /// <summary>
        /// Gets the current awful serializer
        /// </summary>
        public static AwfulSerializer Awful
        {
            get { return Serializers.awfulSerializer; }
        }

        /// <summary>
        /// Gets the current data contract serializer helper.
        /// </summary>
        public static DataContractSerializerHelper DataContract
        {
            get { return Serializers.dataContractSerializerHelper; }
        }

        /// <summary>
        /// Gets the current data contract serializer helper.
        /// </summary>
        public static DataContractJsonSerializerHelper DataContractJson
        {
            get { return Serializers.dataContractJsonSerializerHelper; }
        }

        /// <summary>
        /// Overrides the default awful serializer.
        /// </summary>
        /// <param name="serializer">The serializer.</param>
        public static void Override(AwfulSerializer serializer)
        {
            Validate.Is.Not.Null(awfulSerializer, "awfulSerializer");
            Serializers.awfulSerializer = serializer;
        }

        /// <summary>
        /// Overrides the default data contract serializer.
        /// </summary>
        /// <param name="serializer">The data contract.</param>
        public static void Override(DataContractSerializerHelper serializer)
        {
            Validate.Is.Not.Null(dataContractSerializerHelper, "dataContractSerializerHelper");
            Serializers.dataContractSerializerHelper = serializer;
        }

        /// <summary>
        /// Overrides the default data contract json serializer.
        /// </summary>
        /// <param name="serializer">The data contract json serializer helper.</param>
        public static void Override(DataContractJsonSerializerHelper serializer)
        {
            Validate.Is.Not.Null(dataContractJsonSerializerHelper, "dataContractJsonSerializerHelper");
            Serializers.dataContractJsonSerializerHelper = serializer;
        }
    }
}
