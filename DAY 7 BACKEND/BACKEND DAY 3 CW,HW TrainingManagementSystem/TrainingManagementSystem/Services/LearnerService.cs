
using System;
using System.Collections.Generic;
using TrainingManagementSystem.Models;

namespace TrainingManagementSystem.Services
{
    public class LearnerService
    {
        private readonly LearnerRegistrationService registrationService;
        private readonly LearnerManagement managementService;

        public LearnerService(
            Dictionary<string, LearnerRegistration> learners)
        {
            if (learners == null)
            {
                throw new ArgumentNullException(nameof(learners));
            }

            registrationService = new LearnerRegistrationService(learners);
            managementService = new LearnerManagement(learners);
        }

        public void RegisterLearner()
        {
            registrationService.RegisterLearner();
        }

        public void DisplayAllLearners()
        {
            managementService.DisplayAllLearners();
        }

        public void SearchLearner()
        {
            managementService.SearchLearner();
        }

        public void RemoveLearner()
        {
            managementService.RemoveLearner();
        }
    }
}
