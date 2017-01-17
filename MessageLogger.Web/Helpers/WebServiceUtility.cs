using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace MessageLogger.Web.Helpers
{
    public class WebServiceUtility
    {
        private string _baseAddress;
        public string BaseAddress
        {
            get
            {
                if (_baseAddress == null)
                    _baseAddress = ConfigurationManager.AppSettings["WebApiBaseAddress"];

                return _baseAddress;
            }
        }

        public async Task<T> PostWebServiceObject<T>(string relativeUri, string authorizationHeader, object data)
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
                throw (e);
            }
        }
    }
}