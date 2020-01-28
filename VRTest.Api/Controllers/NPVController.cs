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
        
        [HttpGet("{cashFlowsDescription}/{initialCost}/{upperBoundDiscountRate}/{lowerBoundDiscountRate}/{incrementRate}")]
        public async Task<ActionResult<List<NPVSet>>> Get(
            string cashFlowsDescription
            , double initialCost
            ,double upperBoundDiscountRate
            ,double lowerBoundDiscountRate
            , double incrementRate)
        {
            var getNPVPreviousRequestBy = new NPVPreviousRequest
            {
                CashFlowsDescription = cashFlowsDescription
               ,
                IncrementRate = incrementRate
               ,
                InitialCost = initialCost
               ,
                LowerBoundDiscountRate = lowerBoundDiscountRate
               ,
                UpperBoundDiscountRate = upperBoundDiscountRate
            };
            
            var npvRequest = await _npvDataAccess.GetNPVPreviousRequestBy(getNPVPreviousRequestBy);
            if(npvRequest==null)
            {
                return Ok(null);
            }
            var npvSet = new List<NPVSet>();

            var previousResults = npvRequest.NPVPreviousResults.ToList();
            npvSet = previousResults.Select(pr => new NPVSet
            {
                CashFlowSummary = npvRequest.CashFlowsDescription
                    ,
                DiscountRate = pr.DiscountRate
                    ,
                InitialCost = npvRequest.InitialCost
                    ,
                NPV = pr.NPV
            }).ToList();

            return Ok(npvSet);
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
           
                var npvEngine = new NPVEngine.NPVEngine();
                request.InitialCost = request.InitialCost * -1;
                var npvSet = npvEngine.CalculateSetOfNPV(request);

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
           

           
            return Ok(npvSet);
        }
    }
}