using System;
using System.Collections.Generic;
using TrainingManagementSystem.Models;
using TrainingManagementSystem.Services;
using TrainingManagementSystem.Payments;
public class Program
{
    static Course[] courses =
{
    new Course("C101", "CSharp Basics", 5000m, 30, 20),
    new Course("C102", "Java Programming", 6000m, 45, 25),
    new Course("C103", "Web Development", 4500m, 30, 20),
    new Course("C104", "Python Programming", 5500m, 40, 25),
    new Course("C105", "Database Management", 4000m, 25, 15)
};
    static Dictionary<string, LearnerRegistration> learners = new Dictionary<string, LearnerRegistration>();

    static List<Enrollment> enrollments = new List<Enrollment>();

    static TrainingService trainingService = new TrainingService(courses);

    static LearnerService learnerService = new LearnerService(learners);

    static EnrollmentService enrollmentService = new EnrollmentService(learners, courses, enrollments);

    static List<Payment> payments = new List<Payment>();

    static PaymentService paymentService = new PaymentService(payments);

    static ReportService reportService = new ReportService(enrollments, payments);

    static int nextEnrollmentId = 1;

    public static void Main(string[] args)
    {
    
        while (true)
        {
            Console.WriteLine("\n====== TRAINING MANAGEMENT SYSTEM ======");
            Console.WriteLine("1.  Display All Courses");
            Console.WriteLine("2.  Search Course by Code or Title");
            Console.WriteLine("3.  Sort Courses by Fee (Ascending)");
            Console.WriteLine("4.  Register Learner");
            Console.WriteLine("5.  Display All Learners");
            Console.WriteLine("6.  Search Learner");
            Console.WriteLine("7.  Enroll Learner");
            Console.WriteLine("8.  Display All Enrollments");
            Console.WriteLine("9.  Cancel Enrollment");
            Console.WriteLine("10. Display Fee Breakdown");
            Console.WriteLine("11. Process Enrollment Payment");
            Console.WriteLine("12. Display Payment History");
            Console.WriteLine("13. Display Enrollment and Payment Reports");
            Console.WriteLine("14. Exit");
            Console.WriteLine("========================================");

            Console.Write("Enter your choice (1-14): ");
            string choice = Console.ReadLine() ?? "";

            if (!int.TryParse(choice, out int menuChoice))
            {
                Console.WriteLine("Please enter a valid number.");
                continue;
            }

            switch (menuChoice)
            {
                case 1:
                    DisplayCourses(courses);
                    break;

                case 2:
                    Console.WriteLine("1. Search by Course Code");
                    Console.WriteLine("2. Search by Course Title");
                    Console.Write("Enter search option: ");
                    string searchOption = Console.ReadLine() ?? "";

                    if (searchOption == "1")
                    {
                        Console.Write("Enter course code: ");
                        string code = Console.ReadLine() ?? "";
                        Course? foundCourse = FindByCode(code);

                        if (foundCourse != null)
                            DisplayCourse(foundCourse);
                        else
                            Console.WriteLine("Course not found.");
                    }
                    else if (searchOption == "2")
                    {
                        Console.Write("Enter course title: ");
                        string title = Console.ReadLine() ?? "";
                        SearchByTitle(title);
                    }
                    else
                    {
                        Console.WriteLine("Invalid search option.");
                    }
                    break;

                case 3:
                    SortByFee();
                    DisplayCourses(courses);
                    break;

                case 4:
                    learnerService.RegisterLearner();
                    break;

                case 5:
                    learnerService.DisplayAllLearners();
                    break;

                case 6:
                    learnerService.SearchLearner();
                    break;

                case 7:
                    enrollmentService.EnrollLearner();
                    break;

                case 8:
                    enrollmentService.DisplayAllEnrollments();
                    break;

                case 9:
                    enrollmentService.CancelEnrollment();
                    break;

                case 10:
                    DisplayFeeBreakdown();
                    break;

                case 11:
                    ProcessPayment();
                    break;

                case 12:
                    paymentService.DisplayPaymentHistory();
                    break;

                case 13:
                    reportService.DisplayEnrollmentReport();
                    reportService.DisplayPaymentReport();
                    break;

                case 14:
                    Console.WriteLine("Thank you for using the Training Management System!");
                    return;

                default:
                    Console.WriteLine("Invalid menu choice. Please select 1 to 14.");
                    break;
            }


        }
    }

