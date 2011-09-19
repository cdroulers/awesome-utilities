using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Awesome.Utilities.Test.System
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenAdvancedMath
    {
        [TestCase(7850, 625, 25)]
        [TestCase(56, 42, 14)]
        [TestCase(12, 18, 6)]
        [TestCase(48, 180, 12)]
        [TestCase(2, 4, 2)]
        [TestCase(-2, 4, 2)]
        public void When_calculating_greatest_common_divisor_Then_it_returns_the_right_result(int one, int two, int expected)
        {
            int actual = AdvancedMath.GreatestCommonDivisor(one, two);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(21, 6, 42)]
        [TestCase(4, 6, 12)]
        [TestCase(8, 14, 56)]
        [TestCase(2, 8, 8)]
        [TestCase(48, 180, 720)]
        [TestCase(180, 48, 720)]
        [TestCase(-180, 48, 720)]
        public void When_calculating_least_common_multiple_Then_it_returns_the_right_result(int one, int two, int expected)
        {
            int actual = AdvancedMath.LeastCommonMultiple(one, two);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
