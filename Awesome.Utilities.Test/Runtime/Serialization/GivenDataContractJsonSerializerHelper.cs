using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using NUnit.Framework;
using System.Runtime.Serialization.Json;

namespace Awesome.Utilities.Test.Runtime.Serialization
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenDataContractJsonSerializerHelper
    {
        [DataContract(Namespace = "")]
        private class TestClass
        {
            [DataMember]
            public string Value { get; set; }
        }

        [Test]
        public void When_serializing_Then_works()
        {
            string result = DataContractJsonSerializerHelper.Serialize(new TestClass() { Value = "Wot" });

            Assert.That(result, Is.EqualTo(@"{""Value"":""Wot""}"));
        }

        [Test]
        public void When_deserializing_Then_works()
        {
            var result = DataContractJsonSerializerHelper.Deserialize<TestClass>(@"{""Value"":""Wot""}");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo("Wot"));
        }
    }
}
