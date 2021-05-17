using System;
using System.ComponentModel;
using System.Security;
using System.Threading;
using System.Windows.Controls;
using DevExpress.Mvvm;
using EducationProcess.Desktop.Core;
using EducationProcess.Desktop.Helpers.Identity;
using MahApps.Metro.Controls;

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
                    return string.Format("Signed in as {0}. {1}",
                          Thread.CurrentPrincipal.Identity.Name,
                          Thread.CurrentPrincipal.IsInRole("Administrators") ? "You are an administrator!"
                              : "You are NOT a member of the administrators group.");

                return "Not authenticated!";
            }
        }

        public string Status { get; set; }
        #endregion

        #region Commands
        public RelayCommand LoginCommand { get; set; }

        public RelayCommand LogoutCommand { get; set; }

        public RelayCommand ShowViewCommand { get; set; }
        #endregion

        private void Login(object parameter)
        {
            PasswordBox passwordBox = parameter as PasswordBox;
            string clearTextPassword = passwordBox.Password;
            try
            {
                //Validate credentials through the authentication service
                User user = _authenticationService.AuthenticateUser(Username, clearTextPassword);

                //Get the current principal object
                CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
                if (customPrincipal == null)
                    throw new ArgumentException("The application's default thread principal must be set to a CustomPrincipal object on startup.");
                //Authenticate the user
                customPrincipal.Identity = new CustomIdentity(user.Username, user.Email, user.Roles);
                IsAuthenticated = Thread.CurrentPrincipal.Identity.IsAuthenticated;
                //Update UI
                Username = string.Empty; //reset
                passwordBox.Password = string.Empty; //reset
                Status = string.Empty;
            }
            catch (UnauthorizedAccessException)
            {
                Status = "Login failed! Please provide some valid credentials.";
            }
            catch (Exception ex)
            {
                Status = string.Format("ERROR: {0}", ex.Message);
            }
        }

        private bool CanLogin(object parameter)
        {
            return !IsAuthenticated;
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
                if (Thread.CurrentPrincipal.IsInRole("Administrators") == false)
                    throw new SecurityException();

                view?.Show();
                this.CloseView();
            }
            catch (SecurityException)
            {
                Status = "You are not authorized!";
            }
        }
    }
}
