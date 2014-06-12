using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Security.Cryptography
{
    /// <summary>
    ///     Cipher methods
    /// </summary>
    public interface ICryptographyCipher
    {
        /// <summary>
        /// Enciphers the string as Base64
        /// </summary>
        /// <param name="toEncipher">To encipher.</param>
        /// <returns>An enciphered string</returns>
        string ToBase64(string toEncipher);

        /// <summary>
        /// Deciphers the string from Base64
        /// </summary>
        /// <param name="toDecipher">To decipher.</param>
        /// <returns>An enciphered string</returns>
        string FromBase64(string toDecipher);

        /// <summary>
        /// Enciphers the string.
        /// </summary>
        /// <param name="algorithm">The algorithm.</param>
        /// <param name="toEncipher">To encipher.</param>
        /// <param name="key">The key.</param>
        /// <returns>An enciphered string</returns>
        string EncipherString(SupportedCipherAlgorithm algorithm, string toEncipher, string key);

        /// <summary>
        /// Enciphers the bytes.
        /// </summary>
        /// <param name="algorithm">The algorithm.</param>
        /// <param name="toEncipher">To encipher.</param>
        /// <param name="key">The key.</param>
        /// <returns>An enciphered byte array</returns>
        byte[] EncipherBytes(SupportedCipherAlgorithm algorithm, string toEncipher, string key);

        /// <summary>
        /// Enciphers the string.
        /// </summary>
        /// <param name="algorithm">The algorithm.</param>
        /// <param name="toEncipher">To encipher.</param>
        /// <param name="key">The key.</param>
        /// <returns>An enciphered string</returns>
        string EncipherString(SupportedCipherAlgorithm algorithm, byte[] toEncipher, byte[] key);

        /// <summary>
        /// Enciphers the bytes.
        /// </summary>
        /// <param name="algorithm">The algorithm.</param>
        /// <param name="toEncipher">To encipher.</param>
        /// <param name="key">The key.</param>
        /// <returns>An enciphered byte array</returns>
        byte[] EncipherBytes(SupportedCipherAlgorithm algorithm, byte[] toEncipher, byte[] key);

        /// <summary>
        /// Deciphers the string.
        /// </summary>
        /// <param name="algorithm">The algorithm.</param>
        /// <param name="toDecipher">To decipher.</param>
        /// <param name="key">The key.</param>
        /// <returns>A deciphered string</returns>
        string DecipherString(SupportedCipherAlgorithm algorithm, string toDecipher, string key);

        /// <summary>
        /// Deciphers the bytes.
        /// </summary>
        /// <param name="algorithm">The algorithm.</param>
        /// <param name="toDecipher">To decipher.</param>
        /// <param name="key">The key.</param>
        /// <returns>A deciphered byte array</returns>
        byte[] DecipherBytes(SupportedCipherAlgorithm algorithm, string toDecipher, string key);

        /// <summary>
        /// Deciphers the string.
        /// </summary>
        /// <param name="algorithm">The algorithm.</param>
        /// <param name="toDecipher">To decipher.</param>
        /// <param name="key">The key.</param>
        /// <returns>A deciphered string</returns>
        string DecipherString(SupportedCipherAlgorithm algorithm, byte[] toDecipher, byte[] key);

        /// <summary>
        /// Deciphers the bytes.
        /// </summary>
        /// <param name="algorithm">The algorithm.</param>
        /// <param name="toDecipher">To decipher.</param>
        /// <param name="key">The key.</param>
        /// <returns>A deciphered byte array</returns>
        byte[] DecipherBytes(SupportedCipherAlgorithm algorithm, byte[] toDecipher, byte[] key);
    }
}
