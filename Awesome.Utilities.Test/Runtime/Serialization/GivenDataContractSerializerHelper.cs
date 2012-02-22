using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Runtime.Serialization;

namespace Awesome.Utilities.Test.Runtime.Serialization
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenDataContractSerializerHelper
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
            string result = DataContractSerializerHelper.Serialize(new TestClass() { Value = "Wot" });

            Assert.That(result, Is.EqualTo(@"<?xml version=""1.0"" encoding=""utf-8""?>
<GivenDataContractSerializerHelper.TestClass xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"">
  <Value>Wot</Value>
</GivenDataContractSerializerHelper.TestClass>"));
        }

        [Test]
        public void When_deserializing_Then_works()
        {
            var result = DataContractSerializerHelper.Deserialize<TestClass>(@"<GivenDataContractSerializerHelper.TestClass>
  <Value>Wot</Value>
</GivenDataContractSerializerHelper.TestClass>");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo("Wot"));
        }
    }
}
