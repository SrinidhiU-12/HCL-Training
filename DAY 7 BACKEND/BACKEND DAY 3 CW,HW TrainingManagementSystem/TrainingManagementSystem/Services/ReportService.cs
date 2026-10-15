
using System;
using System.Collections.Generic;
using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.Services
{
    public class ReportService
    {
        private readonly List<Enrollment> enrollments;
        private readonly List<Payment> payments;

        public ReportService(
            List<Enrollment> enrollmentData,
            List<Payment> paymentData)
        {
            if (enrollmentData == null)
            {
                throw new ArgumentNullException(nameof(enrollmentData));
            }

            if (paymentData == null)
            {
                throw new ArgumentNullException(nameof(paymentData));
            }

            enrollments = enrollmentData;
            payments = paymentData;
        }

        public void DisplayEnrollmentReport()
        {
            Console.WriteLine("\n===== ENROLLMENT REPORT =====");
            Console.WriteLine("Total Enrollments: " + enrollments.Count);

            int activeCount = 0;
            int cancelledCount = 0;
            int completedCount = 0;

            for (int i = 0; i < enrollments.Count; i++)
            {
                if (enrollments[i].Status == EnrollmentStatus.Active)
                {
                    activeCount++;
                }
                else if (enrollments[i].Status == EnrollmentStatus.Cancelled)
                {
                    cancelledCount++;
                }
                else if (enrollments[i].Status == EnrollmentStatus.Completed)
                {
                    completedCount++;
                }
            }

            Console.WriteLine("Active: " + activeCount);
            Console.WriteLine("Cancelled: " + cancelledCount);
            Console.WriteLine("Completed: " + completedCount);
        }

        public void DisplayPaymentReport()
        {
            Console.WriteLine("\n===== PAYMENT REPORT =====");
            Console.WriteLine("Total Payment Attempts: " + payments.Count);

            int successfulCount = 0;
            int failedCount = 0;
            decimal successfulAmount = 0m;

            for (int i = 0; i < payments.Count; i++)
            {
                if (payments[i].Status == PaymentStatus.Successful)
                {
                    successfulCount++;
                    successfulAmount += payments[i].Amount;
                }
                else if (payments[i].Status == PaymentStatus.Failed)
                {
                    failedCount++;
                }
            }

            Console.WriteLine("Successful Payments: " + successfulCount);
            Console.WriteLine("Failed Payments: " + failedCount);
            Console.WriteLine("Successful Payment Amount: ₹" + successfulAmount);
        }

        public void DisplayCourseOccupancy(Course[] courses)
        {
            Console.WriteLine("\n===== COURSE OCCUPANCY =====");

            for (int i = 0; i < courses.Length; i++)
            {
                int totalSeats = courses[i].TotalSeats;
                int availableSeats = courses[i].AvailableSeats;
                int occupiedSeats = totalSeats - availableSeats;

                double occupancy = totalSeats > 0
                    ? (double)occupiedSeats / totalSeats * 100
                    : 0;

                Console.WriteLine("\nCourse: " + courses[i].Title);
                Console.WriteLine("Total Seats: " + totalSeats);
                Console.WriteLine("Occupied Seats: " + occupiedSeats);
                Console.WriteLine("Available Seats: " + availableSeats);
                Console.WriteLine("Occupancy: " + occupancy.ToString("F2") + "%");
            }
        }

        public void DisplayExpectedFees()
        {
            decimal expectedFees = 0m;

            for (int i = 0; i < enrollments.Count; i++)
            {
                if (enrollments[i].Status == EnrollmentStatus.Active)
                {
                    expectedFees += enrollments[i].FinalFee;
                }
            }

            Console.WriteLine("\n===== EXPECTED FEES =====");
            Console.WriteLine("Expected fees from active enrollments: ₹" + expectedFees);
        }

        public void DisplayCourseEnrollmentSummary(Course[] courses)
        {
            Console.WriteLine("\n===== COURSE ENROLLMENT SUMMARY =====");

            for (int i = 0; i < courses.Length; i++)
            {
                int activeCount = 0;

                for (int j = 0; j < enrollments.Count; j++)
                {
                    if (enrollments[j].CourseCode == courses[i].CourseCode &&
                        enrollments[j].Status == EnrollmentStatus.Active)
                    {
                        activeCount++;
                    }
                }

                Console.WriteLine("\nCourse: " + courses[i].Title);
                Console.WriteLine("Active Enrollments: " + activeCount);
                Console.WriteLine("Available Seats: " + courses[i].AvailableSeats);

                if (courses[i].AvailableSeats <= 0)
                {
                    Console.WriteLine("Status: No remaining seats");
                }
            }
        }

    }
}
