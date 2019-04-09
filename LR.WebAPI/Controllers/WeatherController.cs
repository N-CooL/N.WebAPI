using LR.WebAPI.Common;
using LR.WebAPI.Models;
using System;
using System.Web.Http;

namespace LR.WebAPI.Controllers
{
    [Authorize]
    public class WeatherController : BaseController
    {
        // GET: api/Weather/CityName
        [HttpGet]
        [Route("api/Weather/CityName/{cityName},{country}")]
        public ResponseData<WeatherData> City(string cityName, string country)
        {
            string url = String.Format("weather?q={0},{1}&APPID={2}", cityName, country, BaseConstants.OpenWeatherAPPID);
            ResponseData<WeatherData> weatherInfo = APIAnonymousGetCaller<WeatherData>(url);
            return NullDataCheck(weatherInfo);
        }

        // GET: api/Weather/CityZip
        [HttpGet]
        [Route("api/Weather/CityZip/{cityZip},{country}")]
        public ResponseData<WeatherData> CityZip(string cityZip, string country)
        {
            string url = String.Format("weather?zip={0},{1}&APPID={2}", cityZip, country, BaseConstants.OpenWeatherAPPID);
            ResponseData<WeatherData> weatherInfo = APIAnonymousGetCaller<WeatherData>(url);

            return NullDataCheck(weatherInfo);
        }
        private ResponseData<WeatherData> NullDataCheck(ResponseData<WeatherData> data)
        {
            if (data.Entity != null)
            {
                data.IsSuccess = (data.Entity.Base != null);
                if (data.IsSuccess)
                {
                    data.Message = "Success";
                }
                else
                {
                    data.Message = "Not valid input.";
                    data.Entity = null;
                }
            }
            return data;
        }

    }
}





