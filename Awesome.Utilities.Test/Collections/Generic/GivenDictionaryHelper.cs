using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Awesome.Utilities.Test.Collections.Generic
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenDictionaryHelper
    {
        [Test]
        public void When_getting_dictionary_from_anonymous_Then_works()
        {
            using (Clock.Pause())
            {
                var obj = new
                {
                    Name = "lol",
                    Age = 2,
                    CreatedOn = Clock.UtcNow
                };

                var dict = DictionaryHelper.BuildDictionaryFromAnonymousObject(obj);

                Assert.That(dict.Keys, Has.Count.EqualTo(3));
                Assert.That(dict["Name"], Is.EqualTo("lol"));
                Assert.That(dict["Age"], Is.EqualTo(2));
                Assert.That(dict["CreatedOn"], Is.EqualTo(Clock.UtcNow));
            }
        }
    }
}
