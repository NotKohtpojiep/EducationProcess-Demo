using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EducationProcess.Desktop.Views
{
    /// <summary>
    /// Логика взаимодействия для TeacherScheduleView.xaml
    /// </summary>
    public partial class TeacherScheduleView : UserControl
    {
        public TeacherScheduleView()
        {
            InitializeComponent();
            InitializeComponent();
            ObservableCollection<@group> samdata = new ObservableCollection<group>
            {
                new group{groupname="Group1",CLgroup="xxx",displayname="demo1"},
                new group{groupname="Group1",CLgroup="yyy",displayname="demo2"},
                new group{groupname="Group1",CLgroup="yyy",displayname="demo2"},
                new group{groupname="Group2",CLgroup="yyy",displayname="demo2"},
                new group{groupname="Group2",CLgroup="yyy",displayname="demo2"},
                new group{groupname="Group2",CLgroup="yyy",displayname="demo2"},
                new group{groupname="Group3",CLgroup="zzz",displayname="demo3"},
                new group{groupname="Group3",CLgroup="yyy",displayname="demo2"},
                new group{groupname="Group3",CLgroup="yyy",displayname="demo2"},
            };
            ListCollectionView collection = new ListCollectionView(samdata);
            collection.GroupDescriptions.Add(new PropertyGroupDescription("groupname"));
            dgdata.ItemsSource = collection;
        }
        public class group
        {
            public string groupname { get; set; }
            public string CLgroup { get; set; }
            public string displayname { get; set; }
        }
    }
}
