using System;
using System.Collections.Generic;
using System.Text;

namespace VRTest.Entities
{
    public class NPVPreviousResult
    {
        public int Id { get; set; }
        public int NPVPreviousRequestId { get; set; }
        public double DiscountRate { get; set; }
        public double NPV { get; set; }

        public NPVPreviousRequest NPVPreviousRequest { get; set; }
    }
}
