using DevExpress.Mvvm;
using MahApps.Metro.Controls;

namespace EducationProcess.Desktop.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddGroupToEducationPlanWindow.xaml
    /// </summary>
    public partial class AddGroupToEducationPlanWindow : MetroWindow
    {
        public AddGroupToEducationPlanWindow()
        {
            InitializeComponent();
        }

        public AddGroupToEducationPlanWindow(BindableBase viewModel) : this()
        {
            DataContext = viewModel;
        }
    }
}
