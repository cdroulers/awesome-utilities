using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;

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
        public static AwfulSerializer Awful { get { return Serializers.awfulSerializer; } }

        /// <summary>
        /// Gets the current data contract serializer helper.
        /// </summary>
        public static DataContractSerializerHelper DataContract { get { return Serializers.dataContractSerializerHelper; } }

        /// <summary>
        /// Gets the current data contract serializer helper.
        /// </summary>
        public static DataContractJsonSerializerHelper DataContractJson { get { return Serializers.dataContractJsonSerializerHelper; } }

        /// <summary>
        /// Overrides the default awful serializer.
        /// </summary>
        /// <param name="awfulSerializer">The awful serializer.</param>
        public static void Override(AwfulSerializer awfulSerializer)
        {
            Validate.Is.Not.Null(awfulSerializer, "awfulSerializer");
            Serializers.awfulSerializer = awfulSerializer;
        }

        /// <summary>
        /// Overrides the default data contract serializer.
        /// </summary>
        /// <param name="dataContractSerializerHelper">The data contract.</param>
        public static void Override(DataContractSerializerHelper dataContractSerializerHelper)
        {
            Validate.Is.Not.Null(dataContractSerializerHelper, "dataContractSerializerHelper");
            Serializers.dataContractSerializerHelper = dataContractSerializerHelper;
        }

        /// <summary>
        /// Overrides the default data contract json serializer.
        /// </summary>
        /// <param name="dataContractJsonSerializerHelper">The data contract json serializer helper.</param>
        public static void Override(DataContractJsonSerializerHelper dataContractJsonSerializerHelper)
        {
            Validate.Is.Not.Null(dataContractJsonSerializerHelper, "dataContractJsonSerializerHelper");
            Serializers.dataContractJsonSerializerHelper = dataContractJsonSerializerHelper;
        }
    }
}
