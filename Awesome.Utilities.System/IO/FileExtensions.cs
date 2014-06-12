using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace System.IO
{
    /// <summary>
    ///     File Extensions methods.
    /// </summary>
    public static class FileExtensions
    {
        /// <summary>
        /// Gets the MIME type associated with the specified extension.
        /// </summary>
        /// <param name="extension">The extension.</param>
        /// <returns>The MIME type for the following file extension.</returns>
        public static string GetMimeType(string extension)
        {
            string mimeType = "application/unknown";

            RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(extension.ToLowerInvariant());

            if (regKey != null)
            {
                string contentType = regKey.GetValue("Content Type") as string;

                if (contentType != null)
                {
                    mimeType = contentType;
                }
            }

            return mimeType;
        }

        /// <summary>
        /// Gets the default extension for the specified MIME type.
        /// </summary>
        /// <param name="mimeType">Type of the MIME.</param>
        /// <returns>The file extension for the following MIME type.</returns>
        public static string GetExtension(string mimeType)
        {
            string extension = string.Empty;

            var key = Registry.ClassesRoot.OpenSubKey(@"MIME\Database\Content Type\" + mimeType, false);
            if (key != null)
            {
                var value = key.GetValue("Extension", null) as string;
                if (value != null)
                {
                    extension = value;
                }
            }

            return extension;
        }
    }
}
