using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.DataAccess.Entities;
using EducationProcess.Desktop.ViewModels;
using Xunit;

namespace EducationProcess.Desktop.Tests.ViewModels
{
    public class AddGroupToEducationPlanViewModelTests
    {
        [Theory]
        [InlineData("", 2, 4)]
        [InlineData("ИС2-1", 0, 4)]
        [InlineData("ИС11", 2, 12313)]
        [InlineData("АГРО", 5, 2)]
        [InlineData("ЖЕЖЕ", 0, 2019)]
        public void IsValidGroupWithInvalidGroupShouldReturnFalse(string groupName, byte courseNumber, short receiptYear)
        {
            // Arrange
            EducationPlan educationPlan = new EducationProcessContext().EducationPlans.First();
            AddGroupToEducationPlanViewModel viewModel = new AddGroupToEducationPlanViewModel(educationPlan);
            Group group = new Group() { Name = groupName, CourseNumber = courseNumber, ReceiptYear = receiptYear };
            // Act
            bool isValidGroup = viewModel.IsValidGroup(group);
            // Assert
            Assert.False(isValidGroup);
        }

        [Theory]
        [InlineData("АП1-1", 2, 2020)]
        [InlineData("АП5-1", 5, 2001)]
        [InlineData("ИНФО-3", 3, 2022)]
        public void IsValidGroupWithInvalidGroupShouldReturnTrue(string groupName, byte courseNumber, short receiptYear)
        {
            // Arrange
            EducationPlan educationPlan = new EducationProcessContext().EducationPlans.First();
            AddGroupToEducationPlanViewModel viewModel = new AddGroupToEducationPlanViewModel(educationPlan);
            Group group = new Group() { Name = groupName, CourseNumber = courseNumber, ReceiptYear = receiptYear };
            // Act
            bool isValidGroup = viewModel.IsValidGroup(group);
            // Assert
            Assert.True(isValidGroup);
        }

        [Fact]
        public void RelevantGroupShouldBeCorrect()
        {
            // Arrange
            EducationPlan educationPlan = new EducationProcessContext().EducationPlans.First();
            AddGroupToEducationPlanViewModel viewModel = new AddGroupToEducationPlanViewModel(educationPlan);

            // Act
            Group[] groups = viewModel.GetRelevantGroupsByEducationPlan(educationPlan);
            // Assert
            foreach (var group in groups)
            {
                Assert.Equal(educationPlan.SpecialtieId, group.ReceivedEducation.ReceivedSpecialty.SpecialtieId);
                Assert.Equal(null, group.EducationPlanId);
            }
        }

        [Fact]
        public void IsChainedGroupWithChainedGroupShouldReturnTrue()
        {
            // Arrange
            EducationPlan educationPlan = new EducationProcessContext().EducationPlans.First();
            AddGroupToEducationPlanViewModel viewModel = new AddGroupToEducationPlanViewModel(educationPlan);
            Group group = new Group() { EducationPlanId = 12};

            // Act
            bool isChained = viewModel.IsGroupChained(group);
            // Assert
            Assert.True(isChained);
        }

        [Fact]
        public void IsChainedGroupWithotChainedGroupShouldReturnFalse()
        {
            // Arrange
            EducationPlan educationPlan = new EducationProcessContext().EducationPlans.First();
            AddGroupToEducationPlanViewModel viewModel = new AddGroupToEducationPlanViewModel(educationPlan);
            Group group = new Group();

            // Act
            bool isChained = viewModel.IsGroupChained(group);
            // Assert
            Assert.False(isChained);
        }
    }
}