
namespace TrainingManagementSystem.Models
{
    public class Course
    {
        public string CourseCode { get; set; }
        public string Title { get; set; }
        public decimal Fee { get; set; }
        public int DurationDays { get; set; }
        public int TotalSeats { get; set; }
        public int EnrolledCount { get; set; }

        public int AvailableSeats
        {
            get { return TotalSeats - EnrolledCount; }
        }

        public Course(
            string courseCode,
            string title,
            decimal fee,
            int durationDays,
            int totalSeats)
        {
            CourseCode = courseCode;
            Title = title;
            Fee = fee;
            DurationDays = durationDays;
            TotalSeats = totalSeats;
            EnrolledCount = 0;
        }
    }
}
