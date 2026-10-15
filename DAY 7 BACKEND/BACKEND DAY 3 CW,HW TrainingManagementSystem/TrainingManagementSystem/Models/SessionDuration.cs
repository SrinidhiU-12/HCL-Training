
namespace TrainingManagementSystem.Models
{
    public struct SessionDuration
    {
        public int Days { get; set; }

        public SessionDuration(int days)
        {
            if (days <= 0)
            {
                throw new System.ArgumentException(
                    "Duration must be greater than zero.");
            }

            Days = days;
        }

        public override string ToString()
        {
            return Days + " days";
        }
    }
}
