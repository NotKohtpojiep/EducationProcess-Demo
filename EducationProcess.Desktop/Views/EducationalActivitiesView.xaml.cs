using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
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
using EducationProcess.Desktop.DataAccess.Entities;
using EducationProcess.Desktop.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace EducationProcess.Desktop.Views
{
    /// <summary>
    /// Логика взаимодействия для EducationalActivitiesView.xaml
    /// </summary>
    public partial class EducationalActivitiesView : UserControl
    {
        public EducationalActivitiesView()
        {
            InitializeComponent();
            DataContext = new EducationalActivitiesViewModel();
        }
        
    }
}
