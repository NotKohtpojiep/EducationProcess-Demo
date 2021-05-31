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
    /// Логика взаимодействия для DisciplineEditorWindow.xaml
    /// </summary>
    public partial class DisciplineEditorWindow : MetroWindow
    {
        public DisciplineEditorWindow()
        {
            InitializeComponent();
        }
        public DisciplineEditorWindow(BindableBase dataContext) : this()
        {
            DataContext = dataContext;
        }
    }
}
