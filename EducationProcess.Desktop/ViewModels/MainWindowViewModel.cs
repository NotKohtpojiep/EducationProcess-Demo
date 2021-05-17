using System.Linq;
using DevExpress.Mvvm;

namespace EducationProcess.Desktop.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public BaseRoleViewModel SelectedRole
        {
            get { return _selectedRole; }
            set { SetValue(ref _selectedRole, value); }
        }
        private BaseRoleViewModel _selectedRole;

        public BaseRoleViewModel[] Roles { get; }

        public MainWindowViewModel()
        {
            // формировать список или создавать нужную модель представления можно через MEF или Reflection.
            // тогда вы не будете зависеть от перечня ролей
            Roles = new BaseRoleViewModel[]
            {
                new AdminRoleViewModel(),
                new UserRoleViewModel(),
            };

            SelectedRole = Roles.FirstOrDefault();
        }
    }
}
