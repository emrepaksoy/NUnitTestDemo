using NUnitTestDemo;
using NUnitTestDemo.Models;

namespace NUnitDemo.JobApplicaton.Test
{
    public class ApplicationEvaluateUnitTest
    {
        [Test]
        public void Application_SouldTransferredToAutoRejected_WithUnderAge()
        {
            //Arrange
            var evaluator = new ApplicationEvaluator();
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
            Assert.AreEqual(ApplicationResult.AutoRejected, appResult);

        }



    }
}