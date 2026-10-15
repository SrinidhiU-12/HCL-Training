using System;
using System.Collections.Generic;
using TrainingManagementSystem.Models;
using TrainingManagementSystem.Services;
//using TrainingManagementSystem.Models;
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

    // 2. Shared collections
    static Dictionary<string, LearnerRegistration> learners =
        new Dictionary<string, LearnerRegistration>();

    static List<Enrollment> enrollments =
        new List<Enrollment>();

    // 3. Services (initialize after the collections)
    static LearnerRegistrationService registration =
        new LearnerRegistrationService(learners);

    static LearnerManagement management =
        new LearnerManagement(learners);

    static EnrollmentService enrollmentService =
        new EnrollmentService(learners, courses, enrollments);

    static int nextEnrollmentId = 1;

    public static void Main(string[] args)
    {
        Enrollment enrollment = new Enrollment(
       nextEnrollmentId,
       "L101",
       "C101",
       DateTime.Now,
       5000m,
       500m,
       4500m,
       EnrollmentStatus.Active
   );

        enrollments.Add(enrollment);
        nextEnrollmentId++;

        Console.WriteLine("Enrollment added successfully!");
        Console.WriteLine("Enrollment ID: " + enrollments[0].EnrollmentId);
        Console.WriteLine("Learner ID: " + enrollments[0].LearnerId);
        Console.WriteLine("Course Code: " + enrollments[0].CourseCode);
        Console.WriteLine("Enrollment Date: " + enrollments[0].EnrollmentDate);
        Console.WriteLine("Base Fee: " + enrollments[0].BaseFee);
        Console.WriteLine("Discount: " + enrollments[0].DiscountAmount);
        Console.WriteLine("Final Fee: " + enrollments[0].FinalFee);
        Console.WriteLine("Status: " + enrollments[0].Status);
        Console.WriteLine("Total Enrollments: " + enrollments.Count);


        while (true)
        {
            Console.WriteLine("\n****** TRAINING MANAGEMENT SYSTEM ******");
            Console.WriteLine("1. Display All Courses");
            Console.WriteLine("2. Search Course by Code");
            Console.WriteLine("3. Search Course by Title");
            Console.WriteLine("4. Sort Courses by Fee (Ascending)");
    

            //==================================

            Console.WriteLine("5. Register Learner");
            Console.WriteLine("6. Display All Learners");
            Console.WriteLine("7. Search Learner");
            Console.WriteLine("8. Remove Learner");
            Console.WriteLine("9. Exit");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine()!;

            if (choice == "1")
            {
                DisplayCourses(courses);
            }
            else if (choice == "2")
            {
                Console.Write("Enter course code: ");
                string code = Console.ReadLine()!;

                Course? found = FindByCode(code);

                if (found != null)
                {
                    DisplayCourse(found);
                }
                else
                {
                    Console.WriteLine("Course not found, Try again with correct data");
                }
            }
            else if (choice == "3")
            {
                Console.Write("Enter course title: ");
                string title = Console.ReadLine()!;

                SearchByTitle(title);
            }
            else if (choice == "4")
            {
                SortByFee();
                DisplayCourses(courses);
            }
            else if (choice == "5")
            {
                registration.RegisterLearner();
            }
            else if (choice == "6")
            {
                enrollmentService.EnrollLearner();
            }
            else if (choice == "7")
            {
                management.DisplayAllLearners();
            }
            else if (choice == "8")
            {
                management.SearchLearner();
            }
            else if (choice == "9")
            {
                management.RemoveLearner();
            }
            else if (choice == "10")
            {
                enrollmentService.CancelEnrollment();
            }
            else if (choice == "11")
            {
                Console.WriteLine("Thank you!");
                return;
            }
            
            else
            {
                Console.WriteLine("Invalid choice. Try again.");
            }
            
        }
    }

    static void DisplayCourses(Course[] courseList)
    {
        Console.WriteLine("\n---------------- COURSE CATALOGUE ----------------");
        //Console.WriteLine("Code | Title | Fee | Days | Total Seats | Enrolled | Available");
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