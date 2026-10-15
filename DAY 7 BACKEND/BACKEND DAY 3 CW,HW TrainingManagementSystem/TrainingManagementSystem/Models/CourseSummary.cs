

namespace TrainingManagementSystem.Models
{
    public record CourseSummary(
        string CourseCode,
        string Title,
        decimal Fee,
        int AvailableSeats
    );
}