    static void RunSessionTest()
    {
        Console.WriteLine("\n===== SESSION TEST =====");

        CourseSummary summary1 = new CourseSummary(
            courses[0].CourseCode,
            courses[0].Title,
            courses[0].Fee,
            courses[0].AvailableSeats);

        CourseSummary summary2 = new CourseSummary(
            courses[0].CourseCode,
            courses[0].Title,
            courses[0].Fee,
            courses[0].AvailableSeats);

        Console.WriteLine("Are summaries equal? " + (summary1 == summary2));

        CourseSummary modifiedSummary = summary1 with { Fee = 6000m };
        Console.WriteLine("Original fee: " + summary1.Fee);
        Console.WriteLine("Modified fee: " + modifiedSummary.Fee);

        SessionDuration duration1 = new SessionDuration(5);
        SessionDuration duration2 = duration1;
        duration2 = new SessionDuration(10);

        Console.WriteLine("Original duration: " + duration1);
        Console.WriteLine("Modified duration: " + duration2);

        CourseSession onlineSession = new OnlineSession(
            "C101", DateTime.Today, new SessionDuration(5));
        CourseSession classroomSession = new ClassroomSession(
            "C102", DateTime.Today, new SessionDuration(5));

        onlineSession.DisplaySessionDetails();
        Console.WriteLine();
        classroomSession.DisplaySessionDetails();
        Console.WriteLine("========================\n");
    }

    static void DisplayCourses(Course[] courseList)
    {
        Console.WriteLine("\n---------------- COURSE CATALOGUE ----------------");
        Console.WriteLine(
    "Code".PadRight(12) + " | " +
    "Title".PadRight(30) + " | " +
    "Fee".PadRight(10) + " | " +
    "Days".PadRight(8) + " | " +
    "Total Seats".PadRight(8) + " | " +
    "Enrolled".PadRight(10) + " | " +
    "Available".PadRight(10)
);

        foreach (Course course in courseList)
        {
            DisplayCourse(course);
        }
    }

    static void DisplayCourse(Course course)
    {
        Console.WriteLine(
            course.CourseCode.PadRight(12) + " | " +
            course.Title.PadRight(30) + " | " +
            course.Fee.ToString().PadRight(10) + " | " +
            course.DurationDays.ToString().PadRight(8) + " | " +
            course.TotalSeats.ToString().PadRight(8) + " | " +
            course.EnrolledCount.ToString().PadRight(10) + " | " +
            course.AvailableSeats.ToString().PadRight(10)
        );

    }

    static void DisplayFeeBreakdown()
    {
        Console.Write("Enter Enrollment ID: ");
        string input = Console.ReadLine() ?? "";

        if (!int.TryParse(input, out int id))
        {
            Console.WriteLine("Invalid Enrollment ID.");
            return;
        }

        for (int i = 0; i < enrollments.Count; i++)
        {
            Enrollment e = enrollments[i];

            if (e.EnrollmentId == id)
            {
                Console.WriteLine("\n===== FEE BREAKDOWN =====");
                Console.WriteLine("Enrollment ID: " + e.EnrollmentId);
                Console.WriteLine("Course Code: " + e.CourseCode);
                Console.WriteLine("Base Fee: ₹" + e.BaseFee);
                Console.WriteLine("Discount: ₹" + e.DiscountAmount);
                Console.WriteLine("Final Fee: ₹" + e.FinalFee);
                Console.WriteLine("Status: " + e.Status);
                return;
            }
        }

        Console.WriteLine("Enrollment not found.");
    }

