using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace VRTest.Entities
{
    public class NPVPreviousRequest
    {
        
        public int Id { get; set; }
        public string CashFlowsDescription { get; set; }
        public double InitialCost { get; set; }
        public double UpperBoundDiscountRate { get; set; }
        public double LowerBoundDiscountRate { get; set; }
        public double IncrementRate { get; set; }
        public ICollection<NPVPreviousResult> NPVPreviousResults { get; set; }
    }
}
