using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Collections.ObjectModel;

namespace Awesome.Utilities.Test.ComponentModel.DataAnnotations
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenRequiredBooleanAttribute
    {
        [Test]
        public void When_validating_Then_returns_valid()
        {
            var instance = new TestClass()
            {
                One = false,
                Two = true,
            };
            var results = new Collection<ValidationResult>();
            var context = new ValidationContext(instance, null, null);
            bool valid = Validator.TryValidateObject(instance, context, results, true);

            Assert.That(valid, Is.EqualTo(true));
            Assert.That(results, Has.Count.EqualTo(0));
        }

        [Test]
        public void When_validating_Then_returns_invalid()
        {
            var instance = new TestClass()
            {
                One = true,
                Two = false,
            };
            var results = new Collection<ValidationResult>();
            var context = new ValidationContext(instance, null, null);
            bool valid = Validator.TryValidateObject(instance, context, results, true);

            Assert.That(valid, Is.EqualTo(false));
            Assert.That(results, Has.Count.EqualTo(2));
        }


        internal class TestClass
        {
            [RequiredBoolean(false)]
            public bool One { get; set; }
            [RequiredBoolean(true)]
            public bool Two { get; set; }
        }
    }
}
