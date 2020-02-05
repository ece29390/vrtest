using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VRTest.Common.Services;

namespace VRTest.Common.Models
{
    public class NPVRequestModel:IConvertToUriParameter
    {
        public IList<double> CashFlow { get; set; }
        public double UpperBoundDiscountRate { get; set; }
        public double LowerBoundDiscountRate { get; set; }
        public double Increment { get; set; }
        public double InitialCost { get; set; }

        public string ConvertToUriParameter()
        {
            return $"{string.Join(",",CashFlow)}/{InitialCost}/{UpperBoundDiscountRate}/{LowerBoundDiscountRate}/{Increment}";
        }

       
    }
}
