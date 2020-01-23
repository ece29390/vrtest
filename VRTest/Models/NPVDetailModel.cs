using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VRTest.Models
{
    public class NPVDetailModel
    {
     
        public string CashFlowSummary { get; set; }
        public double InitialCost { get; set; }
        public double DiscountRate{get;set;}
        public double NPV { get; set; }
    }
}
