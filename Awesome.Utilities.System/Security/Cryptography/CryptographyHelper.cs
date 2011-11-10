using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Security.Cryptography
{
    /// <summary>
    ///     A helper for cryptography stuff
    /// </summary>
    public static class CryptographyHelper
    {
        /// <summary>
        ///     Hashes a string according to the specified algorithm
        /// </summary>
        /// <param name="algorithm">The algorithm to hash the string with</param>
        /// <param name="toHash">The string to hash</param>
        /// <returns>The hashed string</returns>
        public static string Hash(SupportedHashAlgorithm algorithm, string toHash)
        {
            switch (algorithm)
            {
                case SupportedHashAlgorithm.Md5:
                    return CryptographyHelper.Hash(new MD5CryptoServiceProvider(), toHash);
                case SupportedHashAlgorithm.Sha1:
                    return CryptographyHelper.Hash(new SHA1CryptoServiceProvider(), toHash);
                case SupportedHashAlgorithm.Sha256:
                    return CryptographyHelper.Hash(new SHA256CryptoServiceProvider(), toHash);
                case SupportedHashAlgorithm.Sha512:
                    return CryptographyHelper.Hash(new SHA512CryptoServiceProvider(), toHash);
                default:
                    throw GetAlgorithmNotSupported(algorithm);
            }
        }

        /// <summary>
        ///     Hashes a string according to the specified algorithm
        /// </summary>
        /// <param name="algorithm">The algorithm to hash the string with</param>
        /// <param name="toHash">The string to hash</param>
        /// <returns>The hashed string</returns>
        public static byte[] HashBytes(SupportedHashAlgorithm algorithm, string toHash)
        {
            switch (algorithm)
            {
                case SupportedHashAlgorithm.Md5:
                    return CryptographyHelper.HashBytes(new MD5CryptoServiceProvider(), toHash);
                case SupportedHashAlgorithm.Sha1:
                    return CryptographyHelper.HashBytes(new SHA1CryptoServiceProvider(), toHash);
                case SupportedHashAlgorithm.Sha256:
                    return CryptographyHelper.HashBytes(new SHA256CryptoServiceProvider(), toHash);
                case SupportedHashAlgorithm.Sha512:
                    return CryptographyHelper.HashBytes(new SHA512CryptoServiceProvider(), toHash);
                default:
                    throw GetAlgorithmNotSupported(algorithm);
            }
        }

        /// <summary>
        ///     Hashes a string according to an HashAlgorithm
        /// </summary>
        /// <param name="algorithm">The algorithm to use</param>
        /// <param name="toHash">The string to hash</param>
        /// <returns>The hashed string</returns>
        private static string Hash(HashAlgorithm algorithm, string toHash)
        {
            byte[] bytesToHash = Encoding.UTF8.GetBytes(toHash);

            byte[] bytesHashed = algorithm.ComputeHash(bytesToHash);
            StringBuilder sb = new StringBuilder();
            foreach (byte byteHashed in bytesHashed)
            {
                sb.Append(byteHashed.ToString("x2").ToLower());
            }
            return sb.ToString();
        }

        /// <summary>
        ///     Hashes a string according to an HashAlgorithm and returns the bytes.
        /// </summary>
        /// <param name="algorithm">The algorithm to use</param>
        /// <param name="toHash">The string to hash</param>
        /// <returns>The hashed bytes</returns>
        private static byte[] HashBytes(HashAlgorithm algorithm, string toHash)
        {
            byte[] bytesToHash = Encoding.UTF8.GetBytes(toHash);

            byte[] bytesHashed = algorithm.ComputeHash(bytesToHash);
            return bytesHashed;
        }

        private static CryptographicException GetAlgorithmNotSupported(object algorithm)
        {
            return new CryptographicException(string.Format(Properties.Strings.Cryptography_AlgorithmNotSupported, algorithm));
        }
    }
}
