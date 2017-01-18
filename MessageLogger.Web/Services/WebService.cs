using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace MessageLogger.Web.Services
{
    public class WebService : IWebService
    {
        private static HttpClient client;
        protected HttpClient Client
        {
            get
            {
                if (client == null)
                    client = new HttpClient();
                return client;
            }
        }

        private static string _baseAddress;
        private string BaseAddress
        {
            get
            {
                if (_baseAddress == null)
                    _baseAddress = ConfigurationManager.AppSettings["WebApiBaseAddress"];

                return _baseAddress;
            }
        }
        
        public async Task<T> Post<T>(string relativeUri, string authorizationHeader, object data)
        {
            T returnValue = default(T);
            var uri = string.Concat(BaseAddress, "/", relativeUri);
            try
            {
                using (var client = new HttpClient())
                {
                    var jsonString = new JavaScriptSerializer().Serialize(data);
                    var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                    client.BaseAddress = new Uri(uri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if (authorizationHeader != null)
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authorizationHeader);

                    HttpResponseMessage response = await client.PostAsync(uri, content);
                    response.EnsureSuccessStatusCode();
                    returnValue = JsonConvert.DeserializeObject<T>(((HttpResponseMessage)response).Content.ReadAsStringAsync().Result);
                }
                return returnValue;
            }
            catch (Exception e)
            {
                Trace.TraceError("Error thrown from Post method in WebService. Error:" + e.Message);
                throw (e);
            }
        }
    }
}