using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;

namespace System.Web
{
    /// <summary>
    ///     A file, posted as a Data Uri. <see href="http://en.wikipedia.org/wiki/Data_URI_scheme"/>
    /// </summary>
    public class DataUriPostedFile : HttpPostedFileBase, IDisposable
    {
        /// <summary>
        ///     Data Uris begin with this.
        /// </summary>
        public const string Begin = "data:";

        /// <summary>
        ///     This separator precedes that data.
        /// </summary>
        public const string DataSeparator = ",";

        /// <summary>
        ///     If encoded in Base64, this is included in the string.
        /// </summary>
        public const string Base64Marker = ";base64";

        /// <summary>
        ///     Mark between the mime type and the actual charset.
        /// </summary>
        public const string CharsetMarker = ";charset=";

        /// <summary>
        /// Parses the specified data URL into an HttpPostedFileBase
        /// </summary>
        /// <param name="dataUri">The data URI.</param>
        /// <returns>A parsed Data URI posted file.</returns>
        public static DataUriPostedFile Parse(string dataUri)
        {
            if (!dataUri.StartsWith(DataUriPostedFile.Begin))
            {
                throw new ArgumentException(System.Properties.Strings.DataUriPostedFile_NotADataUri, "dataUri");
            }

            int indexOfDataSeparator = dataUri.IndexOf(DataUriPostedFile.DataSeparator, StringComparison.InvariantCultureIgnoreCase);
            string metadata = dataUri.SubstringStartEnd(DataUriPostedFile.Begin.Length, indexOfDataSeparator - 1);
            string data = dataUri.Substring(indexOfDataSeparator + 1);

            bool base64 = metadata.Contains(DataUriPostedFile.Base64Marker);
            metadata = metadata.Replace(DataUriPostedFile.Base64Marker, string.Empty);

            var contentType = new ContentType(string.IsNullOrWhiteSpace(metadata) ? "text/plain" : metadata);

            byte[] bytes = base64 ? Convert.FromBase64String(data) : HttpUtility.UrlDecodeToBytes(data);
            return new DataUriPostedFile(bytes.Length, "datauri" + FileExtensions.GetExtension(contentType.MediaType), contentType.MediaType, new MemoryStream(bytes));
        }

        private DataUriPostedFile(int contentLength, string fileName, string contentType, Stream inputStream)
        {
            this.contentLength = contentLength;
            this.fileName = fileName;
            this.contentType = contentType;
            this.inputStream = inputStream;
        }

        private readonly int contentLength;

        /// <summary>
        /// When overridden in a derived class, gets the size of an uploaded file, in bytes.
        /// </summary>
        /// <returns>The length of the file, in bytes.</returns>
        public override int ContentLength
        {
            get { return this.contentLength; }
        }

        private readonly string fileName;

        /// <summary>
        /// When overridden in a derived class, gets the fully qualified name of the file on the client.
        /// </summary>
        /// <returns>The name of the file on the client, which includes the directory path.</returns>
        public override string FileName
        {
            get { return this.fileName; }
        }

        private readonly string contentType;

        /// <summary>
        /// When overridden in a derived class, gets the MIME content type of an uploaded file.
        /// </summary>
        /// <returns>The MIME content type of the file.</returns>
        public override string ContentType
        {
            get { return this.contentType; }
        }

        private readonly Stream inputStream;

        /// <summary>
        /// When overridden in a derived class, gets a <see cref="T:System.IO.Stream"/> object that points to an uploaded file to prepare for reading the contents of the file.
        /// </summary>
        /// <returns>An object for reading a file.</returns>
        public override Stream InputStream
        {
            get { return this.inputStream; }
        }

        /// <summary>
        /// When overridden in a derived class, saves the contents of an uploaded file.
        /// </summary>
        /// <param name="filename">The name of the file to save.</param>
        public override void SaveAs(string filename)
        {
            this.inputStream.Seek(0, SeekOrigin.Begin);
            using (var file = File.OpenWrite(filename))
            {
                this.inputStream.CopyTo(file);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.inputStream.Dispose();
        }
    }
}
