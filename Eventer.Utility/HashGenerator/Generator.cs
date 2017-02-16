using System;
using System.Security.Cryptography;
using System.Text;

namespace Eventer.Utility.HashGenerator
{
    public class Generator : IGenerator
    {
        public string GenerateGuid()
            => Guid.NewGuid().ToString();

        public string GenerateClientSecret()
        {
            using (var cryptoRandomDataGenerator = new RNGCryptoServiceProvider())
            {
                var buffer = new byte[26];
                cryptoRandomDataGenerator.GetBytes(buffer);
                return Convert.ToBase64String(buffer);
            }
        }

        public string GetHash(string input)
        {
            using (var hashAlgorithm = new SHA256CryptoServiceProvider())
            {
                var byteValue = Encoding.UTF8.GetBytes(input);
                var byteHash = hashAlgorithm.ComputeHash(byteValue);

                return Convert.ToBase64String(byteHash);
            }
        }
    }
}