using NUnitTestDemo;
using NUnitTestDemo.Models;
using FluentAssertions;
using Moq;
using NUnitTestDemo.Services;

namespace NUnitDemo.JobApplicaton.Test
{
    public class ApplicationEvaluateUnitTest
    {
        [Test]
        public void Application_SouldTransferredToAutoRejected_WithUnderAge()
        {
            //Arrange
            var evaluator = new ApplicationEvaluator(null);
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 17
                }
            };

            //Action
            var appResult =  evaluator.Evaluate(form);

            //Assert
            //Assert.AreEqual(ApplicationResult.AutoRejected, appResult);

            //FluentAssertion
            appResult.Should().Be(ApplicationResult.AutoRejected);
        }

        [Test]
        public void Application_SouldTransferredToAutoRejected_WithNoTechStack()
        {

            var mockValidator = new Mock<IIdentityValidator>();
            mockValidator.Setup(i => i.IsValid(""));
            //Arrange
            var evaluator = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 19
                },
                TechStackList = new System.Collections.Generic.List<string>() {""}
                
            };

            //Action
            var appResult = evaluator.Evaluate(form);

            //Assert
            Assert.AreEqual(ApplicationResult.AutoRejected, appResult);

        }

        [Test]
        public void Application_SouldTransferredToAutoAccepted_WithTechStackOver75P()
        {
            //Arrange
            var evaluator = new ApplicationEvaluator(null);
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 19
                },
                TechStackList = new System.Collections.Generic.List<string>()
                {
                    "C#", "RabbitMQ", "Microservice", "Visual Studio"
                },
                YearsOfExperience = 15,
             

            };

            //Action
            var appResult = evaluator.Evaluate(form);

            //Assert
            //Assert.AreEqual(ApplicationResult.AutoAccepted, appResult);

            //FluentAssertion
            appResult.Should().Be(ApplicationResult.AutoAccepted);

        }



    }
}