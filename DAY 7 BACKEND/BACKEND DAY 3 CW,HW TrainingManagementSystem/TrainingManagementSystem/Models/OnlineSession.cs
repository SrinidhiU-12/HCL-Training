
namespace TrainingManagementSystem.Models
{
    public class OnlineSession : CourseSession
    {
        public OnlineSession(
            string courseCode,
            System.DateTime startDate,
            SessionDuration duration)
            : base(courseCode, startDate, duration)
        {
        }

        public override decimal CalculateSessionCharge()
        {
            return Duration.Days * 200m;
        }

        public override void DisplaySessionDetails()
        {
            base.DisplaySessionDetails();
            System.Console.WriteLine("Session Type: Online");
            System.Console.WriteLine(
                "Session Charge: ₹" + CalculateSessionCharge());
        }
    }
}
