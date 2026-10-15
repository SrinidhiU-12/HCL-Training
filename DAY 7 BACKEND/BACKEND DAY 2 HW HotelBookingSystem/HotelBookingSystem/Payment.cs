namespace HotelBookingSystem
{
    public interface IPaymentProcessor
    {
        bool ProcessPayment(decimal amount);
    }

    // Implicit implementation
    public class CashPayment : IPaymentProcessor
    {
        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Cash payment received: ₹{amount}");
            return true;
        }
    }

    // Explicit implementation
    public class CardPayment : IPaymentProcessor
    {
        bool IPaymentProcessor.ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Card/Digital payment processed: ₹{amount}");

            Console.Write("Enter payment success (yes/no): ");
            string input = Console.ReadLine();

            return input.ToLower() == "yes";
        }
    }
}