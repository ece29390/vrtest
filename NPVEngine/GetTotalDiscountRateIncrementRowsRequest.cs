using System;

namespace NPVEngine
{
    public class GetTotalDiscountRateIncrementRowsRequest:IValidateInputs
    {
        public double UpperBoundDiscountRate { get; set; }
        public double LowerBoundDiscountRate { get; set; }
        public double Increment { get; set; }

        public void ValidateInputs()
        {
            if(UpperBoundDiscountRate%Increment!=0)
            {
                throw new ArgumentException("Increment doesn't match UpperBoundDiscountRate");
            }

            if(UpperBoundDiscountRate<=LowerBoundDiscountRate)
            {
                throw new ArgumentException("UpperBoundDiscountRate should be greater than LowerBoundDiscountRate");
            }
        }
    }
}