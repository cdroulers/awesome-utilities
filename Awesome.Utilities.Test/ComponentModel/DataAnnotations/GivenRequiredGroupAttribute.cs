using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Awesome.Utilities.Test.ComponentModel.DataAnnotations
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenRequiredGroupAttribute
    {
        public class TestClass
        {
            [RequiredGroup("Test")]
            public long? ID { get; set; }
            [RequiredGroup("Test")]
            public string SID { get; set; }
        }
        public class TestClass2
        {
            [RequiredGroup("Test", AllowMultiple = true)]
            public long? ID { get; set; }
            [RequiredGroup("Test")]
            public string SID { get; set; }
        }
        public class TestClass3
        {
            [RequiredGroup("Test", AllowMultiple = true)]
            public long? ID { get; set; }
            [RequiredGroup("Test", AllowMultiple = true)]
            public string SID { get; set; }
        }
        public class TestClass4
        {
            [RequiredGroup("Test")]
            public long? ID { get; set; }
            [RequiredGroup("Test")]
            public string SID { get; set; }

            [RequiredGroup("Test2")]
            public long? G2ID { get; set; }
            [RequiredGroup("Test2")]
            public string G2SID { get; set; }
        }

        [Test]
        public void When_setting_none_Then_returns_false()
        {
            var obj = new TestClass();
            var results = new Collection<ValidationResult>();
            var context = new ValidationContext(obj, null, null);
            bool valid = Validator.TryValidateObject(obj, context, results, true);

            Assert.That(valid, Is.False);
            Assert.That(results, Has.Count.EqualTo(2));
        }

        [Test]
        public void When_setting_all_Then_returns_false()
        {
            var obj = new TestClass() { ID = 1, SID = "LOL" };
            var results = new Collection<ValidationResult>();
            var context = new ValidationContext(obj, null, null);
            bool valid = Validator.TryValidateObject(obj, context, results, true);

            Assert.That(valid, Is.False);
            Assert.That(results, Has.Count.EqualTo(2));
        }

        [Test]
        public void When_setting_first_Then_returns_true()
        {
            var obj = new TestClass() { ID = 1 };
            var results = new Collection<ValidationResult>();
            var context = new ValidationContext(obj, null, null);
            bool valid = Validator.TryValidateObject(obj, context, results, true);

            Assert.That(valid, Is.True);
            Assert.That(results, Has.Count.EqualTo(0));
        }

        [Test]
        public void When_setting_second_Then_returns_true()
        {
            var obj = new TestClass() { SID = "LOL" };
            var results = new Collection<ValidationResult>();
            var context = new ValidationContext(obj, null, null);
            bool valid = Validator.TryValidateObject(obj, context, results, true);

            Assert.That(valid, Is.True);
            Assert.That(results, Has.Count.EqualTo(0));
        }

        [Test]
        public void When_setting_first_with_allow_multiple_Then_returns_true()
        {
            var obj = new TestClass2() { ID = 1 };
            var results = new Collection<ValidationResult>();
            var context = new ValidationContext(obj, null, null);
            bool valid = Validator.TryValidateObject(obj, context, results, true);

            Assert.That(valid, Is.True);
            Assert.That(results, Has.Count.EqualTo(0));
        }

        [Test]
        public void When_setting_first_and_second_with_allow_multiple_Then_returns_false()
        {
            var obj = new TestClass2() { ID = 1, SID = "LOL" };
            var results = new Collection<ValidationResult>();
            var context = new ValidationContext(obj, null, null);
            bool valid = Validator.TryValidateObject(obj, context, results, true);

            Assert.That(valid, Is.False);
            Assert.That(results, Has.Count.EqualTo(1));
        }

        [Test]
        public void When_setting_first_and_second_with_allow_multiple_both_Then_returns_true()
        {
            var obj = new TestClass3() { ID = 1, SID = "LOL" };
            var results = new Collection<ValidationResult>();
            var context = new ValidationContext(obj, null, null);
            bool valid = Validator.TryValidateObject(obj, context, results, true);

            Assert.That(valid, Is.True);
            Assert.That(results, Has.Count.EqualTo(0));
        }

        [Test]
        public void When_setting_with_multiple_groups_Then_returns_true()
        {
            var obj = new TestClass4() { ID = 1, G2SID = "LOL" };
            var results = new Collection<ValidationResult>();
            var context = new ValidationContext(obj, null, null);
            bool valid = Validator.TryValidateObject(obj, context, results, true);

            Assert.That(valid, Is.True);
            Assert.That(results, Has.Count.EqualTo(0));
        }
    }
}
