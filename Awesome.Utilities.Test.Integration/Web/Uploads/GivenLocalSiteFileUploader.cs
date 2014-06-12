using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Uploads;

using NUnit.Framework;

namespace Awesome.Utilities.Test.Integration.Web.Uploads
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenLocalSiteFileUploader
    {
        private const string TestFileName = "wot.jpg";

        private IFileUploader uploader;

        [SetUp]
        public void SetUp()
        {
            this.uploader = new LocalSiteFileUploader(".", s => new Uri("http://example.org/" + s));

            if (File.Exists(GivenLocalSiteFileUploader.TestFileName))
            {
                File.Delete(GivenLocalSiteFileUploader.TestFileName);
            }
        }

        [Test]
        public void When_constructing_Then_validates_function()
        {
            Assert.DoesNotThrow(() => new LocalSiteFileUploader("~/content/uploads")); // Works because it's a virtual path.

            Assert.Throws<ArgumentNullException>(() => new LocalSiteFileUploader(".")); // Shouldn't work because local paths need a function to transform to URI.
        }

        [Test]
        public void When_uploading_Then_saves_file_in_right_place()
        {
            this.uploader.Upload(new HttpPostedFileStub(), GivenLocalSiteFileUploader.TestFileName);

            Assert.That(File.Exists(GivenLocalSiteFileUploader.TestFileName), Is.True, "file was not saved!");
            var bytes = File.ReadAllBytes(GivenLocalSiteFileUploader.TestFileName);
            Assert.That(bytes, Has.Length.EqualTo(10));
        }

        [Test]
        public void When_renaming_Then_works()
        {
            this.uploader.Upload(new HttpPostedFileStub(), GivenLocalSiteFileUploader.TestFileName);
            Assert.That(File.Exists(GivenLocalSiteFileUploader.TestFileName), Is.True, "file was not saved!");

            this.uploader.Rename(GivenLocalSiteFileUploader.TestFileName, "wat.jpg");

            Assert.That(File.Exists("wat.jpg"), Is.True, "file was not renamed!");
            var bytes = File.ReadAllBytes("wat.jpg");
            Assert.That(bytes, Has.Length.EqualTo(10));
        }

        [Test]
        public void When_deleting_Then_works()
        {
            this.uploader.Upload(new HttpPostedFileStub(), GivenLocalSiteFileUploader.TestFileName);
            Assert.That(File.Exists(GivenLocalSiteFileUploader.TestFileName), Is.True, "file was not saved!");

            this.uploader.Delete(GivenLocalSiteFileUploader.TestFileName);

            Assert.That(File.Exists(GivenLocalSiteFileUploader.TestFileName), Is.False, "file was not deleted!");
        }

        private class HttpPostedFileStub : HttpPostedFileBase
        {
            public override int ContentLength
            {
                get { return 10; }
            }

            public override string FileName
            {
                get { return "lol.jpg"; }
            }

            public override string ContentType
            {
                get { return "image/jpg"; }
            }

            public override Stream InputStream
            {
                get { return new MemoryStream(new byte[10]); }
            }

            public override void SaveAs(string filename)
            {
                File.WriteAllBytes(filename, new byte[10]);
            }
        }
    }
}
