
namespace TrainingManagementSystem.Models
{
    public class LearnerRegistration
    {
        public string LearnerId { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }

        public LearnerRegistration(
            string learnerId,
            string name,
            string contactNumber,
            string email)
        {
            LearnerId = learnerId;
            Name = name;
            ContactNumber = contactNumber;
            Email = email;
        }
    }
}
