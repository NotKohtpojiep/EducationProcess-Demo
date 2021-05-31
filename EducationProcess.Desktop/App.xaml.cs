using System;
using System.Windows;
using EducationProcess.Desktop.Core;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.Helpers.Identity;
using EducationProcess.Desktop.ViewModels;
using EducationProcess.Desktop.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EducationProcess.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /*
        private ServiceProvider serviceProvider;
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }
        private void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<EducationProcessContext>(options =>
            {
                options.UseSqlServer("Server=DESKTOP-F79I7DI\\PLEASEBEMYSEMPAI;Database=EducationProcess;Trusted_Connection=True;");
            });
            services.AddSingleton<MainWindow>();
            
        }
        */

        protected override void OnStartup(StartupEventArgs e)
        {
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
