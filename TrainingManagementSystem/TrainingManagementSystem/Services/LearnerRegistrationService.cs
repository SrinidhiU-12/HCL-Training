
using System;
using System.Collections.Generic;
using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.Services
{
    public class LearnerRegistrationService
    {
        private Dictionary<string, LearnerRegistration> learners;

        public LearnerRegistrationService(
            Dictionary<string, LearnerRegistration> learnerData)
        {
            learners = learnerData;
        }

        public void RegisterLearner()
        {
            Console.Write("Enter Learner ID: ");
            string id = Console.ReadLine() ?? "";

            if (IsEmpty(id))
            {
                Console.WriteLine("Learner ID cannot be empty.");
                return;
            }

            if (learners.ContainsKey(id))
            {
                Console.WriteLine("Learner ID already exists.");
                return;
            }

            Console.Write("Enter Learner Name: ");
            string name = Console.ReadLine() ?? "";

            if (IsEmpty(name))
            {
                Console.WriteLine("Learner name cannot be empty.");
                return;
            }

            Console.Write("Enter Contact Number: ");
            string contact = Console.ReadLine() ?? "";

            if (!IsValidContact(contact))
            {
                Console.WriteLine(
                    "Contact number must contain exactly 10 digits.");
                return;
            }

            Console.Write("Enter Email: ");
            string email = Console.ReadLine() ?? "";

            if (IsEmpty(email) || !email.Contains("@"))
            {
                Console.WriteLine("Enter a valid email address.");
                return;
            }

            LearnerRegistration learner =
                new LearnerRegistration(id, name, contact, email);

            learners.Add(id, learner);

            Console.WriteLine("Learner registered successfully!");
        }

        private bool IsEmpty(string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] != ' ')
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsValidContact(string contact)
        {
            if (contact.Length != 10)
            {
                return false;
            }

            for (int i = 0; i < contact.Length; i++)
            {
                if (contact[i] < '0' || contact[i] > '9')
                {
                    return false;
                }
            }

            return true;
        }
    }
}
