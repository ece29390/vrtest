using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VRTest.Models
{
    public class NPVModel
    {
        [Display(Name = "Cash Flows")]
        public string CashFlowsDescription { get; set; }
        public double Increment { get; set; }
        [Display(Name = "Upper Bound Discount Rate")]
        public double UpperBoundDiscountRate { get; set; }
        [Display(Name = "Lower Bound Discount Rate")]
        public double LowerBoundDiscountRate { get; set; }
        [Display(Name = "Initial Cost")]
        public double InitialCost { get; set; }
        
    }
}
