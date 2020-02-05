using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VRTest.Common.Services;
namespace vrtest.angular.app
{
    public class NpvModel:IConvertToUriParameter
    {
        public string CashFlows { get; set; }
        public double Increment { get; set; }
        public double UpperBound { get; set; }
        public double LowerBound { get; set; }
        public double InitialCost { get; set; }

        public string ConvertToUriParameter()
        {
            return $"{CashFlows}/{InitialCost}/{UpperBound}/{LowerBound}/{Increment}";
        }
    }
}
