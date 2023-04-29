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
           mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(true);
            
            //Arrange
            var evaluator = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 19,
                    IdentityNumber = "123"
                },
                TechStackList = new System.Collections.Generic.List<string>() {""}
                
            };

            //Action
            var appResult = evaluator.Evaluate(form);

            //Assert
            //Assert.AreEqual(ApplicationResult.AutoRejected, appResult);

            //FluentAssertion
            appResult.Should().Be(ApplicationResult.AutoRejected);


        }

        [Test]
        public void Application_SouldTransferredToAutoAccepted_WithTechStackOver75P()
        {

            var mockValidator = new Mock<IIdentityValidator>();
            mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(true);

            //Arrange
            var evaluator = new ApplicationEvaluator(mockValidator.Object);
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

        [Test]
        public void Application_SouldTransferredToHR_WithInvalidIdentityNumber()
        {

            var mockValidator = new Mock<IIdentityValidator>(MockBehavior.Loose);
            mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(false);

            //Arrange
            var evaluator = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 19
                },
                
            };

            //Action
            var appResult = evaluator.Evaluate(form);

            //Assert
            //Assert.AreEqual(ApplicationResult.TransferredToHR, appResult);

            //FluentAssertion
            appResult.Should().Be(ApplicationResult.TransferredToHR);

        }

        [Test]
        public void Application_SouldTransferredToCTO_WithOfficeLocation()
        {

            var mockValidator = new Mock<IIdentityValidator>();

            //Arrange
            var evaluator = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 19,
                },
                OfficeLocation = "Ankara"

            };

            //Action
            var appResult = evaluator.Evaluate(form);

            //Assert
            //Assert.AreEqual(ApplicationResult.TransferredToHR, appResult);

            //FluentAssertion
            appResult.Should().Be(ApplicationResult.TransferredToCTO);

        }

        [Test]
        public void Application_SouldTransferredToCTO_WithCountry()
        {

            var mockValidator = new Mock<IIdentityValidator>();
            mockValidator.Setup(i => i.Country).Returns("SPAIN");
            //Arrange
            var evaluator = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 19,
                },
            };

            //Action
            var appResult = evaluator.Evaluate(form);

            //Assert
            //Assert.AreEqual(ApplicationResult.TransferredToHR, appResult);

            //FluentAssertion
            appResult.Should().Be(ApplicationResult.TransferredToCTO);

        }

        [Test]
        public void Application_SouldTransferredToCTO_WithHierarchicalTypeCountry()
        {

            var mockValidator = new Mock<IIdentityValidator>();

            mockValidator.Setup(i => i.CountryProvider.CountryData.Country).Returns("SPAIN");
            //mockValidator.Setup(i => i.Country).Returns("SPAIN");


            //var mockCountry= new Mock<ICountry>();
            //mockCountry.Setup(i => i.Country).Returns("SPAIN");

            //var mockCountryProvider = new Mock<ICountryProvider>();
            //mockCountryProvider.Setup(i => i.CountryData).Returns(mockCountry.Object);

            //mockValidator.Setup(i => i.CountryProvider).Returns(mockCountryProvider.Object);    
            //Arrange
            var evaluator = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 19,
                },
            };

            //Action
            var appResult = evaluator.Evaluate(form);

            //Assert
            //Assert.AreEqual(ApplicationResult.TransferredToHR, appResult);

            //FluentAssertion
            appResult.Should().Be(ApplicationResult.TransferredToCTO);

        }
    }
}