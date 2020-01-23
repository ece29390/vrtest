using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VRTest.Models
{
    public class NPVRequestModel
    {
        public List<double> CashFlow { get; set; }
        public double UpperBoundDiscountRate { get; set; }
        public double LowerBoundDiscountRate { get; set; }
        public double Increment { get; set; }
        public double InitialCost { get; set; }
    }
}
