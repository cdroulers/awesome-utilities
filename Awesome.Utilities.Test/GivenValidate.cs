using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Awesome.Utilities.Test
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenValidate
    {
        [Test]
        public void When_validating_not_null_Then_works()
        {
            Uri temp = null;
            Assert.Throws<ArgumentNullException>(() => Validate.Is.NotNull(temp, "temp"));

            temp = new Uri("http://example.org/");
            Validate.Is.NotNull(temp, "temp");
        }

        [TestCase(4, 5, true)]
        [TestCase(4, 4, true)]
        [TestCase(4, 3, false)]
        public void When_validating_higher_than_Then_works(int toValidate, int compareTo, bool shouldThrow)
        {
            if (shouldThrow)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => Validate.Is.HigherThan(toValidate, compareTo, "toValidate"));
            }
            else
            {
                Validate.Is.HigherThan(toValidate, compareTo, "toValidate");
            }
        }

        [TestCase(4, 5, true)]
        [TestCase(4, 4, false)]
        [TestCase(4, 3, false)]
        public void When_validating_higher_than_or_equal_to_Then_works(int toValidate, int compareTo, bool shouldThrow)
        {
            if (shouldThrow)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => Validate.Is.HigherThanOrEqualTo(toValidate, compareTo, "toValidate"));
            }
            else
            {
                Validate.Is.HigherThanOrEqualTo(toValidate, compareTo, "toValidate");
            }
        }

        [TestCase(4, 5, false)]
        [TestCase(4, 4, true)]
        [TestCase(4, 3, true)]
        public void When_validating_lower_than_Then_works(int toValidate, int compareTo, bool shouldThrow)
        {
            if (shouldThrow)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => Validate.Is.LowerThan(toValidate, compareTo, "toValidate"));
            }
            else
            {
                Validate.Is.LowerThan(toValidate, compareTo, "toValidate");
            }
        }

        [TestCase(4, 5, false)]
        [TestCase(4, 4, false)]
        [TestCase(4, 3, true)]
        public void When_validating_lower_than_or_equal_to_Then_works(int toValidate, int compareTo, bool shouldThrow)
        {
            if (shouldThrow)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => Validate.Is.LowerThanOrEqualTo(toValidate, compareTo, "toValidate"));
            }
            else
            {
                Validate.Is.LowerThanOrEqualTo(toValidate, compareTo, "toValidate");
            }
        }

        [TestCase(4, 3, 5, true, false)]
        [TestCase(3, 3, 5, true, false)]
        [TestCase(5, 3, 5, true, false)]
        [TestCase(4, 3, 5, false, false)]
        [TestCase(3, 3, 5, false, true)]
        [TestCase(5, 3, 5, false, true)]
        public void When_validating_between_Then_works(int toValidate, int lowerLimit, int higherLimit, bool inclusive, bool shouldThrow)
        {
            if (shouldThrow)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => Validate.Is.Between(toValidate, lowerLimit, higherLimit, "toValidate", inclusive));
            }
            else
            {
                Validate.Is.Between(toValidate, lowerLimit, higherLimit, "toValidate", inclusive);
            }
        }

        [TestCase(null, true)]
        [TestCase("", true)]
        [TestCase(" ", false)]
        [TestCase("wat", false)]
        public void When_validating_not_null_or_empty_Then_works(string toValidate, bool shouldThrow)
        {
            if (shouldThrow)
            {
                Assert.Throws<StringArgumentNullOrEmptyException>(() => Validate.Is.NotNullOrEmpty(toValidate, "toValidate"));
            }
            else
            {
                Validate.Is.NotNullOrEmpty(toValidate, "toValidate");
            }
        }

        [TestCase(null, true)]
        [TestCase("", true)]
        [TestCase(" ", true)]
        [TestCase("wat", false)]
        public void When_validating_not_null_or_white_space_Then_works(string toValidate, bool shouldThrow)
        {
            if (shouldThrow)
            {
                Assert.Throws<StringArgumentNullOrWhiteSpaceException>(() => Validate.Is.NotNullOrWhiteSpace(toValidate, "toValidate"));
            }
            else
            {
                Validate.Is.NotNullOrWhiteSpace(toValidate, "toValidate");
            }
        }

        [TestCase("US", 2, false)]
        [TestCase("USA", 2, true)]
        public void When_validating_equal_to_Then_works(string toValidate, int toCompare, bool shouldThrow)
        {
            if (shouldThrow)
            {
                Assert.Throws<ArgumentException>(() => Validate.Is.EqualTo(toValidate.Length, toCompare, "toValidate.Length"));
            }
            else
            {
                Validate.Is.EqualTo(toValidate.Length, toCompare, "toValidate.Length");
            }
        }
    }
}
