using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VRTest.Services
{
    public interface IHttpService
    {
        Task<string> PostAsyncReturnAsJson< R>(string url,R requestObject);
    }
}
