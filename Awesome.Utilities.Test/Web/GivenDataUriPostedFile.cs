using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using NUnit.Framework;

namespace Awesome.Utilities.Test.Web
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenDataUriPostedFile
    {
        [Test]
        public void When_parsing_Then_finds_right_info()
        {
            var bytes = File.ReadAllBytes(@"Web\onebyone.bmp");
            string temp = Convert.ToBase64String(bytes);

            var result = DataUriPostedFile.Parse(@"data:image/bmp;base64," + temp);

            Assert.That(result.ContentType, Is.EqualTo("image/bmp"));
            Assert.That(result.ContentLength, Is.EqualTo(58));
            Assert.That(result.FileName, Is.EqualTo("datauri.bmp"));
            Assert.That(result.InputStream.Length, Is.EqualTo(58));

            var newBytes = new byte[58];
            result.InputStream.Read(newBytes, 0, 58);

            bool equal = newBytes.SequenceEqual(bytes);
            Assert.That(equal, Is.True, "Read file is not the same as disk file!");
            
            result.SaveAs("result.bmp"); // Ensure it doesn't explode.
        }
    }
}
