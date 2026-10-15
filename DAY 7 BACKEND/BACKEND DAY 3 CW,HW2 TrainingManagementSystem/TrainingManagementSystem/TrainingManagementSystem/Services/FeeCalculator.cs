
using System;

namespace TrainingManagementSystem.Services
{
    public class FeeCalculator
    {
        // Overload 1: Fee without discounts.
        public (decimal BaseFee, decimal DiscountAmount, decimal FinalFee)
            CalculateFee(decimal baseFee, bool isOnline)
        {
            ValidateBaseFee(baseFee);

            decimal surcharge = isOnline ? 200m : 800m;
            decimal discount = 0m;
            decimal finalFee = baseFee + surcharge;

            return (baseFee, discount, finalFee);
        }

        // Overload 2: Fee with discounts.
        public (decimal BaseFee, decimal DiscountAmount, decimal FinalFee)
            CalculateFee(
                decimal baseFee,
                decimal earlyBirdDiscount,
                decimal groupDiscount,
                bool isOnline)
        {
            ValidateBaseFee(baseFee);

            if (earlyBirdDiscount < 0 || groupDiscount < 0)
            {
                throw new ArgumentException(
                    "Discounts cannot be negative.");
            }

            decimal totalDiscount =
                earlyBirdDiscount + groupDiscount;

            if (totalDiscount > baseFee)
            {
                throw new ArgumentException(
                    "Total discount cannot exceed the base fee.");
            }

            decimal surcharge = isOnline ? 200m : 800m;
            decimal finalFee = baseFee - totalDiscount + surcharge;

            return (baseFee, totalDiscount, finalFee);
        }

        private void ValidateBaseFee(decimal baseFee)
        {
            if (baseFee < 0)
            {
                throw new ArgumentException(
                    "Base fee cannot be negative.");
            }
        }
    }
}
