using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using NUnit.Framework;

namespace Awesome.Utilities.Test.Drawing
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenImage
    {
        [Test]
        public void When_resizing_image_Then_works()
        {
            using (var image = Image.FromFile(@"..\..\Web\onebyone.bmp"))
            {
                using (var resized = image.Resize(new Size(5, 5)))
                {
                    resized.SaveAsJpeg("fivebyfive.jpg");
                }
            }

            using (var result = Image.FromFile(@"fivebyfive.jpg"))
            {
                Assert.That(result.RawFormat, Is.EqualTo(ImageFormat.Jpeg));
                Assert.That(result.Width, Is.EqualTo(5));
                Assert.That(result.Height, Is.EqualTo(5));
            }
        }
    }
}
