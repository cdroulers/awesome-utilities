using System;
using System.Collections.Generic;
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
    }
}