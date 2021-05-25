using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Mvvm;
using EducationProcess.Desktop.Core;
using EducationProcess.Desktop.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace EducationProcess.Desktop.Windows
{
    /// <summary>
    /// Логика взаимодействия для DisciplineWindow.xaml
    /// </summary>
    public partial class SemesterDisciplineWindow : MetroWindow
    {
        public SemesterDisciplineWindow()
        {
            InitializeComponent();
        }
        public SemesterDisciplineWindow(BindableBase dataContext) : this()
        {
            DataContext = dataContext;
        }
    }
}
