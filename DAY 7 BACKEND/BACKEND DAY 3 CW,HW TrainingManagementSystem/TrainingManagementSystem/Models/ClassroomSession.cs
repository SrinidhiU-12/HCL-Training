
namespace TrainingManagementSystem.Models
{
    public class ClassroomSession : CourseSession
    {
        public ClassroomSession(
            string courseCode,
            System.DateTime startDate,
            SessionDuration duration)
            : base(courseCode, startDate, duration)
        {
        }

        public override decimal CalculateSessionCharge()
        {
            return Duration.Days * 500m;
        }

        public override void DisplaySessionDetails()
        {
            base.DisplaySessionDetails();
            System.Console.WriteLine("Session Type: Classroom");
            System.Console.WriteLine(
                "Session Charge: ₹" + CalculateSessionCharge());
        }
    }
}
