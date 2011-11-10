using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace System.Data.Common
{
    /// <summary>
    ///     A helper class for connection strings.
    /// </summary>
    public static class ConnectionStringHelper
    {
        /// <summary>
        /// Replaces |DataDirectory| macro to point to a file system in a safe way. Checks for proper path separators.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <returns></returns>
        public static string SafeDataDirectoryReplacement(string dataSource)
        {
            if (!dataSource.Contains("|DataDirectory|"))
            {
                return dataSource;
            }
            var parts = dataSource.Split(new string[] { "|DataDirectory|" }, StringSplitOptions.RemoveEmptyEntries);
            return Path.Combine(((string)AppDomain.CurrentDomain.GetData("DataDirectory")).TrimEnd('\\'), parts.Last().TrimStart('\\'));
        }
    }
}
