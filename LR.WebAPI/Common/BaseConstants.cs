using System;
using System.Web.Configuration;

namespace LR.WebAPI.Common
{
    public class BaseConstants
    {
        public static readonly string OpenWeatherAPIPath = WebConfigurationManager.AppSettings["OpenWeatherAPIPath"];
        public static readonly string OpenWeatherAPPID = WebConfigurationManager.AppSettings["OpenWeatherAPPID"];

        public static readonly string AuthToken = WebConfigurationManager.AppSettings["AuthTokenName"];

        //Security Configuration
        public static readonly string KeyGenerationSalt = WebConfigurationManager.AppSettings["KeyGenerationSalt"];
        public static readonly int KeySize = Convert.ToInt32( WebConfigurationManager.AppSettings["KeySize"]);
    }
}
