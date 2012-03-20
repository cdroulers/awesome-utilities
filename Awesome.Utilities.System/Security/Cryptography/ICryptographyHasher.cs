using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Security.Cryptography
{
    /// <summary>
    ///     Hashing cryptography
    /// </summary>
    public interface ICryptographyHasher
    {
        /// <summary>
        /// Hashes the string with the specified algorithm and returns a string
        /// </summary>
        /// <param name="algorithm">The algorithm.</param>
        /// <param name="toHash">To hash.</param>
        /// <returns></returns>
        string AsString(SupportedHashAlgorithm algorithm, string toHash);
        /// <summary>
        /// Hashes the string with the specified algorithm and returns an array of bytes.
        /// </summary>
        /// <param name="algorithm">The algorithm.</param>
        /// <param name="toHash">To hash.</param>
        /// <returns></returns>
        byte[] AsBytes(SupportedHashAlgorithm algorithm, string toHash);
    }
}
