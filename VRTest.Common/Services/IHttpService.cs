using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VRTest.Common.Services
{
    public interface IHttpService
    {
        Task<string> PostAsyncReturnAsJson<R>(string url,R requestObject);
        Task<string> GetAsJson<T>(string url, T requestModel) where T:IConvertToUriParameter;
    }
}
