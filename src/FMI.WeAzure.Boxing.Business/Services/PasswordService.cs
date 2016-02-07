using FMI.WeAzure.Boxing.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Business.Services
{
    internal sealed class PasswordService : IPasswordService
    {
        private static readonly RNGCryptoServiceProvider CspRng = new RNGCryptoServiceProvider();
        private const int SaltSizeBytes = 32;
        private const int HashSizeBytes = 32;
        private const string SaltDelimiter = ":";

        public string CreateHash(string password)
        {
            var salt = new byte[SaltSizeBytes];
            CspRng.GetBytes(salt);

            var hash = PBKDF2(password, salt);
            return 
                Convert.ToBase64String(salt) + ":" +
                Convert.ToBase64String(hash);
        }

        public bool ValidatePassword(string password, string correctHash)
        {
            var splitHash = correctHash.Split(new[] { SaltDelimiter }, StringSplitOptions.None);
            if (splitHash.Length != 2)
            {
                throw new ArgumentException("Provided correct hash is not in the expected format");
            }

            var salt = Convert.FromBase64String(splitHash[0]);
            var expectedHash = Convert.FromBase64String(splitHash[1]);

            var testHash = PBKDF2(password, salt);
            return testHash.SequenceEqual(expectedHash);
        }

        private static byte[] PBKDF2(string password, byte[] salt)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            return pbkdf2.GetBytes(HashSizeBytes);
        }

    }
}
