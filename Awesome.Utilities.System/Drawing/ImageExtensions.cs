using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace System.Drawing
{
    /// <summary>
    ///     Extensions for Image files.
    /// </summary>
    public static class ImageExtensions
    {
        /// <summary>
        /// Resizes the image.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="newSize">The new size.</param>
        /// <param name="compositingQuality">The compositing quality.</param>
        /// <param name="interpolationMode">The interpolation mode.</param>
        /// <param name="smoothingMode">The smoothing mode.</param>
        /// <returns>A resized bitmap.</returns>
        public static Bitmap Resize(
            this Image self,
            Size newSize,
            CompositingQuality compositingQuality = CompositingQuality.Default,
            InterpolationMode interpolationMode = InterpolationMode.Default,
            SmoothingMode smoothingMode = SmoothingMode.Default)
        {
            var result = new Bitmap(newSize.Width, newSize.Height);

            using (Graphics graphics = Graphics.FromImage(result))
            {
                graphics.CompositingQuality = compositingQuality;
                graphics.InterpolationMode = interpolationMode;
                graphics.SmoothingMode = smoothingMode;

                graphics.DrawImage(self, new Rectangle(0, 0, result.Width, result.Height));
            }

            return result;
        }

        /// <summary>
        ///     Saves as JPEG.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="path">The path.</param>
        /// <param name="quality">The quality.</param>
        public static void SaveAsJpeg(this Image self, string path, int quality = 75)
        {
            Validate.Is.Between(quality, 0, 100, "quality");

            var qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

            ImageCodecInfo jpegCodec = ImageCodecInfo.GetImageEncoders().FirstOrDefault(e => e.MimeType == "image/jpeg");

            Validate.Is.Not.Null(jpegCodec, "jpegCodec");

            var encoderParams = new EncoderParameters(1);
            
            encoderParams.Param[0] = qualityParam;
            
            self.Save(path, jpegCodec, encoderParams);
        }
    }
}
