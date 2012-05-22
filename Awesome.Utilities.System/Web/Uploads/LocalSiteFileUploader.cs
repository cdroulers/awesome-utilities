using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace System.Web.Uploads
{
    /// <summary>
    ///     A file uploader that saves images locally, according to a hard-coded path or a virtual path.
    /// </summary>
    public class LocalSiteFileUploader : IFileUploader
    {
        private readonly string localPath;
        /// <summary>
        ///     Allows the caller to specify how to create a URI from the resulting path of the saved file.
        /// </summary>
        private readonly Func<string, Uri> resultToUriFunc;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalSiteFileUploader"/> class.
        /// </summary>
        /// <param name="localPath">The local path.</param>
        /// <param name="resultToUriFunc">The if local path is not a virtual path, this method must be passed to transform file system paths to URIs.</param>
        public LocalSiteFileUploader(string localPath, Func<string, Uri> resultToUriFunc = null)
        {
            this.localPath = localPath;
            if (!VirtualPathUtility.IsAppRelative(localPath))
            {
                Validate.Is.Not.Null(resultToUriFunc, "resultToUriFunc");
            }
            this.resultToUriFunc = resultToUriFunc;
        }

        /// <summary>
        /// Uploads the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public Uri Upload(HttpPostedFileBase file, string fileName)
        {
            var fullPath = Path.Combine(this.localPath, fileName);
            var serverPath = VirtualPathUtility.IsAppRelative(fullPath) ? HttpContext.Current.Server.MapPath(fullPath) : fullPath;
            var directory = Path.GetDirectoryName(serverPath);
            new DirectoryInfo(directory).Create();

            file.SaveAs(serverPath);

            return this.GetUri(fullPath);
        }

        private Uri GetUri(string fullPath)
        {
            if (this.resultToUriFunc != null)
            {
                return this.resultToUriFunc(fullPath);
            }
            else
            {
                var builder = new UriBuilder(HttpContext.Current.Request.Url);
                builder.Path = VirtualPathUtility.ToAbsolute(fullPath, HttpContext.Current.Request.ApplicationPath);
                return builder.Uri;
            }
        }

        /// <summary>
        /// Renames a previously uploaded file to the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="newFileName">New name of the file.</param>
        /// <returns></returns>
        public Uri Rename(string fileName, string newFileName)
        {
            var fullPath = Path.Combine(this.localPath, fileName);
            var serverPath = VirtualPathUtility.IsAppRelative(fullPath) ? HttpContext.Current.Server.MapPath(fullPath) : fullPath;
            
            var newFullPath = Path.Combine(this.localPath, newFileName);
            var newServerPath = VirtualPathUtility.IsAppRelative(fullPath) ? HttpContext.Current.Server.MapPath(newFullPath) : newFullPath;

            if (File.Exists(newServerPath))
            {
                File.Delete(newServerPath);
            }
            File.Move(serverPath, newServerPath);

            return this.GetUri(newFullPath);
        }

        /// <summary>
        /// Deletes a previously uploaded file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void Delete(string fileName)
        {
            var fullPath = Path.Combine(this.localPath, fileName);
            var serverPath = VirtualPathUtility.IsAppRelative(fullPath) ? HttpContext.Current.Server.MapPath(fullPath) : fullPath;

            if (File.Exists(serverPath))
            {
                File.Delete(serverPath);
            }
        }
    }
}