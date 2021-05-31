using System.Windows;
using DevExpress.Mvvm;
using MahApps.Metro.Controls;

namespace EducationProcess.Desktop.Windows
{
    /// <summary>
    /// Логика взаимодействия для DisciplinesStatisticWindow.xaml
    /// </summary>
    public partial class DisciplinesStatisticWindow : MetroWindow
    {
        public DisciplinesStatisticWindow()
        {
            InitializeComponent();
        }
        public DisciplinesStatisticWindow(BindableBase viewModel) : this()
        {
            DataContext = viewModel;
        }
    }
}
