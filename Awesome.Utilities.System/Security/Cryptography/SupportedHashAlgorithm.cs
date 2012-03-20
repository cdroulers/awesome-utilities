using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Security.Cryptography
{
    /// <summary>
    ///     Supported Hash Algorithm for CryptographyHelper
    /// </summary>
    public enum SupportedHashAlgorithm
    {
        /// <summary>
        ///     The MD5 hash algorithm
        /// </summary>
        Md5,
        /// <summary>
        ///     The SHA1 hash algorithm
        /// </summary>
        Sha1,
        /// <summary>
        ///     The SHA256 hash algorithm
        /// </summary>
        Sha256,
        /// <summary>
        ///     The SHA512 hash algorithm
        /// </summary>
        Sha512,
    }
}
