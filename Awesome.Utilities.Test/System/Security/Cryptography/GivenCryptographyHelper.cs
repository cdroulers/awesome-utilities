using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Security.Cryptography;

namespace Awesome.Utilities.Test.System.Security.Cryptography
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class GivenCryptographyHelper
    {
        [TestCase("chris@invup.com", "00bd20324418074d88d86e562e78ed0b", SupportedHashAlgorithm.Md5)]
        [TestCase("chris@invup.com", "1af8f2f439d452bbfd2f7242b71dc9a6a4c94a11", SupportedHashAlgorithm.Sha1)]
        [TestCase("chris@invup.com", "988a1e44fe6a1a4d68f74149847c1fd2a15a819ad51b70d2e36d0cf831bf7ed1", SupportedHashAlgorithm.Sha256)]
        [TestCase("chris@invup.com", "20e6403774a7298c828357f5afc5c698147ab9668cd8f984ec7ef9ca54ae9f4a634ec678fd8ea8c6e73fe6d6d549d76892086462f75515066ac2c11a8089a9c8", SupportedHashAlgorithm.Sha512)]
        public void When_hashing_Then_works(string toHash, string expected, SupportedHashAlgorithm algo)
        {
            string actual = CryptographyHelper.Hash(algo, toHash);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
