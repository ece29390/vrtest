using NPVEngine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using VRTest.NPVEngine;
namespace VRTest.UnitTest
{
   [TestFixture]
    public class NPVEngineTest
    {
 
        [TestCase(0.01,-10000,4463.71)]
        [TestCase(0.0125, -10000, 4333.81)]
        [TestCase(0.015, -10000, 4205.53)]
        [TestCase(0.0175, -10000, 4078.85)]
        [TestCase(0.045, -10000, 2783.79)]
        public void CalculateIndividualNPV_Shall_Satisfy_Expected(double interest, double initialValue, double expectedNPV)
        {
            var npvEngine = new NPVEngine.NPVEngine();
            var cashFlows = new List<double> { 1000, 2000, 3000, 4000, 5000 };
            var npv = npvEngine.CalculateIndividualNPV(
                new CalculateIndividualNPVRequest
                {
                    CashFlows = cashFlows
                    ,Interest = interest
                    ,InitialValue = initialValue
                }
                );

            Assert.AreEqual(expectedNPV, Math.Round(npv, 2));
        }
        [TestCase(-1000,1.5,1.0,.25)]
        [TestCase(-1000, 2.5, 1.0, .25)]
        [TestCase(-1000, 2.5, 1.0, .5)]
        public void CalculateSetOfNPV_Given_All_Parameters_Is_Valid(
            double initialCost
            ,double upperLimit
            ,double lowerLimit
            ,double increment)
        {
            var request = new CalculateSetOfNPVRequest
            {
                CashFlow=new List<double>
                {
                    1000
                    ,1500
                    ,2000
                    ,2500
                    ,3000
                },
                UpperBoundDiscountRate = upperLimit
                ,LowerBoundDiscountRate = lowerLimit
                ,Increment=increment
                ,InitialCost = initialCost
            };
            var cashFlowSummary = string.Join(", ", request.CashFlow.ToArray());
            var npvEngine = new NPVEngine.NPVEngine();
            var expectedOutput = new List<NPVSet>();
          
            while (lowerLimit<=upperLimit)
            {
                expectedOutput.Add(new NPVSet
                {
                    CashFlowSummary = cashFlowSummary
                    ,InitialCost = initialCost*-1
                    ,DiscountRate = lowerLimit
                    ,NPV = npvEngine.CalculateIndividualNPV(new CalculateIndividualNPVRequest
                    {
                        CashFlows = request.CashFlow
                        ,
                        InitialValue = request.InitialCost
                        ,
                        Interest = lowerLimit / 100
                    })
                });
                lowerLimit += increment;
            }

            var actualOutput = npvEngine.CalculateSetOfNPV(request);

            for(var i=0;i<expectedOutput.Count;i++)
            {
                Assert.AreEqual(expectedOutput[i].CashFlowSummary, actualOutput[i].CashFlowSummary);
                Assert.AreEqual(expectedOutput[i].DiscountRate, actualOutput[i].DiscountRate);
                Assert.AreEqual(Math.Round(expectedOutput[i].NPV,2), actualOutput[i].NPV);
            }
        }

        [Test]
        public void CalculateSetOfNPV_Given_Increment_Is_Invalid_Expect_Exception()
        {
            var npvEngine = new NPVEngine.NPVEngine();
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
                UpperBoundDiscountRate = 1.5
                ,
                LowerBoundDiscountRate = 1.0
                ,
                Increment = .8
                ,
                InitialCost = -1000
            };
            Assert.Throws<ArgumentException>(() => npvEngine.CalculateSetOfNPV(request));
        }

        [Test]
        public void CalculateSetOfNPV_Given_UpperBound_Is_Lesser_Than_LowerBound_Expect_Exception()
        {
            var npvEngine = new NPVEngine.NPVEngine();
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
                UpperBoundDiscountRate = .75
                ,
                LowerBoundDiscountRate = 1.0
                ,
                Increment = .25
                ,
                InitialCost = -1000
            };
            Assert.Throws<ArgumentException>(() => npvEngine.CalculateSetOfNPV(request));
        }

        [Test]
        public void CalculateSetOfNPV_Given_UpperBound_Is_Equals_LowerBound_Expect_Exception()
        {
            var npvEngine = new NPVEngine.NPVEngine();
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
                UpperBoundDiscountRate = 1.0
                ,
                LowerBoundDiscountRate = 1.0
                ,
                Increment = .25
                ,
                InitialCost = -1000
            };
            Assert.Throws<ArgumentException>(() => npvEngine.CalculateSetOfNPV(request));
        }

        [Test]
        public void CalculateSetOfNPV_Given_InitialValue_Is_Not_Negative_Exception()
        {
            var npvEngine = new NPVEngine.NPVEngine();
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
                UpperBoundDiscountRate = 2.5
                ,
                LowerBoundDiscountRate = 1.0
                ,
                Increment = .25
                ,
                InitialCost = 1000
            };
            Assert.Throws<ArgumentException>(() => npvEngine.CalculateSetOfNPV(request));
        }


    }
}
