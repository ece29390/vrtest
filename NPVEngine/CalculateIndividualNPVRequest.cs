using System.Collections.Generic;

namespace NPVEngine
{
    public class CalculateIndividualNPVRequest
    {
        public List<double> CashFlows { get; set; }
        public double Interest { get; set; }
        public double InitialValue { get; set; }
    }
}