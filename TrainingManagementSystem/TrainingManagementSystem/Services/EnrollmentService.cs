
using System;
using System.Collections.Generic;
using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.Services
{
    public class EnrollmentService
    {
        private Dictionary<string, LearnerRegistration> learners;
        private Course[] courses;
        private List<Enrollment> enrollments;
        private FeeCalculator feeCalculator = new FeeCalculator();

        private int nextEnrollmentId = 1;
    
public EnrollmentService(
    Dictionary<string, LearnerRegistration> learnerData,
    Course[] courseData,
    List<Enrollment> enrollmentData)
        {
            if (learnerData == null)
            {
                throw new ArgumentNullException(nameof(learnerData));
            }
            if (courseData == null)
            {
                throw new ArgumentNullException(nameof(courseData));
            }

            if (enrollmentData == null)
            {
                throw new ArgumentNullException(nameof(enrollmentData));
            }

            learners = learnerData;
            courses = courseData;
            enrollments = enrollmentData;

            nextEnrollmentId = 1;

            for (int i = 0; i < enrollments.Count; i++)
            {
                Enrollment currentEnrollment = enrollments[i];

                if (currentEnrollment == null)
                {
                    continue;
                }

                if (currentEnrollment.EnrollmentId >= nextEnrollmentId)
                {
                    nextEnrollmentId = currentEnrollment.EnrollmentId + 1;
                }
            }
        }


        public void EnrollLearner()
        {
            Console.Write("Enter Learner ID: ");
            string learnerId = Console.ReadLine() ?? "";

            if (!learners.ContainsKey(learnerId))
            {
                Console.WriteLine("Learner not found.");
                return;
            }

            Console.Write("Enter Course Code: ");
            string courseCode = Console.ReadLine() ?? "";

            Course selectedCourse = null;

            for (int i = 0; i < courses.Length; i++)
            {
                if (courses[i].CourseCode == courseCode)
                {
                    selectedCourse = courses[i];
                    break;
                }
            }

            if (selectedCourse == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            for (int i = 0; i < enrollments.Count; i++)
            {
                if (enrollments[i].LearnerId == learnerId &&
                    enrollments[i].CourseCode == courseCode &&
                    enrollments[i].Status == EnrollmentStatus.Active)
                {
                    Console.WriteLine(
                        "Learner is already actively enrolled.");
                    return;
                }
            }

            if (selectedCourse.EnrolledCount >= selectedCourse.TotalSeats)
            {
                Console.WriteLine("No seats available.");
                return;
            }

            Console.Write("Days before course start: ");
            string daysInput = Console.ReadLine() ?? "";
            int daysBeforeStart;

            if (!int.TryParse(daysInput, out daysBeforeStart) ||
                daysBeforeStart < 0)
            {
                Console.WriteLine("Invalid number of days.");
                return;
            }

            Console.Write("Eligible learners enrolling together: ");
            string groupInput = Console.ReadLine() ?? "";
            int groupSize;

            if (!int.TryParse(groupInput, out groupSize) || groupSize < 1)
            {
                Console.WriteLine("Invalid group size.");
                return;
            }

            Console.Write("Delivery mode (1 = Online, 2 = Classroom): ");
            string mode = Console.ReadLine() ?? "";

            if (mode != "1" && mode != "2")
            {
                Console.WriteLine("Invalid delivery mode.");
                return;
            }

            bool isOnline = mode == "1";
            decimal baseFee = selectedCourse.Fee;
            decimal earlyBirdDiscount = 0m;
            decimal groupDiscount = 0m;

            if (daysBeforeStart >= 14)
            {
                earlyBirdDiscount = baseFee * 0.10m;
            }

            if (groupSize >= 3)
            {
                groupDiscount = baseFee * 0.05m;
            }

            (decimal BaseFee, decimal DiscountAmount, decimal FinalFee)
                feeSummary;

            try
            {
                feeSummary = feeCalculator.CalculateFee(
                    baseFee,
                    earlyBirdDiscount,
                    groupDiscount,
                    isOnline);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Fee error: " + ex.Message);
                return;
            }

            // Create enrollment only after all checks succeed.
            Enrollment enrollment = new Enrollment(
                nextEnrollmentId,
                learnerId,
                courseCode,
                DateTime.Now,
                feeSummary.BaseFee,
                feeSummary.DiscountAmount,
                feeSummary.FinalFee,
                EnrollmentStatus.Active);

            enrollments.Add(enrollment);

            // ref example: update a local count by reference.
            int updatedCount = selectedCourse.EnrolledCount;
            IncreaseCount(ref updatedCount);
            selectedCourse.EnrolledCount = updatedCount;

            nextEnrollmentId++;

            FeeSummary summary = new FeeSummary(
                feeSummary.BaseFee,
                feeSummary.DiscountAmount,
                feeSummary.FinalFee);

            DisplayFeeSummary(in summary);

            Console.WriteLine("\nEnrollment successful!");
            Console.WriteLine("Enrollment ID : " + enrollment.EnrollmentId);
            Console.WriteLine("Learner ID    : " + learnerId);
            Console.WriteLine("Course Code   : " + courseCode);
            Console.WriteLine("Date          : " + enrollment.EnrollmentDate);
            Console.WriteLine("Status        : " + enrollment.Status);
            Console.WriteLine("Early-Bird Discount: ₹" + earlyBirdDiscount);
            Console.WriteLine("Group Discount    : ₹" + groupDiscount);
            Console.WriteLine("Surcharge         : ₹" + (isOnline ? 200m : 800m));
            Console.WriteLine("Seats Left        : " + selectedCourse.AvailableSeats);
        }

        private void IncreaseCount(ref int count)
        {
            count++;
        }

        private void DisplayFeeSummary(in FeeSummary summary)
        {
            Console.WriteLine("\n----- FEE BREAKDOWN -----");
            Console.WriteLine("Base Fee     : ₹" + summary.BaseFee);
            Console.WriteLine("Total Discount: ₹" + summary.DiscountAmount);
            Console.WriteLine("Final Fee    : ₹" + summary.FinalFee);
            Console.WriteLine("-------------------------");
        }

        public void CancelEnrollment()
        {
            Console.Write("Enter Enrollment ID to cancel: ");
            string input = Console.ReadLine() ?? "";
            int enrollmentId;

            // out example: parse input into an integer.
            if (!int.TryParse(input, out enrollmentId))
            {
                Console.WriteLine("Invalid enrollment ID.");
                return;
            }

            Enrollment foundEnrollment = null;

            for (int i = 0; i < enrollments.Count; i++)
            {
                if (enrollments[i].EnrollmentId == enrollmentId)
                {
                    foundEnrollment = enrollments[i];
                    break;
                }
            }

            if (foundEnrollment == null)
            {
                Console.WriteLine("Enrollment not found.");
                return;
            }

            if (foundEnrollment.Status == EnrollmentStatus.Cancelled)
            {
                Console.WriteLine("Enrollment is already cancelled.");
                return;
            }

            if (foundEnrollment.Status == EnrollmentStatus.Completed)
            {
                Console.WriteLine("Completed enrollments cannot be cancelled.");
                return;
            }

            Course selectedCourse = null;

            for (int i = 0; i < courses.Length; i++)
            {
                if (courses[i].CourseCode == foundEnrollment.CourseCode)
                {
                    selectedCourse = courses[i];
                    break;
                }
            }

            if (selectedCourse == null)
            {
                Console.WriteLine("Associated course not found.");
                return;
            }

            foundEnrollment.Status = EnrollmentStatus.Cancelled;

            if (selectedCourse.EnrolledCount > 0)
            {
                selectedCourse.EnrolledCount--;
            }

            Console.WriteLine("\nEnrollment cancelled successfully!");
            Console.WriteLine("Enrollment ID : " + foundEnrollment.EnrollmentId);
            Console.WriteLine("Status        : " + foundEnrollment.Status);
            Console.WriteLine("Seats Left    : " + selectedCourse.AvailableSeats);
            Console.WriteLine("Record retained for history.");
        }

        public void DisplayAllEnrollments()
        {
            if (enrollments.Count == 0)
            {
                Console.WriteLine("No enrollment records.");
                return;
            }

            for (int i = 0; i < enrollments.Count; i++)
            {
                Enrollment e = enrollments[i];

                Console.WriteLine("--------------------------------");
                Console.WriteLine("Enrollment ID : " + e.EnrollmentId);
                Console.WriteLine("Learner ID    : " + e.LearnerId);
                Console.WriteLine("Course Code   : " + e.CourseCode);
                Console.WriteLine("Date          : " + e.EnrollmentDate);
                Console.WriteLine("Base Fee      : ₹" + e.BaseFee);
                Console.WriteLine("Discount      : ₹" + e.DiscountAmount);
                Console.WriteLine("Final Fee     : ₹" + e.FinalFee);
                Console.WriteLine("Status        : " + e.Status);
            }
        }
    }
}
