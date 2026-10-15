
using System;
using System.Collections.Generic;
using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.Services
{
    public class LearnerManagement
    {
        private Dictionary<string, LearnerRegistration> learners;

        public LearnerManagement(
            Dictionary<string, LearnerRegistration> learnerData)
        {
            learners = learnerData;
        }

        public void DisplayAllLearners()
        {
            if (learners.Count == 0)
            {
                Console.WriteLine("No learners registered yet.");
                return;
            }

            Console.WriteLine("\n----- ALL LEARNERS -----");

            foreach (LearnerRegistration learner in learners.Values)
            {
                DisplayLearner(learner);
            }
        }

        public void SearchLearner()
        {
            Console.Write("Enter Learner ID to search: ");
            string id = Console.ReadLine() ?? "";

            LearnerRegistration learner;

            if (learners.TryGetValue(id, out learner!))
            {
                DisplayLearner(learner);
            }
            else
            {
                Console.WriteLine("Learner not found.");
            }
        }

        public void RemoveLearner()
        {
            Console.Write("Enter Learner ID to remove: ");
            string id = Console.ReadLine() ?? "";

            if (!learners.ContainsKey(id))
            {
                Console.WriteLine("Learner ID not found.");
                return;
            }

            // Keep learner records linked to enrollment history.
            foreach (KeyValuePair<string, LearnerRegistration> item
                     in learners)
            {
                if (item.Key == id)
                {
                    Console.WriteLine(
                        "This learner has a record. " +
                        "Use a separate deactivation feature " +
                        "if enrollment history must be preserved.");
                    return;
                }
            }

            learners.Remove(id);
            Console.WriteLine("Learner removed successfully.");
        }

        private void DisplayLearner(LearnerRegistration learner)
        {
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Learner ID".PadRight(20) + " : " + learner.LearnerId);
            Console.WriteLine("Name".PadRight(20) + " : " + learner.Name);
            Console.WriteLine("Contact Number".PadRight(20) + " : " + learner.ContactNumber);
            Console.WriteLine("Email".PadRight(20) + " : " + learner.Email);
            Console.WriteLine("-----------------------------------------------");
        }
    }
}
