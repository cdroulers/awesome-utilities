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
    public class GivenGreaterThanOrEqualToAttribute
    {
        [TestCase("2011-01-01", "2010-01-01", false, 1)]
        [TestCase("2011-01-01", "2011-01-01", true, 0)]
        [TestCase("2011-01-01", "2011-01-02", true, 0)]
        public void When_validating_Then_returns_invalid(string date1, string date2, bool expected, int count)
        {
            var instance = new TestClass()
            {
                One = DateTime.Parse(date1),
                Two = DateTime.Parse(date2),
            };
            var results = new Collection<ValidationResult>();
            var context = new ValidationContext(instance, null, null);
            bool valid = Validator.TryValidateObject(instance, context, results, true);

            Assert.That(valid, Is.EqualTo(expected));
            Assert.That(results, Has.Count.EqualTo(count));
        }


        internal class TestClass
        {
            public DateTime One { get; set; }
            [GreaterThanOrEqualTo("One")]
            public DateTime Two { get; set; }
        }
    }
}
