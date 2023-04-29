using NUnitTestDemo.Models;
using NUnitTestDemo.Services;
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
        private IIdentityValidator validator;

        public ApplicationEvaluator(IIdentityValidator identityValidator)
        {
            validator = identityValidator;
            // test edilecek bir class içerisinde bir nesneyi new keyword u ile oluşturuyorsak ve bu bir class ise 
            // bu nesne test edilcek method içerisinde kullanılıyorsa burda bir bağımlılık ortaya çıkar./
            // test edilecek methodun başka bir servise bağımlılığı ortaya çıkmış oluyor , belki burdaki servis arkaplanda
            // 3. bir servise gidiyor olabilir e-devlet gibi -- bu servis yavaş olabilir veya ayakta olmayabilir.
            // diğer bir problem Evaulate() methodu içerisindeki IsValid() methodunun çağırılması bu methodun 
            // çalışıyor olması veya çalışmıyor olması testleri etkilemiyor olmalı.
        }

        public ApplicationResult Evaluate(JobApplication form)
        {
            
            if (form.Applicant.Age < minAge)
                return ApplicationResult.AutoRejected;

            if (validator.CountryProvider.CountryData.Country != "TURKEY")
                return ApplicationResult.TransferredToCTO;

            if (validator.Country != "TURKEY")
                return ApplicationResult.TransferredToCTO;

            if (form.OfficeLocation != "Istanbul")
                return ApplicationResult.TransferredToCTO;

            var connectionSucceded = validator.CheckConnectionToRemoteServer();

            var validIdentity = validator.IsValid(form.Applicant.IdentityNumber);

            if (!validIdentity)
                return ApplicationResult.TransferredToHR;

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
