using LR.WebAPI.Models;
using LR.WebAPI.Security;
using System.Web.Configuration;

namespace LR.WebAPI.Service
{
    public class UserService
    {
        public User GetUserByCredentials(string userName, string password)
        {
            User user;
            ICryptoService hashObj = new CryptoService();

            string passwordHash = hashObj.GenerateHash(password); //Generate Password hash.

            string storedPassword= WebConfigurationManager.AppSettings["Password"];
            string storedUserName= WebConfigurationManager.AppSettings["UserName"];

            if (userName.ToLower() == storedUserName.ToLower() && passwordHash == storedPassword)
            {
                user = new User() { Id = "1", Email = "ntest@domain.com", Name = userName };
            }
            else
            {
                user = null;
            }
            
            return user;
        }
    }
}