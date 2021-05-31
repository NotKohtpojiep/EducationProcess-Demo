using System.Collections.ObjectModel;
using EducationProcess.Desktop.DataAccess.Entities;

namespace EducationProcess.Desktop.Models
{
    public class LessonItem
    {
        public string PairInfo { get; set; }
        public LessonItem(Discipline[] disciplines, string pairInfo)
        {
            Disciplines = new ObservableCollection<Discipline>(disciplines);
            PairInfo = pairInfo;
        }
        public ObservableCollection<Discipline> Disciplines { get; set; }
        public Discipline SelectedDiscipline { get; set; }
    }
}
