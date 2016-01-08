using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Awesome.Utilities.Test.Data.Common
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenConnectionStringHelper
    {
        [TestCase(@"C:\database", @"|DataDirectory|Test.s3db", @"C:\database\Test.s3db")]
        [TestCase(@"C:\database\", @"|DataDirectory|Test.s3db", @"C:\database\Test.s3db")]
        [TestCase(@"C:\database", @"|DataDirectory|\Test.s3db", @"C:\database\Test.s3db")]
        [TestCase(@"C:\database\", @"|DataDirectory|\Test.s3db", @"C:\database\Test.s3db")]
        [TestCase(@"C:\database", @"Test.s3db", @"Test.s3db")]
        public void When_replacing_data_directory_then_works(string dataDirectory, string dataSource, string expected)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", dataDirectory);

            string actual = ConnectionStringHelper.SafeDataDirectoryReplacement(dataSource);

            Assert.That(actual.Replace('\\', Path.DirectorySeparatorChar), Is.EqualTo(expected.Replace('\\', Path.DirectorySeparatorChar)));
        }
    }
}
