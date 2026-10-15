
using System;
using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.Services
{
    public class TrainingService
    {
        private readonly Course[] courses;

        public TrainingService(Course[] courseData)
        {
            if (courseData == null)
            {
                throw new ArgumentNullException(nameof(courseData));
            }

            courses = courseData;
        }

        public void DisplayAllCourses()
        {
            Console.WriteLine("\n===== AVAILABLE COURSES =====");

            for (int i = 0; i < courses.Length; i++)
            {
                Console.WriteLine("------------------------------");
                Console.WriteLine("Course Code: " + courses[i].CourseCode);
                Console.WriteLine("Title: " + courses[i].Title);
                Console.WriteLine("Fee: ₹" + courses[i].Fee);
                Console.WriteLine("Available Seats: " + courses[i].AvailableSeats);
            }
        }

        public void SearchCourseByCode(string courseCode)
        {
            for (int i = 0; i < courses.Length; i++)
            {
                if (string.Equals(
                    courses[i].CourseCode,
                    courseCode,
                    StringComparison.OrdinalIgnoreCase))
                {
                    DisplayCourse(courses[i]);
                    return;
                }
            }

            Console.WriteLine("Course not found.");
        }

        public void SearchCourseByTitle(string title)
        {
            for (int i = 0; i < courses.Length; i++)
            {
                if (courses[i].Title.IndexOf(
                    title,
                    StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    DisplayCourse(courses[i]);
                    return;
                }
            }

            Console.WriteLine("Course not found.");
        }

        public void SortCoursesByFee()
        {
            for (int i = 0; i < courses.Length - 1; i++)
            {
                for (int j = 0; j < courses.Length - i - 1; j++)
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

        private void DisplayCourse(Course course)
        {
            Console.WriteLine("Course Code: " + course.CourseCode);
            Console.WriteLine("Title: " + course.Title);
            Console.WriteLine("Fee: ₹" + course.Fee);
            Console.WriteLine("Available Seats: " + course.AvailableSeats);
        }
    }
}
