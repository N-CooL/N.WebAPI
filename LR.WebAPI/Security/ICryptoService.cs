
using System.Collections.Generic;

namespace LR.WebAPI.Security
{
    public interface ICryptoService
    {
        /// <summary>
        /// This method generate hased value for given text.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        string GenerateHash(string text);

        /// <summary>
        /// This method generate salted hash.
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        string GenerateSaltedHash(string plainText, out string salt);
        
       
    }
}
