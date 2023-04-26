using NUnitTestDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestDemo
{
    public class ApplicationEvaluator
    {
        private const int minAge = 18;
        private const int autoAcceptedYearsOfExperience = 10;
        private List<string> techStackList = new() {"C#",  "RabbitMQ","Microservice", "Visual Studio"};
        public ApplicationResult Evaluate(JobApplication form)
        {
            
            if (form.Applicant.Age < minAge)
                return ApplicationResult.AutoRejected;

            var simRate = GetTechStackSimilarityRate(form.TechStackList);
            if(simRate < 25)
                return ApplicationResult.AutoRejected;

            if (simRate > 75 && form.YearsOfExperience > autoAcceptedYearsOfExperience)
                return ApplicationResult.AutoAccepted;

           
            return ApplicationResult.AutoAccepted;
        }

        private double GetTechStackSimilarityRate(List<string> techList)
        {
            var matchCount = techList
                .Where(t => techStackList.Contains(t, StringComparer.OrdinalIgnoreCase))
                .Count();
            return (double)matchCount / techStackList.Count * 100;
        }
        
    }


    public enum ApplicationResult
    {
        AutoRejected,
        TransferredToHR,
        TransferredToLead,
        TransferredToCTO,
        AutoAccepted
    }
}
