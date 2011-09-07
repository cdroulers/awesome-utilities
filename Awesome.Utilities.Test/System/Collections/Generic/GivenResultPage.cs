using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Awesome.Utilities.Test.System.Collections.Generic
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenResultPage
    {
        [TestCase(1, 3, 9, null, 2, 3)]
        [TestCase(2, 3, 9, 1, 3, 3)]
        [TestCase(3, 3, 9, 2, null, 3)]
        [TestCase(2, 3, 12, 1, 3, 4)]
        public void When_constructing_Then_assigns_previous_and_next_properly(int currentPage, int perPage, int total, int? previous, int? next, int last)
        {
            var temp = new List<string>() { "one", "two", "three" };
            var results = new ResultPage<string>(temp, currentPage, perPage, total);

            Assert.That(results.Count, Is.EqualTo(temp.Count));
            Assert.That(results.CurrentPage, Is.EqualTo(currentPage));
            Assert.That(results.PerPage, Is.EqualTo(perPage));
            Assert.That(results.TotalNumberOfRecords, Is.EqualTo(total));
            Assert.That(results.PreviousPage, Is.EqualTo(previous));
            Assert.That(results.NextPage, Is.EqualTo(next));
            Assert.That(results.LastPage, Is.EqualTo(last));
        }
    }
}
