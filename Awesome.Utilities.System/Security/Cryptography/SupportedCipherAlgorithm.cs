using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Security.Cryptography
{
    /// <summary>
    ///     Supported Cipher Algorithm for CryptographyHelper
    /// </summary>
    public enum SupportedCipherAlgorithm
    {
        /// <summary>
        ///     The Rijndael cipher algorithm
        /// </summary>
        Rijndael,
        /// <summary>
        ///     The Triple DES cipher algorithm
        /// </summary>
        TripleDes,
    }
}
