using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Security.Cryptography
{
    /// <summary>
    ///     A helper for cryptography stuff
    /// </summary>
    public class CryptographyHelper : ICryptographyHasher, ICryptographyCipher
    {
        private static readonly CryptographyHelper Instance = new CryptographyHelper();

        private CryptographyHelper()
        {
        }

        /// <summary>
        /// Gets the the hasher instance.
        /// </summary>
        public static ICryptographyHasher Hash
        {
            get { return CryptographyHelper.Instance; }
        }

        /// <summary>
        /// Gets the the cipher instance.
        /// </summary>
        public static ICryptographyCipher Cipher
        {
            get { return CryptographyHelper.Instance; }
        }

        private static readonly IDictionary<SupportedHashAlgorithm, Func<HashAlgorithm>> HashAlgorithms = new Dictionary<SupportedHashAlgorithm, Func<HashAlgorithm>>()
        {
            { SupportedHashAlgorithm.Md5, () => new MD5CryptoServiceProvider() },
            { SupportedHashAlgorithm.Sha1, () => new SHA1CryptoServiceProvider() },
            { SupportedHashAlgorithm.Sha256, () => new SHA256CryptoServiceProvider() },
            { SupportedHashAlgorithm.Sha512, () => new SHA512CryptoServiceProvider() },
        };

        private static readonly IDictionary<SupportedCipherAlgorithm, Func<SymmetricAlgorithm>> CipherAlgorithms = new Dictionary<SupportedCipherAlgorithm, Func<SymmetricAlgorithm>>()
        {
            { SupportedCipherAlgorithm.Rijndael, () => new RijndaelManaged() { Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 } },
            { SupportedCipherAlgorithm.TripleDes, () => new TripleDESCryptoServiceProvider() { Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 } },
        };

        /// <summary>
        /// Hashes a string according to the specified algorithm
        /// </summary>
        /// <param name="algorithm">The algorithm to hash the string with</param>
        /// <param name="toHash">The string to hash</param>
        /// <returns>
        /// The hashed string
        /// </returns>
        string ICryptographyHasher.AsString(SupportedHashAlgorithm algorithm, string toHash)
        {
            if (HashAlgorithms.ContainsKey(algorithm))
            {
                return CryptographyHelper.HashString(HashAlgorithms[algorithm](), toHash);
            }

            throw GetAlgorithmNotSupported(algorithm);
        }

        /// <summary>
        /// Hashes a string according to the specified algorithm
        /// </summary>
        /// <param name="algorithm">The algorithm to hash the string with</param>
        /// <param name="toHash">The string to hash</param>
        /// <returns>
        /// The hashed string
        /// </returns>
        byte[] ICryptographyHasher.AsBytes(SupportedHashAlgorithm algorithm, string toHash)
        {
            if (HashAlgorithms.ContainsKey(algorithm))
            {
                return CryptographyHelper.HashBytes(HashAlgorithms[algorithm](), toHash);
            }

            throw GetAlgorithmNotSupported(algorithm);
        }

        string ICryptographyCipher.EncipherString(SupportedCipherAlgorithm algorithm, string toEncipher, string key)
        {
            if (CipherAlgorithms.ContainsKey(algorithm))
            {
                return CryptographyHelper.CipherString(CipherAlgorithms[algorithm](), toEncipher, key);
            }

            throw GetAlgorithmNotSupported(algorithm);
        }

        byte[] ICryptographyCipher.EncipherBytes(SupportedCipherAlgorithm algorithm, string toEncipher, string key)
        {
            if (CipherAlgorithms.ContainsKey(algorithm))
            {
                return CryptographyHelper.CipherBytes(CipherAlgorithms[algorithm](), toEncipher, key);
            }

            throw GetAlgorithmNotSupported(algorithm);
        }

        string ICryptographyCipher.EncipherString(SupportedCipherAlgorithm algorithm, byte[] toEncipher, byte[] key)
        {
            if (CipherAlgorithms.ContainsKey(algorithm))
            {
                return CryptographyHelper.CipherString(CipherAlgorithms[algorithm](), toEncipher, key);
            }

            throw GetAlgorithmNotSupported(algorithm);
        }

        byte[] ICryptographyCipher.EncipherBytes(SupportedCipherAlgorithm algorithm, byte[] toEncipher, byte[] key)
        {
            if (CipherAlgorithms.ContainsKey(algorithm))
            {
                return CryptographyHelper.CipherBytes(CipherAlgorithms[algorithm](), toEncipher, key);
            }

            throw GetAlgorithmNotSupported(algorithm);
        }

        string ICryptographyCipher.DecipherString(SupportedCipherAlgorithm algorithm, string toDecipher, string key)
        {
            if (CipherAlgorithms.ContainsKey(algorithm))
            {
                return CryptographyHelper.DecipherString(CipherAlgorithms[algorithm](), toDecipher, key);
            }

            throw GetAlgorithmNotSupported(algorithm);
        }

        byte[] ICryptographyCipher.DecipherBytes(SupportedCipherAlgorithm algorithm, string toDecipher, string key)
        {
            if (CipherAlgorithms.ContainsKey(algorithm))
            {
                return CryptographyHelper.DecipherBytes(CipherAlgorithms[algorithm](), toDecipher, key);
            }

            throw GetAlgorithmNotSupported(algorithm);
        }

        string ICryptographyCipher.DecipherString(SupportedCipherAlgorithm algorithm, byte[] toDecipher, byte[] key)
        {
            if (CipherAlgorithms.ContainsKey(algorithm))
            {
                return CryptographyHelper.DecipherString(CipherAlgorithms[algorithm](), toDecipher, key);
            }

            throw GetAlgorithmNotSupported(algorithm);
        }

        byte[] ICryptographyCipher.DecipherBytes(SupportedCipherAlgorithm algorithm, byte[] toDecipher, byte[] key)
        {
            if (CipherAlgorithms.ContainsKey(algorithm))
            {
                return CryptographyHelper.DecipherBytes(CipherAlgorithms[algorithm](), toDecipher, key);
            }

            throw GetAlgorithmNotSupported(algorithm);
        }

        string ICryptographyCipher.ToBase64(string toEncipher)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(toEncipher));
        }

        string ICryptographyCipher.FromBase64(string toDecipher)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(toDecipher));
        }

        /// <summary>
        ///     Hashes a string according to an HashAlgorithm
        /// </summary>
        /// <param name="algorithm">The algorithm to use</param>
        /// <param name="toHash">The string to hash</param>
        /// <returns>The hashed string</returns>
        private static string HashString(HashAlgorithm algorithm, string toHash)
        {
            byte[] bytesHashed = HashBytes(algorithm, toHash);
            var sb = new StringBuilder();
            foreach (byte byteHashed in bytesHashed)
            {
                sb.Append(byteHashed.ToString("x2").ToLower());
            }

            return sb.ToString();
        }

        private static byte[] HashBytes(HashAlgorithm algorithm, string toHash)
        {
            byte[] bytesToHash = Encoding.UTF8.GetBytes(toHash);

            byte[] bytesHashed = algorithm.ComputeHash(bytesToHash);
            algorithm.Clear();
            algorithm.Dispose();
            return bytesHashed;
        }

        private static string CipherString(SymmetricAlgorithm algorithm, string toHash, string key)
        {
            byte[] bytesToHash = Encoding.UTF8.GetBytes(toHash);
            byte[] keyBytes = HashBytes(HashAlgorithms[SupportedHashAlgorithm.Md5](), key);
            return CipherString(algorithm, bytesToHash, keyBytes);
        }

        private static string CipherString(SymmetricAlgorithm algorithm, byte[] toHash, byte[] key)
        {
            var bytesHashed = CipherBytes(algorithm, toHash, key);
            return Convert.ToBase64String(bytesHashed);
        }

        private static byte[] CipherBytes(SymmetricAlgorithm algorithm, string toHash, string key)
        {
            byte[] bytesToHash = Encoding.UTF8.GetBytes(toHash);
            byte[] keyBytes = HashBytes(HashAlgorithms[SupportedHashAlgorithm.Md5](), key);
            return CipherBytes(algorithm, bytesToHash, keyBytes);
        }

        private static byte[] CipherBytes(SymmetricAlgorithm algorithm, byte[] toHash, byte[] key)
        {
            algorithm.Key = key;
            try
            {
                var encryptor = algorithm.CreateEncryptor();
                return encryptor.TransformFinalBlock(toHash, 0, toHash.Length);
            }
            finally
            {
                algorithm.Clear();
                algorithm.Dispose();
            }
        }

        private static string DecipherString(SymmetricAlgorithm algorithm, string toHash, string key)
        {
            byte[] keyBytes = HashBytes(HashAlgorithms[SupportedHashAlgorithm.Md5](), key);
            return DecipherString(algorithm, Convert.FromBase64String(toHash), keyBytes);
        }

        private static string DecipherString(SymmetricAlgorithm algorithm, byte[] toHash, byte[] key)
        {
            return Encoding.UTF8.GetString(DecipherBytes(algorithm, toHash, key));
        }

        private static byte[] DecipherBytes(SymmetricAlgorithm algorithm, string toHash, string key)
        {
            byte[] keyBytes = HashBytes(HashAlgorithms[SupportedHashAlgorithm.Md5](), key);
            return DecipherBytes(algorithm, Convert.FromBase64String(toHash), keyBytes);
        }

        private static byte[] DecipherBytes(SymmetricAlgorithm algorithm, byte[] toHash, byte[] key)
        {
            algorithm.Key = key;
            try
            {
                var encryptor = algorithm.CreateDecryptor();
                return encryptor.TransformFinalBlock(toHash, 0, toHash.Length);
            }
            finally
            {
                algorithm.Clear();
                algorithm.Dispose();
            }
        }

        private static CryptographicException GetAlgorithmNotSupported(object algorithm)
        {
            return new CryptographicException(string.Format(Properties.Strings.Cryptography_AlgorithmNotSupported, algorithm));
        }
    }
}
