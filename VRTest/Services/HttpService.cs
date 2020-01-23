using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VRTest.Services
{
    public class HttpService:IHttpService
    {
        public HttpService()
        {
            
        
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
