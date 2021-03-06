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
using MahApps.Metro.Controls;

namespace EducationProcess.Desktop.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddDisciplineToTeacherWindow.xaml
    /// </summary>
    public partial class AddDisciplineToTeacherWindow : MetroWindow
    {
        public AddDisciplineToTeacherWindow()
        {
            InitializeComponent();
        }
        public AddDisciplineToTeacherWindow(BindableBase viewModel) : this()
        {
            DataContext = viewModel;
        }
    }
}
