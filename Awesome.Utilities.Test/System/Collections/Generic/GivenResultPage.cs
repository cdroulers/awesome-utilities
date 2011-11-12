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

        [Test]
        public void When_getting_empty_Then_works()
        {
            var temp = ResultPage<string>.Empty;

            Assert.That(temp, Is.Empty);
            Assert.That(temp.CurrentPage, Is.EqualTo(1));
            Assert.That(temp.PerPage, Is.EqualTo(1));
            Assert.That(temp.TotalNumberOfRecords, Is.EqualTo(0));
            Assert.That(temp.PreviousPage, Is.EqualTo(null));
            Assert.That(temp.NextPage, Is.EqualTo(null));
            Assert.That(temp.LastPage, Is.EqualTo(0));
        }

        private class Class1
        {
            public string Prop1 { get; set; }
        }

        private class Class2 : Class1
        {
            public string Prop2 { get; set; }
        }

        [Test]
        public void When_casting_Then_works()
        {
            var list = new List<Class1>() { new Class2() { Prop1 = "Prop1-1", Prop2 = "Prop2-1" }, new Class2() { Prop1 = "Prop1-2", Prop2 = "Prop2-2" } };

            var page = new ResultPage<Class1>(list, 1, 2, 2);

            var actual = page.Cast<Class2>();

            Assert.That(actual, Is.InstanceOf<ResultPage<Class2>>());
            Assert.That(actual.First().Prop2, Is.EqualTo("Prop2-1"));
            Assert.That(actual.Last().Prop2, Is.EqualTo("Prop2-2"));
        }
    }
}
