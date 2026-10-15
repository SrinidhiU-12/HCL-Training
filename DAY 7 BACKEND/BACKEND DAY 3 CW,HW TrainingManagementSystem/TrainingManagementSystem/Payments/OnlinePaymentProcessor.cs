
using System;

namespace TrainingManagementSystem.Payments
{
    public class OnlinePaymentProcessor : IPaymentProcessor
    {
        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine("Processing online payment...");

            if (amount <= 0)
            {
                Console.WriteLine("Payment failed: Invalid amount.");
                return false;
            }

            Console.WriteLine("Online payment successful.");
            return true;
        }
    }
}
