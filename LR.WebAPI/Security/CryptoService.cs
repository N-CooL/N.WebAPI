using LR.WebAPI.Common;
using System;
using System.Security.Cryptography;
using System.Text;

namespace LR.WebAPI.Security
{
    public class CryptoService : ICryptoService
    {

        private static RNGCryptoServiceProvider m_cryptoServiceProvider = null;

        public CryptoService()
        {
            m_cryptoServiceProvider = new RNGCryptoServiceProvider();
        }

        /// <summary>
        /// This method generate hased value for given text.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string GenerateHash(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException(nameof(text));
            }         

            return GenerateSHA512String(text);
        }

        /// <summary>
        /// This method generate salted hash.
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public string GenerateSaltedHash(string plainTextPassword, out string salt)
        {
            salt = GetSaltString();
            string finalString = $" {plainTextPassword.ToLower() }{ salt.ToLower()} {BaseConstants.KeyGenerationSalt}";
            return GenerateHash(finalString);
        }
        
        /// <summary>
        /// Generte  SHA512 hash String 
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        private string GenerateSHA512String(string inputString)
        {
            SHA512 sha512 = SHA512Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        private string GetSaltString()
        {
            byte[] saltBytes = new byte[BaseConstants.KeySize];
            m_cryptoServiceProvider.GetNonZeroBytes(saltBytes);
            string saltString = GetStringFromHash(saltBytes);
            return saltString;
        }


        /// <summary>
        /// Get Strign From Hash
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        private string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }


        

    }
}
