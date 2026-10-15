
namespace TrainingManagementSystem.Models
{
    public readonly struct FeeSummary
    {
        public decimal BaseFee { get; }
        public decimal DiscountAmount { get; }
        public decimal FinalFee { get; }

        public FeeSummary(
            decimal baseFee,
            decimal discountAmount,
            decimal finalFee)
        {
            BaseFee = baseFee;
            DiscountAmount = discountAmount;
            FinalFee = finalFee;
        }
    }
}
