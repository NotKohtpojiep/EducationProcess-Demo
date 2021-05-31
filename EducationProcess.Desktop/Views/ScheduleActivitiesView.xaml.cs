using System.Windows.Controls;
using EducationProcess.Desktop.ViewModels;

namespace EducationProcess.Desktop.Views
{
    /// <summary>
    /// Логика взаимодействия для ScheduleActivitiesView.xaml
    /// </summary>
    public partial class ScheduleActivitiesView : UserControl
    {
        public ScheduleActivitiesView()
        {
            InitializeComponent();
            DataContext = new ScheduleActivitiesViewModel();
        }
    }
}
