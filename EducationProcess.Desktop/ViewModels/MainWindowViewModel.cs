using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using DevExpress.Mvvm;
using EducationProcess.Desktop.Core;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.DataAccess.Entities;
using EducationProcess.Desktop.Helpers.Identity;
using MahApps.Metro.Controls.Dialogs;

namespace EducationProcess.Desktop.ViewModels
{
    public class MainWindowViewModel : BindableBase, IDataErrorInfo
    {
        private readonly IDialogCoordinator _dialogCoordinator;
        public BaseRoleViewModel SelectedRole
        {
            get { return _selectedRole; }
            set { SetValue(ref _selectedRole, value); }
        }
        private BaseRoleViewModel _selectedRole;

        public BaseRoleViewModel[] Roles { get; }
        public EducationalActivitiesViewModel EducationalActivitiesViewModels { get; set; }
        public SemesterDisciplineViewModel DisciplinesViewModel { get; set; }

        public MainWindowViewModel(IDialogCoordinator dialogCoordinator)
        {
            this._dialogCoordinator = dialogCoordinator;
            // формировать список или создавать нужную модель представления можно через MEF или Reflection.
            // тогда вы не будете зависеть от перечня ролей
            Roles = new BaseRoleViewModel[]
            {
                new AdminRoleViewModel(),
                new UserRoleViewModel(),
            };
            //DisciplinesViewModel = new SemesterDisciplineViewModel();

            EducationalActivitiesViewModels = new EducationalActivitiesViewModel();

            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            if (customPrincipal == null)
                throw new ArgumentException("The application's default thread principal must be set to a CustomPrincipal object on startup.");
            int employeeId = customPrincipal.Identity.EmployeeId;

            Employee currentEmployee = new EducationProcessContext().Employees.FirstOrDefault(x => x.EmployeeId == employeeId);

            SelectedRole = Roles.FirstOrDefault();
            this.ShowInputDialogCommand = new RelayCommand(
                o => true,
                async x =>
                {
                    await this._dialogCoordinator.ShowMessageAsync(this, "From a VM",
                        $"This dialog was shown from a VM, without knowledge of Window... And hello, {currentEmployee.Firstname} {currentEmployee.Lastname}");
                }
            );
            HelloUserMessage = $"Здравствуйте, {currentEmployee.Firstname} {currentEmployee.Lastname}";
            UserRole = $"{SelectedRole.Name}";

        }

        public RelayCommand ShowInputDialogCommand { get; set; }
        public string HelloUserMessage { get; private set; }
        public string UserRole { get; private set; }
        public string Error => string.Empty;

        public string? this[string columnName]
        {
            get
            {
                return null;
            }
        }
    }
}
