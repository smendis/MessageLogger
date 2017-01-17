using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MessageLogger.Web.Controllers
{
    public class BaseController : Controller
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
        
        public async Task<TResponse> PostWebServiceObject<TResponse>(string relativeUri, string authorizationHeader, object data)
        {
            var uri = string.Concat(BaseAddress, "/", relativeUri);
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(uri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if (authorizationHeader != null)
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authorizationHeader);

                    var response = await client.PostAsync(uri, data, new JsonMediaTypeFormatter());
                    response.EnsureSuccessStatusCode();
                    var text = await response.Content.ReadAsStringAsync();
                    return await JsonConvert.DeserializeObjectAsync<TResponse>(text);

                }
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
    }
}