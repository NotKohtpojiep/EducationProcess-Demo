using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.DataAccess.Entities;
using EducationProcess.Desktop.Helpers.Identity;
using EducationProcess.Desktop.ViewModels;
using Xunit;

namespace EducationProcess.Desktop.Tests.ViewModels
{
    public class AuthenticationViewModelTests
    {
        [Theory]
        [InlineData("               ")]
        [InlineData("12")]
        [InlineData("")]
        [InlineData(null)]
        public void LoginValidationWithIncorrectLoginShouldReturnFalse(string login)
        {
            // Arrange
            AuthenticationViewModel viewModel = new AuthenticationViewModel(new AuthenticationService());

            // Act
            bool isCorrectLogin = viewModel.ValidateLogin(login);

            // Assert
            Assert.False(isCorrectLogin);
        }

        [Theory]
        [InlineData("Lo and foo")]
        [InlineData("Lofin")]
        [InlineData("anything")]
        public void LoginValidationWithIncorrectLoginShouldReturnTrue(string login)
        {
            // Arrange
            AuthenticationViewModel viewModel = new AuthenticationViewModel(new AuthenticationService());

            // Act
            bool isCorrectLogin = viewModel.ValidateLogin(login);

            // Assert
            Assert.True(isCorrectLogin);
        }

        [Theory]
        [InlineData("Lo and foo")]
        [InlineData("Lofin")]
        [InlineData("anything")]
        [InlineData("               ")]
        [InlineData("12")]
        [InlineData("1212312312313123")]
        [InlineData("")]
        [InlineData(null)]
        public void PasswordValidationWithIncorrectLoginShouldReturnFalse(string password)
        {
            // Arrange
            AuthenticationViewModel viewModel = new AuthenticationViewModel(new AuthenticationService());

            // Act
            bool isCorrectPassword = viewModel.ValidatePassword(password);

            // Assert
            Assert.False(isCorrectPassword);
        }

        [Theory]
        [InlineData("ANOTHER1")]
        [InlineData("Lofin123")]
        public void PasswordValidationWithIncorrectLoginShouldReturnTrue(string password)
        {
            // Arrange
            AuthenticationViewModel viewModel = new AuthenticationViewModel(new AuthenticationService());

            // Act
            bool isCorrectPassword = viewModel.ValidatePassword(password);

            // Assert
            Assert.True(isCorrectPassword);
        }
    }
}
