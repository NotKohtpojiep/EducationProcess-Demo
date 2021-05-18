using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;
using DevExpress.Mvvm;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.DataAccess.Entities;

namespace EducationProcess.Desktop.ViewModels
{
    public class DisciplinesViewModel : BindableBase
    {
        public ObservableCollection<Discipline> Disciplines { get; set; }

        public DisciplinesViewModel()
        {
            Discipline[] disciplines = new EducationProcessContext().Disciplines.ToArray();
            Disciplines = new ObservableCollection<Discipline>(disciplines);


            List<Cathedra> cathedras = new List<Cathedra>();
            cathedras.Add(new Cathedra() { Name = "Все" });
            cathedras.AddRange(new EducationProcessContext().Cathedras.ToArray());
            Cathedras = new ObservableCollection<Cathedra>(cathedras);
        }

        public ObservableCollection<Cathedra> Cathedras { get; set; }
        public int SelectedIndex { get; set; }
    }
}