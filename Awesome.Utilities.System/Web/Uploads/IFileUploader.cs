using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace System.Web.Uploads
{
    /// <summary>
    ///     An interface to upload files from the Web
    /// </summary>
    public interface IFileUploader
    {
        /// <summary>
        /// Uploads the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        Uri Upload(HttpPostedFileBase file, string fileName);

        /// <summary>
        /// Uploads the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        Uri Upload(Stream stream, string fileName);

        /// <summary>
        ///     Renames a previously uploaded file to the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="newFileName">New name of the file.</param>
        /// <returns></returns>
        Uri Rename(string fileName, string newFileName);

        /// <summary>
        ///     Deletes a previously uploaded file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        void Delete(string fileName);
    }
}