using LR.WebAPI.Common;
using LR.WebAPI.Models;
using System.Net;
using System.Web.Http;

namespace LR.WebAPI.Controllers
{
    public class BaseController : ApiController
    {
        #region Restsharp Call for Weather API
        protected ResponseData<TResponse> APIPostCaller<TRequest, TResponse>(string url, TRequest request, string authToken)
        {
            url = BaseConstants.OpenWeatherAPIPath + url;
            ResponseData<TResponse> response = url.ToPostAPI<TRequest, TResponse>(request, authToken);
            return response;
        }

        protected ResponseData<T> APIGetCaller<T>(string url, string authToken)
        {
            url = BaseConstants.OpenWeatherAPIPath + url;
            ResponseData<T> response = url.ToGetAPI<T>(authToken);
            return response;
        }

        protected ResponseData<TResponse> APIAnonymousPostCaller<TRequest, TResponse>(string url, TRequest request)
        {
            url = BaseConstants.OpenWeatherAPIPath + url;
            ResponseData<TResponse> response = url.ToPostAPI<TRequest, TResponse>(request, "");
            return response;
        }


        protected ResponseData<T> APIAnonymousGetCaller<T>(string url)
        {
            url = BaseConstants.OpenWeatherAPIPath + url;
            ResponseData<T> response = url.ToGetAPI<T>("");
            return response;
        }


        

        #endregion
    }
}
