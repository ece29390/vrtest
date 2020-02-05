using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VRTest.Common.Services
{
    public class HttpService:IHttpService
    {
        public HttpService()
        {
            
        
        }

        public async Task<string> GetAsJson<T>(string url, T requestModel) where T:IConvertToUriParameter
        {
            var uriParameter = requestModel.ConvertToUriParameter();
            var returnJson = string.Empty;

            using (var client = new HttpClient())
            {
                var uri = $"{url}/{uriParameter}";
                var response = await client.GetAsync(uri);
                if(response.StatusCode==System.Net.HttpStatusCode.OK)
                {
                    returnJson = await response.Content.ReadAsStringAsync();
                }
            }

            return returnJson;
        }

        public async Task<string> PostAsyncReturnAsJson<R>(string url, R requestObject)        {
            var returnJson = string.Empty;
            var json = JsonConvert.SerializeObject(requestObject);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(url, data);
                returnJson = await response.Content.ReadAsStringAsync();
               
            }
            return returnJson;   
        }
    }
}
