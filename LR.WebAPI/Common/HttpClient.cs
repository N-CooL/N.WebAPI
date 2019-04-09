using LR.WebAPI.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace LR.WebAPI.Common
{
    public static class HttpClient
    {
        public static ResponseData<TResponse> ToPostAPI<TRequest, TResponse>(this string url, TRequest request, string authToken = "")
        {
            IRestResponse response = url.ToRestAPI<TRequest>(Method.POST.ToRestRequest(request, authToken));
            HandleErrorResponse(response);
            var data = JsonConvert.DeserializeObject<TResponse>(response.Content);
            return new ResponseData<TResponse>()
            {
                Entity = data
            };
        }

        public static ResponseData<T> ToGetAPI<T>(this string url, string authToken = "")
        {
            IRestResponse response = url.ToRestAPI<NullValueHandling>(Method.GET.ToRestRequest(authToken));
            HandleErrorResponse(response);
            var data = JsonConvert.DeserializeObject<T>(response.Content);
            return new ResponseData<T>()
            {
                Entity = data
            };

        }

        private static IRestResponse ToRestAPI<TRequest>(this string url, IRestRequest restRequest)
        {
            var client = new RestClient(url);
            IRestResponse restResponse = client.Execute(restRequest);
            return restResponse;
        }

        private static IRestRequest ToRestRequest(this Method requestMethod, string authToken = "")
        {
            //make request
            var restRequest = new RestRequest(requestMethod)
            {
                RequestFormat = DataFormat.Json
            };

            //set request header
            if (!string.IsNullOrWhiteSpace(authToken))
            {
                restRequest.AddHeader(BaseConstants.AuthToken, authToken);
            }

            return restRequest;
        }

        private static IRestRequest ToRestRequest<TRequest>(this Method requestMethod, TRequest request, string authToken = "")
        {
            //make request
            var restRequest = requestMethod.ToRestRequest(authToken);
            restRequest.JsonSerializer = NewtonsoftJsonSerializer.Default; // added for Date Format
            restRequest.AddBody(request);
            return restRequest;
        }

        private static void HandleErrorResponse(IRestResponse response)
        {
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
            {
                //var exception = JsonConvert.DeserializeObject<CatchExceptionData>(response.Content);
                //throw new Exception(exception.Message);
            }
        }
    }


    internal class NewtonsoftJsonSerializer : ISerializer, IDeserializer
    {
        private Newtonsoft.Json.JsonSerializer serializer;

        public NewtonsoftJsonSerializer(Newtonsoft.Json.JsonSerializer serializer)
        {
            this.serializer = serializer;
        }

        public string ContentType
        {
            get { return "application/json"; } // Probably used for Serialization?
            set { }
        }

        public string DateFormat { get; set; }

        public string Namespace { get; set; }

        public string RootElement { get; set; }

        public string Serialize(object obj)
        {
            using (var stringWriter = new System.IO.StringWriter())
            {
                using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                {
                    serializer.Serialize(jsonTextWriter, obj);

                    return stringWriter.ToString();
                }
            }
        }

        public T Deserialize<T>(RestSharp.IRestResponse response)
        {
            var content = response.Content;

            using (var stringReader = new System.IO.StringReader(content))
            {
                using (var jsonTextReader = new JsonTextReader(stringReader))
                {
                    return serializer.Deserialize<T>(jsonTextReader);
                }
            }
        }

        public static NewtonsoftJsonSerializer Default
        {
            get
            {
                return new NewtonsoftJsonSerializer(new Newtonsoft.Json.JsonSerializer()
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });
            }
        }
    }
}
