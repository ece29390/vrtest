using Microsoft.AspNetCore.Mvc;
using NPVEngine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using VRTest.Api.Controllers;
using Moq;
using VRTest.DataAccess;
using VRTest.Entities;
using System.Threading.Tasks;

namespace VRTest.UnitTest.VRTest_Api
{
    [TestFixture]
    public class NPVControllerTest
    {
        private Mock<INPVDataAccess> _npvDataAccessMock;
        private NPVController _controller;

        [SetUp]
        public void Setup()
        {
            _npvDataAccessMock = new Mock<INPVDataAccess>(MockBehavior.Strict);

            _controller = new NPVController(_npvDataAccessMock.Object);
        }

        [TestCase(1.5,1.0,.25,1000)]
        [TestCase(2.5, 1.0, .25, 1000)]
        [TestCase(2.5, 1.0, .5, 1000)]
        public void Post_Given_Request_Has_Not_Been_Made_Before_Returns_Ok(
            double upperLimit
            ,double lowerLimit
            ,double increment
            ,double initialCost
            )
        {
            var request = new CalculateSetOfNPVRequest
            {
                CashFlow = new List<double>
                {
                    1000
                    ,1500
                    ,2000
                    ,2500
                    ,3000
                },
                UpperBoundDiscountRate = upperLimit
               ,
                LowerBoundDiscountRate = lowerLimit
               ,
                Increment = increment
               ,
                InitialCost = initialCost
            };
           

            var cashFlowSummary = string.Join(", ", request.CashFlow.ToArray());
            var npvEngine = new NPVEngine.NPVEngine();
            var expectedOutput = new List<NPVSet>();
            var npvPreviousResults = new List<NPVPreviousResult>();
            var id = 1;
            while (lowerLimit <= upperLimit)
            {
                var npv = npvEngine.CalculateIndividualNPV(new CalculateIndividualNPVRequest
                {
                    CashFlows = request.CashFlow
                        ,
                    InitialValue = request.InitialCost * -1
                        ,
                    Interest = lowerLimit / 100
                });

                expectedOutput.Add(new NPVSet
                {
                    CashFlowSummary = cashFlowSummary
                    ,
                    InitialCost = initialCost *-1
                    ,
                    DiscountRate = lowerLimit
                    ,
                    NPV = npv
                });

                npvPreviousResults.Add(new NPVPreviousResult
                {
                    Id =id
                    ,DiscountRate = lowerLimit
                    ,NPV = Math.Round(npv,2)
                    
                });
                lowerLimit += increment;
                id += 1;
            }

          

            var nPVPreviousRequestAfterSave = new NPVPreviousRequest
            {
                CashFlowsDescription = cashFlowSummary
                ,
                Id = 1
                ,
                IncrementRate = increment
                ,
                InitialCost = initialCost
                ,
                LowerBoundDiscountRate = lowerLimit
                ,
                UpperBoundDiscountRate = upperLimit
                ,
                NPVPreviousResults = npvPreviousResults
            };
            _npvDataAccessMock.Setup(n => n.SaveNPVPreviousRequest(It.IsAny<NPVPreviousRequest>()))
                .Returns(Task.FromResult(nPVPreviousRequestAfterSave));


            var result = _controller.Post(request);
            var actualResult = result.Result.Result as OkObjectResult;
            var actualOutput = actualResult.Value as List<NPVSet>;

            for (var i = 0; i < expectedOutput.Count; i++)
            {
                Assert.AreEqual(expectedOutput[i].CashFlowSummary, actualOutput[i].CashFlowSummary);
                Assert.AreEqual(expectedOutput[i].DiscountRate, actualOutput[i].DiscountRate);
                Assert.AreEqual(Math.Round(expectedOutput[i].NPV,2), actualOutput[i].NPV);
            }

            _npvDataAccessMock.VerifyAll();
        }

        
    }
}
