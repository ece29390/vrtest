using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using VRTest.Common.Services;

namespace vrtest.angular.app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NpvDataController : Controller
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _configuration;
        private readonly string _npvApiUrl;

        public NpvDataController(IHttpService httpService, IConfiguration configuration)
        {
            _httpService = httpService;
            _configuration = configuration;
            _npvApiUrl = configuration["Endpoints:vrtest.api.npv"];
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CalculateNpv([FromBody]NpvModel model)
        {
            
            var npvResults = await _httpService.GetAsJson(_npvApiUrl, model);
            if (string.IsNullOrEmpty(npvResults))
            {
                npvResults = await _httpService.PostAsyncReturnAsJson(_npvApiUrl, model);
            }

         
            return Ok(npvResults);
        }
    }
}