using DevExpress.Mvvm;
using MahApps.Metro.Controls;

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
        }
        public SemesterDisciplineEditorWindow(BindableBase dataContext) : this()
        {
            DataContext = dataContext;
        }
    }
}
