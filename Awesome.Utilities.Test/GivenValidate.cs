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
            Assert.Throws<ArgumentNullException>(() => Validate.Is.Not.Null(temp, "temp"));

            temp = new Uri("http://example.org/");
            Validate.Is.Not.Null(temp, "temp");
        }

        [Test]
        public void When_validating_null_Then_works()
        {
            Uri temp = new Uri("http://example.org/");
            Assert.Throws<ArgumentNotNullException>(() => Validate.Is.Null(temp, "temp"));

            temp = null;
            Validate.Is.Null(temp, "temp");
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

        [TestCase(4, 5, false)]
        [TestCase(4, 4, false)]
        [TestCase(4, 3, true)]
        public void When_validating_not_higher_than_Then_works(int toValidate, int compareTo, bool shouldThrow)
        {
            if (shouldThrow)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => Validate.Is.Not.HigherThan(toValidate, compareTo, "toValidate"));
            }
            else
            {
                Validate.Is.Not.HigherThan(toValidate, compareTo, "toValidate");
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
        public void When_validating_not_higher_than_or_equal_to_Then_works(int toValidate, int compareTo, bool shouldThrow)
        {
            if (shouldThrow)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => Validate.Is.Not.HigherThanOrEqualTo(toValidate, compareTo, "toValidate"));
            }
            else
            {
                Validate.Is.Not.HigherThanOrEqualTo(toValidate, compareTo, "toValidate");
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

        [TestCase(4, 5, true)]
        [TestCase(4, 4, false)]
        [TestCase(4, 3, false)]
        public void When_validating_not_lower_than_Then_works(int toValidate, int compareTo, bool shouldThrow)
        {
            if (shouldThrow)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => Validate.Is.Not.LowerThan(toValidate, compareTo, "toValidate"));
            }
            else
            {
                Validate.Is.Not.LowerThan(toValidate, compareTo, "toValidate");
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

        [TestCase(4, 5, true)]
        [TestCase(4, 4, true)]
        [TestCase(4, 3, false)]
        public void When_validating_not_lower_than_or_equal_to_Then_works(int toValidate, int compareTo, bool shouldThrow)
        {
            if (shouldThrow)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => Validate.Is.Not.LowerThanOrEqualTo(toValidate, compareTo, "toValidate"));
            }
            else
            {
                Validate.Is.Not.LowerThanOrEqualTo(toValidate, compareTo, "toValidate");
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

        [TestCase(4, 3, 5, true, true)]
        [TestCase(3, 3, 5, true, true)]
        [TestCase(5, 3, 5, true, true)]
        [TestCase(4, 3, 5, false, true)]
        [TestCase(3, 3, 5, false, false)]
        [TestCase(5, 3, 5, false, false)]
        public void When_validating_not_between_Then_works(int toValidate, int lowerLimit, int higherLimit, bool inclusive, bool shouldThrow)
        {
            if (shouldThrow)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => Validate.Is.Not.Between(toValidate, lowerLimit, higherLimit, "toValidate", inclusive));
            }
            else
            {
                Validate.Is.Not.Between(toValidate, lowerLimit, higherLimit, "toValidate", inclusive);
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
                Assert.Throws<StringArgumentNullOrEmptyException>(() => Validate.Is.Not.NullOrEmpty(toValidate, "toValidate"));
            }
            else
            {
                Validate.Is.Not.NullOrEmpty(toValidate, "toValidate");
            }
        }

        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase(" ", true)]
        [TestCase("wat", true)]
        public void When_validating_null_or_empty_Then_works(string toValidate, bool shouldThrow)
        {
            if (shouldThrow)
            {
                Assert.Throws<StringArgumentNotNullOrEmptyException>(() => Validate.Is.NullOrEmpty(toValidate, "toValidate"));
            }
            else
            {
                Validate.Is.NullOrEmpty(toValidate, "toValidate");
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
                Assert.Throws<StringArgumentNullOrWhiteSpaceException>(() => Validate.Is.Not.NullOrWhiteSpace(toValidate, "toValidate"));
            }
            else
            {
                Validate.Is.Not.NullOrWhiteSpace(toValidate, "toValidate");
            }
        }

        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase("wat", true)]
        public void When_validating_null_or_white_space_Then_works(string toValidate, bool shouldThrow)
        {
            if (shouldThrow)
            {
                Assert.Throws<StringArgumentNotNullOrWhiteSpaceException>(() => Validate.Is.NullOrWhiteSpace(toValidate, "toValidate"));
            }
            else
            {
                Validate.Is.NullOrWhiteSpace(toValidate, "toValidate");
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

        [TestCase("US", 2, true)]
        [TestCase("USA", 2, false)]
        public void When_validating_not_equal_to_Then_works(string toValidate, int toCompare, bool shouldThrow)
        {
            if (shouldThrow)
            {
                Assert.Throws<ArgumentException>(() => Validate.Is.Not.EqualTo(toValidate.Length, toCompare, "toValidate.Length"));
            }
            else
            {
                Validate.Is.Not.EqualTo(toValidate.Length, toCompare, "toValidate.Length");
            }
        }

        [TestCase("US,CA,FR", "CA", false)]
        [TestCase("US,CA,FR", "DE", true)]
        public void When_validating_contained_in_Then_works(string toSplit, string toCompare, bool shouldThrow)
        {
            if (shouldThrow)
            {
                Assert.Throws<ArgumentException>(() => Validate.Is.ContainedIn(toCompare, toSplit.Split(','), "toValidate"));
            }
            else
            {
                Validate.Is.ContainedIn(toCompare, toSplit.Split(','), "toValidate");
            }
        }

        [TestCase("US,CA,FR", "CA", true)]
        [TestCase("US,CA,FR", "DE", false)]
        public void When_validating_not_contained_in_Then_works(string toSplit, string toCompare, bool shouldThrow)
        {
            if (shouldThrow)
            {
                Assert.Throws<ArgumentException>(() => Validate.Is.Not.ContainedIn(toCompare, toSplit.Split(','), "toValidate"));
            }
            else
            {
                Validate.Is.Not.ContainedIn(toCompare, toSplit.Split(','), "toValidate");
            }
        }
    }
}
