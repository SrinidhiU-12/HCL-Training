
using System;

namespace TrainingManagementSystem.Payments
{
    public class ClassroomPaymentProcessor : IPaymentProcessor
    {
        bool IPaymentProcessor.ProcessPayment(decimal amount)
        {
            Console.WriteLine("Processing classroom payment...");

            if (amount <= 0)
            {
                Console.WriteLine("Payment failed: Invalid amount.");
                return false;
            }

            Console.WriteLine("Classroom payment successful.");
            return true;
        }
    }
}
