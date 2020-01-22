using System;
using System.Collections.Generic;
using NPVEngine;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Linq;

namespace VRTest.NPVEngine
{
    public class NPVEngine
    {    
        public double CalculateIndividualNPV(CalculateIndividualNPVRequest calculateIndividualNPVRequest)
        {
            double pvOfExpectedCashFlows = 0;
            var yearCount = 1;
            foreach (var cashFlow in calculateIndividualNPVRequest.CashFlows)
            {
                pvOfExpectedCashFlows += CalculatePVOfExpectedCashFlow
                    (
                    cashFlow
                    , calculateIndividualNPVRequest.Interest
                    , yearCount
                    );
                yearCount += 1;

            }

            var npv = pvOfExpectedCashFlows + calculateIndividualNPVRequest.InitialValue;
            return npv;
        }

        private double CalculatePVOfExpectedCashFlow(double cashFlow
            , double interest
            , int cashFlowYearNumber)
        {
            var retValue = (cashFlow) / Math.Pow(1 + interest, cashFlowYearNumber);
            return retValue;
        }

        private ConcurrentBag<double> BuildConcurrentCollectionOfInterest(GetTotalDiscountRateIncrementRowsRequest request)
        {
            var retValue = new ConcurrentBag<double>();
            var lowerLimitDiscountRate = request.LowerBoundDiscountRate;
            while(lowerLimitDiscountRate<=request.UpperBoundDiscountRate)
            {
                retValue.Add(lowerLimitDiscountRate);
                lowerLimitDiscountRate += request.Increment;
            }
            return retValue;
        }

        private void ValidateInputs(params IValidateInputs[] inputs)
        {
            foreach(var input in inputs)
            {
                input.ValidateInputs();
            }
        }

        public List<NPVSet> CalculateSetOfNPV(CalculateSetOfNPVRequest request)
        {
            var totalDiscountInterestRateRequest = new GetTotalDiscountRateIncrementRowsRequest
            {
                Increment = request.Increment
                ,
                UpperBoundDiscountRate = request.UpperBoundDiscountRate
                ,
                LowerBoundDiscountRate = request.LowerBoundDiscountRate
            };

           
            ValidateInputs(totalDiscountInterestRateRequest, request);

            var listOfInterests = BuildConcurrentCollectionOfInterest(totalDiscountInterestRateRequest);

            var dictionary = new ConcurrentDictionary<double, NPVSet>();

            Parallel.ForEach(listOfInterests, (interest) =>
            {
                var discountRate = interest / 100;
                var npv = CalculateIndividualNPV(new CalculateIndividualNPVRequest {
                    CashFlows = request.CashFlow
                    ,InitialValue = request.InitialCost
                    ,Interest = discountRate
                });

                var npvSet = new NPVSet
                {
                    CashFlowSummary = string.Join(", ", request.CashFlow.ToArray())
                    ,
                    DiscountRate = interest
                    ,
                    InitialCost = Math.Abs(request.InitialCost)
                    ,
                    NPV = Math.Round(npv,2)
                };

                dictionary.AddOrUpdate(interest, npvSet, (dr, set) => npvSet);
            });

            var listOfKeys = dictionary.Keys.ToList();
            listOfKeys.Sort();

            var finalList = new List<NPVSet>();
            listOfKeys.ForEach(k =>
            {
                finalList.Add(dictionary[k]);
            });
            return finalList;
        }
    }
}
