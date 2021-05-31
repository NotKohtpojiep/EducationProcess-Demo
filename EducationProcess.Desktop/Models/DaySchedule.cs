using System.Collections.Generic;
using System.Collections.ObjectModel;
using EducationProcess.Desktop.DataAccess.Entities;

namespace EducationProcess.Desktop.Models
{
    public class DaySchedule
    {
        public DaySchedule(Discipline[] suggestionDisciplines, string weekday)
        {
            List<Lesson> disciplines = new List<Lesson>();
            for (int i = 0; i < 5; i++)
            {
                disciplines.Add(new Lesson(suggestionDisciplines, i + 1));
            }

            Weekday = weekday;
            Lessons = new ObservableCollection<Lesson>(disciplines);
        }
        public string Weekday { get; set; }
        public ObservableCollection<Lesson> Lessons { get; set; }
    }
}
