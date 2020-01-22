using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPVEngine;
using VRTest.DataAccess;
using VRTest.Entities;

namespace VRTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NPVController : ControllerBase
    {
        private INPVDataAccess _npvDataAccess;

        public NPVController(INPVDataAccess npvDataAccess)
        {
            this._npvDataAccess = npvDataAccess;
        }

        [HttpPost]
        public async Task<ActionResult<List<NPVSet>>> Post([FromBody] CalculateSetOfNPVRequest request)
        {
            var getNPVPreviousRequestBy = new NPVPreviousRequest
            {
                CashFlowsDescription = string.Join(", ",request.CashFlow.ToArray())
                ,IncrementRate=request.Increment
                ,InitialCost = request.InitialCost
                ,LowerBoundDiscountRate = request.LowerBoundDiscountRate
                ,UpperBoundDiscountRate = request.UpperBoundDiscountRate
            };
            List<NPVSet> npvSet = null;
            var npvRequest = await _npvDataAccess.GetNPVPreviousRequestBy(getNPVPreviousRequestBy);

            if (npvRequest==null)
            {
                var npvEngine = new NPVEngine.NPVEngine();
                request.InitialCost = request.InitialCost * -1;
                npvSet = npvEngine.CalculateSetOfNPV(request);

                getNPVPreviousRequestBy.NPVPreviousResults = new List<NPVPreviousResult>();
                npvSet.ForEach(s => {
                    getNPVPreviousRequestBy.NPVPreviousResults.Add(
                        new NPVPreviousResult
                        {
                            DiscountRate = s.DiscountRate
                            ,NPV = s.NPV
                        }
                        );
                   
                });

                await _npvDataAccess.SaveNPVPreviousRequest(getNPVPreviousRequestBy);
            }
            else
            {
                npvSet = new List<NPVSet>();
                var previousResults = npvRequest.NPVPreviousResults.ToList();
                previousResults.ForEach(pr =>
                {
                    npvSet.Add(new NPVSet
                    {
                        CashFlowSummary = npvRequest.CashFlowsDescription
                        ,DiscountRate = pr.DiscountRate
                        ,InitialCost = npvRequest.InitialCost
                        ,NPV= pr.NPV
                    });
                });
            }

           
            return Ok(npvSet);
        }
    }
}