using System;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Threading;
using System.Windows.Controls;
using DevExpress.Mvvm;
using EducationProcess.Desktop.Core;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.DataAccess.Entities;
using EducationProcess.Desktop.Helpers.Identity;
using MahApps.Metro.Controls;
using Microsoft.EntityFrameworkCore;

namespace EducationProcess.Desktop.ViewModels
{
    public class AuthenticationViewModel : BindableBase, IViewModel
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            LoginCommand = new RelayCommand(CanLogin, Login);
            LogoutCommand = new RelayCommand(CanLogout, Logout);
            ShowViewCommand = new RelayCommand(null, ShowView);
        }

        public event EventHandler ClosingRequest;
        protected void CloseView()
        {
            ClosingRequest?.Invoke(this, EventArgs.Empty);
        }

        #region Properties
        public string Username { get; set; }

        public string AuthenticatedUser
        {
            get
            {
                if (IsAuthenticated)
                    return string.Format("Здравствуйте, {0}. {1}",
                          Thread.CurrentPrincipal.Identity.Name,
                          Thread.CurrentPrincipal.IsInRole("Администратор") ? "Ого, ты админ."
                              : "Вы не относитесь к группе администраторов.");
                return "Не авторизован.";
            }
        }

        public string Status { get; set; }
        #endregion

        #region Commands
        public RelayCommand LoginCommand { get; set; }

        public RelayCommand LogoutCommand { get; set; }

        public RelayCommand ShowViewCommand { get; set; }
        #endregion

        private async void Login(object parameter)
        {
            IsAuthenticating = true;
            
            PasswordBox passwordBox = parameter as PasswordBox;
            string clearTextPassword = passwordBox.Password;
            try
            {
                //Validate credentials through the authentication service
                Employee employee = await _authenticationService.AuthenticateUser(Username, clearTextPassword);

                //Get the current principal object
                CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
                if (customPrincipal == null)
                    throw new ArgumentException(
                        "The application's default thread principal must be set to a CustomPrincipal object on startup.");

                Post post =
                    await new EducationProcessContext().Posts.FirstOrDefaultAsync(x => x.PostId == employee.PostId);
                //Authenticate the user
                customPrincipal.Identity =
                    new CustomIdentity(employee.Firstname, "no", new[] { post.Name }, employee.EmployeeId);
                IsAuthenticated = Thread.CurrentPrincipal.Identity.IsAuthenticated;
                //Update UI
                Username = string.Empty; //reset
                passwordBox.Password = string.Empty; //reset
                Status = string.Empty;

                ShowView("Gay");
            }
            catch (UnauthorizedAccessException)
            {
                Status = "Неверные логин или пароль. Попробуйте ещё раз.";
            }
            catch (Exception ex)
            {
                Status = string.Format("ERROR: {0}", ex.Message);
            }
            IsAuthenticating = false;
        }

        private bool CanLogin(object parameter)
        {
            return !IsAuthenticated;
        }

        public bool ValidateLogin(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
                return false;
            if (login.Length <= 3)
                return false;
            return true;
        }

        public bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;
            if (password.Length <= 3)
                return false;
            if (password.Any(char.IsDigit) == false)
                return false;
            if (password.Any(char.IsUpper) == false)
                return false;
            return true;
        }

        private void Logout(object parameter)
        {
            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            if (customPrincipal != null)
            {
                customPrincipal.Identity = new AnonymousIdentity();
                Status = string.Empty;
                IsAuthenticated = customPrincipal.Identity.IsAuthenticated;
            }
        }

        private bool CanLogout(object parameter)
        {
            return IsAuthenticated;
        }

        public bool IsAuthenticating { get; set; }

        public bool IsAuthenticated { get; set; }

        private void ShowView(object parameter)
        {
            try
            {
                Status = string.Empty;
                IView view = null;
                if (parameter?.ToString() == "Gay")
                    view = new MainWindow();

                //Authenticate the user
                if (Thread.CurrentPrincipal == null)
                    throw new Exception("wataf..");
                //if (Thread.CurrentPrincipal.IsInRole("Администратор") == false)
                //    throw new SecurityException();

                this.CloseView();
                view?.Show();
            }
            catch (SecurityException)
            {
                Status = "Вы не авторизованы";
            }
        }
    }
}
