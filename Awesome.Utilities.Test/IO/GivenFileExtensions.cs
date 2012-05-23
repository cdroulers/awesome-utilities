using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Web;
using System.IO;

namespace Awesome.Utilities.Test.IO
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenFileExtensions
    {
        [TestCase(".jpg", "image/jpeg")]
        [TestCase(".png", "image/png")]
        [TestCase(".gif", "image/gif")]
        [TestCase(".exe", "application/x-msdownload")]
        public void When_getting_mime_type_Then_works(string extension, string expected)
        {
            var actual = FileExtensions.GetMimeType(extension);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase("image/jpeg", ".jpg")]
        [TestCase("image/png", ".png")]
        [TestCase("image/gif", ".gif")]
        [TestCase("application/hta", ".hta")]
        public void When_getting_extension_Then_works(string mimeType, string expected)
        {
            var actual = FileExtensions.GetExtension(mimeType);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
