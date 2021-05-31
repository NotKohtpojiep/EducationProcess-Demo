using System;
using System.Linq;
using System.Threading;
using EducationProcess.Desktop.Core;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.DataAccess.Entities;
using EducationProcess.Desktop.Helpers.Identity;
using EducationProcess.Desktop.ViewModels;
using EducationProcess.Desktop.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;

namespace EducationProcess.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, IView
    {
        private readonly MainWindowViewModel _viewModel;
        public MainWindow()
        {
            this._viewModel = new MainWindowViewModel(DialogCoordinator.Instance);
            this.DataContext = this._viewModel;
            InitializeComponent();

            CustomPrincipal? customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            if (customPrincipal == null)
                throw new ArgumentException("The application's default thread principal must be set to a CustomPrincipal object on startup.");
            int employeeId = customPrincipal.Identity.EmployeeId;
            Employee employee = new EducationProcessContext().Employees
                .Where(x => x.EmployeeId == employeeId)
                .Include(x => x.Post).First();

            string role = employee.Post.Name;
            object viewByRole = null;
            if (role == "Руководитель УМО")
                viewByRole = new UmoManagerMainView();
            if (role == "Сотрудник УМО")
                viewByRole = new UmoEmployeeMainView();
            if (role == "Преподаватель")
            {
                TeacherMainView mainView = new TeacherMainView();
                mainView.DataContext = new TeacherMainViewModel(DialogCoordinator.Instance);
                viewByRole = mainView;
            }

            MainFrame.Navigate(viewByRole);
        }

        #region IView Members
        public IViewModel ViewModel
        {
            get { return DataContext as IViewModel; }
            set { DataContext = value; }
        }
        #endregion
    }
}
