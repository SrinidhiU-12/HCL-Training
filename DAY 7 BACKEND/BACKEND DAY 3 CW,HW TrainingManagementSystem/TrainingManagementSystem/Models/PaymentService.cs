
using System;
using System.Collections.Generic;
using TrainingManagementSystem.Models;
using TrainingManagementSystem.Payments;

namespace TrainingManagementSystem.Services
{
    public class PaymentService
    {
        private readonly List<Payment> payments;
        private int nextPaymentId;

        public PaymentService(List<Payment> paymentHistory)
        {
            if (paymentHistory == null)
            {
                throw new ArgumentNullException(nameof(paymentHistory));
            }

            payments = paymentHistory;
            nextPaymentId = 1;

            for (int i = 0; i < payments.Count; i++)
            {
                if (payments[i] != null &&
                    payments[i].PaymentId >= nextPaymentId)
                {
                    nextPaymentId = payments[i].PaymentId + 1;
                }
            }
        }

        public bool ProcessPayment(
            Enrollment enrollment,
            IPaymentProcessor processor)
        {
            if (enrollment == null)
            {
                Console.WriteLine("Enrollment not found.");
                return false;
            }

            if (enrollment.Status == EnrollmentStatus.Cancelled)
            {
                Console.WriteLine(
                    "Payment rejected: Enrollment is cancelled.");
                return false;
            }

            if (processor == null)
            {
                Console.WriteLine("Payment processor is required.");
                return false;
            }

            bool success = processor.ProcessPayment(enrollment.FinalFee);

            PaymentStatus status = success
                ? PaymentStatus.Successful
                : PaymentStatus.Failed;

            Payment payment = new Payment(
                nextPaymentId,
                enrollment.EnrollmentId,
                enrollment.FinalFee,
                DateTime.Now,
                status);

            payments.Add(payment);
            nextPaymentId++;

            if (success)
            {
                enrollment.PaymentStatus = PaymentStatus.Successful;
                Console.WriteLine("Payment successful and recorded.");
            }
            else
            {
                Console.WriteLine("Failed payment attempt recorded.");
            }

            Console.WriteLine("Payment ID: " + payment.PaymentId);
            Console.WriteLine("Payment Status: " + payment.Status);

            return success;
        }

        public void DisplayPaymentHistory()
        {
            if (payments.Count == 0)
            {
                Console.WriteLine("No payment attempts recorded.");
                return;
            }

            for (int i = 0; i < payments.Count; i++)
            {
                Payment payment = payments[i];

                Console.WriteLine("------------------------------");
                Console.WriteLine("Payment ID: " + payment.PaymentId);
                Console.WriteLine("Enrollment ID: " + payment.EnrollmentId);
                Console.WriteLine("Amount: ₹" + payment.Amount);
                Console.WriteLine("Date: " + payment.PaymentDate);
                Console.WriteLine("Status: " + payment.Status);
            }
        }
    }
}
