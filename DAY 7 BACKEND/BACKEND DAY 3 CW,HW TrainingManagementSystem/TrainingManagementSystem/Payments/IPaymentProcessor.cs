
namespace TrainingManagementSystem.Payments
{
    public interface IPaymentProcessor
    {
        bool ProcessPayment(decimal amount);
    }
}
