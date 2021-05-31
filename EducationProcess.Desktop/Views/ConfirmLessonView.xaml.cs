using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.DataAccess.Entities;
using EducationProcess.Desktop.Helpers.Identity;
using EducationProcess.Desktop.ViewModels;

namespace EducationProcess.Desktop.Views
{
    /// <summary>
    /// Логика взаимодействия для ConfirmLessonView.xaml
    /// </summary>
    public partial class ConfirmLessonView : UserControl
    {
        public ConfirmLessonView()
        {
            InitializeComponent();

            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            if (customPrincipal == null)
                throw new ArgumentException("The application's default thread principal must be set to a CustomPrincipal object on startup.");
            int employeeId = customPrincipal.Identity.EmployeeId;

            Employee currentEmployee = new EducationProcessContext().Employees.FirstOrDefault(x => x.EmployeeId == employeeId);

            DataContext = new ConfirmLessonViewModel(currentEmployee, DateTime.Now);
        }
    }
}
