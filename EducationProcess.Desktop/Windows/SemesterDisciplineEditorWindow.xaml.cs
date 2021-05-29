using System.Linq;
using DevExpress.Mvvm;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.DataAccess.Entities;
using EducationProcess.Desktop.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace EducationProcess.Desktop.Windows
{
    /// <summary>
    /// Логика взаимодействия для DisciplineWindow.xaml
    /// </summary>
    public partial class SemesterDisciplineEditorWindow : MetroWindow
    {
        public SemesterDisciplineEditorWindow()
        {
            InitializeComponent();
            EducationPlan educationPlan = new EducationProcessContext().EducationPlans.First();
            SemesterDiscipline discipline = new EducationProcessContext().SemesterDisciplines.First();
            DataContext = new SemesterDisciplineEditViewModel(DialogCoordinator.Instance, educationPlan, discipline);
        }
        public SemesterDisciplineEditorWindow(BindableBase dataContext) : this()
        {
            DataContext = dataContext;
        }
    }
}