    static void ProcessPayment()
    {
        Console.Write("Enter Enrollment ID: ");
        string input = Console.ReadLine() ?? "";

        if (!int.TryParse(input, out int id))
        {
            Console.WriteLine("Invalid Enrollment ID.");
            return;
        }

        Enrollment? found = null;

        for (int i = 0; i < enrollments.Count; i++)
        {
            if (enrollments[i].EnrollmentId == id)
            {
                found = enrollments[i];
                break;
            }
        }

        if (found == null)
        {
            Console.WriteLine("Enrollment not found.");
            return;
        }

        Console.WriteLine("1. Online Payment");
        Console.WriteLine("2. Classroom Payment");
        Console.Write("Select payment method: ");

        string method = Console.ReadLine() ?? "";

        IPaymentProcessor processor;

        switch (method)
        {
            case "1":
                processor = new OnlinePaymentProcessor();
                break;

            case "2":
                processor = new ClassroomPaymentProcessor();
                break;

            default:
                Console.WriteLine("Invalid payment method.");
                return;
        }

        paymentService.ProcessPayment(found, processor);
    }

    static void DisplayCourseReports()
    {
        reportService.DisplayCourseOccupancy(courses);
        reportService.DisplayExpectedFees();
        reportService.DisplayCourseEnrollmentSummary(courses);
    }


    static Course? FindByCode(string code)
    {
        for (int i = 0; i < code.Length; i++)
        {
            if (code[i] >= 'a' && code[i] <= 'z')
            {
                code = ChangeToUpperCase(code);
                break;
            }
        }

        for (int i = 0; i < courses.Length; i++)
        {
            string storedCode = courses[i].CourseCode;
            bool same = true;

            if (code.Length != storedCode.Length)
            {
                same = false;
            }
            else
            {
                for (int j = 0; j < code.Length; j++)
                {
                    char entered = code[j];

                    if (entered >= 'a' && entered <= 'z')
                    {
                        entered = (char)(entered - 32);
                    }

                    if (entered != storedCode[j])
                    {
                        same = false;
                        break;
                    }
                }
            }

            if (same)
            {
                return courses[i];
            }
        }

        return null;
    }

    static void SearchByTitle(string title)
    {
        bool found = false;

        for (int i = 0; i < courses.Length; i++)
        {
            string storedTitle = courses[i].Title;

            for (int j = 0; j <= storedTitle.Length - title.Length; j++)
            {
                bool match = true;

                for (int k = 0; k < title.Length; k++)
                {
                    char entered = title[k];
                    char stored = storedTitle[j + k];

                    if (entered >= 'a' && entered <= 'z')
                    {
                        entered = (char)(entered - 32);
                    }

                    if (stored >= 'a' && stored <= 'z')
                    {
                        stored = (char)(stored - 32);
                    }

                    if (entered != stored)
                    {
                        match = false;
                        break;
                    }
                }

                if (match)
                {
                    DisplayCourse(courses[i]);
                    found = true;
                    break;
                }
            }
        }

        if (found == false)
        {
            Console.WriteLine("No matching courses found.");
        }
    }

    static string ChangeToUpperCase(string value)
    {
        string result = "";

        for (int i = 0; i < value.Length; i++)
        {
            char ch = value[i];

            if (ch >= 'a' && ch <= 'z')
            {
                ch = (char)(ch - 32);
            }

            result = result + ch;
        }

        return result;
    }

    static void SortByFee()
    {
        for (int i = 0; i < courses.Length - 1; i++)
        {
            for (int j = 0; j < courses.Length - 1 - i; j++)
            {
                if (courses[j].Fee > courses[j + 1].Fee)
                {
                    Course temp = courses[j];
                    courses[j] = courses[j + 1];
                    courses[j + 1] = temp;
                }
            }
        }

        Console.WriteLine("Courses sorted by fee in ascending order.");
    }
}