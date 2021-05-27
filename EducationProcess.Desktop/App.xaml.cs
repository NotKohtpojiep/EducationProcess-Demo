using System;
using System.Windows;
using EducationProcess.Desktop.Core;
using EducationProcess.Desktop.Helpers.Identity;
using EducationProcess.Desktop.ViewModels;
using EducationProcess.Desktop.Windows;

namespace EducationProcess.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            /*
            EducationPlan educationPlan = new EducationProcessContext().EducationPlans.First();
            SemesterDiscipline semesterDiscipline = new EducationProcessContext().SemesterDisciplines.First();
            SemesterDisciplineViewModel semesterDisciplineViewModel =
                new SemesterDisciplineViewModel(DialogCoordinator.Instance, educationPlan, semesterDiscipline);
            SemesterDisciplineWindow semesterDisciplineWindow = new SemesterDisciplineWindow(semesterDisciplineViewModel);

            semesterDisciplineWindow.Show();
            */
          

            //Create a custom principal with an anonymous identity at startup
            CustomPrincipal customPrincipal = new CustomPrincipal();
            AppDomain.CurrentDomain.SetThreadPrincipal(customPrincipal);

            base.OnStartup(e);

            //Show the login view
            AuthenticationViewModel viewModel = new AuthenticationViewModel(new AuthenticationService());
            IView loginWindow = new LoginWindow(viewModel);
            loginWindow.Show();
        }
    }
}
