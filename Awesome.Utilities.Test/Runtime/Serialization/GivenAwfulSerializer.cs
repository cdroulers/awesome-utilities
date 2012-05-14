using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Globalization;

namespace Awesome.Utilities.Test.Runtime.Serialization
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenAwfulSerializer
    {
        private class TestClass
        {
            public string StringValue { get; set; }
            public int IntValue { get; set; }
            public DateTime DateTimeValue { get; set; }
            public string[] StringArray { get; set; }
            public List<string> StringList { get; set; }
            public IDictionary<string, MailAddress> Dictionary { get; set; }
            public IDictionary<SecondClass, SecondClass> DictionaryComplex { get; set; }
            public IDictionary<string, SecondClass> DictionaryComplex2 { get; set; }
        }

        private class SecondClass
        {
            public string One { get; set; }
        }

        [Test]
        public void When_serializing_Then_outputs_right_formatted_text()
        {
            using (Culture.As(CultureInfo.InvariantCulture))
            {
                var serializer = new AwfulSerializer("  ").AddStringableType<MailAddress>();

                string actual = serializer.Serialize(new TestClass()
                {
                    StringValue = "SOME STRING TEXT HERE",
                    IntValue = 1,
                    DateTimeValue = DateTime.Parse("2012-04-26 14:59:00"),
                    StringArray = new[] { "Text one", "Text two", "Text three" },
                    StringList = new List<string>() { "Text one", null, "Text three" },
                    Dictionary = new Dictionary<string, MailAddress>() { { "Clé one", new MailAddress("valueone@example.org") }, { "Clé two", new MailAddress("keytwo@example.org") }, { "Clé three", null } },
                    DictionaryComplex = new Dictionary<SecondClass, SecondClass>() { { new SecondClass() { One = "Clé one string" }, new SecondClass() { One = "Valeur one string" } } },
                    DictionaryComplex2 = new Dictionary<string, SecondClass>() { { "Clé one string", new SecondClass() { One = "Valeur one string" } } },
                });

                Assert.That(actual, Is.Not.Null);
                Assert.That(actual, Is.EqualTo(@"TestClass
  StringValue (String) => SOME STRING TEXT HERE
  IntValue (Int32) => 1
  DateTimeValue (DateTime) => 04/26/2012 14:59:00
  StringArray (String[]) => 
    0    => Text one
    1    => Text two
    2    => Text three
  StringList (List<String>) => 
    0    => Text one
    1    => NULL
    2    => Text three
  Dictionary (IDictionary<String, MailAddress>) => 
    Clé one => valueone@example.org
    Clé two => keytwo@example.org
    Clé three => NULL
  DictionaryComplex (IDictionary<SecondClass, SecondClass>) => 
    Key 
      One (String) => Clé one string
    Value
      One (String) => Valeur one string
  DictionaryComplex2 (IDictionary<String, SecondClass>) => 
    Key  => Clé one string
    Value
      One (String) => Valeur one string"));
            }
        }

        [Test]
        public void When_serializing_dictionary_Then_outputs_right_formatted_text()
        {
            using (Culture.As(CultureInfo.InvariantCulture))
            {
                var serializer = new AwfulSerializer("  ");

                string actual = serializer.Serialize(new Dictionary<string, SecondClass>()
                {
                    { "Clé one", new SecondClass() { One = "Second class valeur one" } },
                    { "Clé two", new SecondClass() { One = "Second class valeur two" } },
                });

                Assert.That(actual, Is.Not.Null);
                Assert.That(actual, Is.EqualTo(@"Dictionary<String, SecondClass>
  Key  => Clé one
  Value
    One (String) => Second class valeur one
  Key  => Clé two
  Value
    One (String) => Second class valeur two"));
            }
        }

        [Test]
        public void When_enum_Then_outputs_right_formatted_text()
        {
            using (Culture.As(CultureInfo.InvariantCulture))
            {
                var serializer = new AwfulSerializer("  ");

                string actual = serializer.Serialize(new Dictionary<string, StringComparison>()
                {
                    { "Clé one", StringComparison.CurrentCulture },
                });

                Assert.That(actual, Is.Not.Null);
                Assert.That(actual, Is.EqualTo(@"Dictionary<String, StringComparison>
  Clé one => CurrentCulture"));
            }
        }

        [Test]
        public void When_recursion_Then_doesnt_explode()
        {
            using (Culture.As(CultureInfo.InvariantCulture))
            {
                var serializer = new AwfulSerializer("  ");
                serializer.StringableTypes.Remove(typeof(CultureInfo));

                string actual = serializer.Serialize(new CultureInfo("en-US"));

                Assert.That(actual, Is.Not.Null);
                Assert.That(actual, Contains.Substring(@"Parent (CultureInfo) =>   RECURSION DETECTED!"));
            }
        }
    }
}
