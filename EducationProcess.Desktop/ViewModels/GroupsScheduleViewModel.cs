using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using EducationProcess.Desktop.Core;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.DataAccess.Entities;
using MahApps.Metro.Controls.Dialogs;

namespace EducationProcess.Desktop.ViewModels
{

    public class GroupsScheduleViewModel : BindableBase
    {
        private string[] weekdays = new string[] { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница" };
        public GroupsScheduleViewModel()
        {
            Discipline[] disciplines = new EducationProcessContext().Disciplines.ToArray();
            Group[] groups = new EducationProcessContext().Groups.ToArray();
            Groups = new ObservableCollection<Group>(groups);

            Dates = new ObservableCollection<DateTime>()
                {DateTime.Now, DateTime.Now.AddDays(7), DateTime.Now.AddDays(14)};
            List<DaySchedule> schedules = new List<DaySchedule>();
            for (int i = 0; i < 5; i++)
            {
                string weekday = weekdays[i] + " - " + DateTime.Now.AddDays(i).ToShortDateString();
                DaySchedule daySchedule = new DaySchedule(disciplines, weekday);
                schedules.Add(daySchedule);
            }

            WeekSchedule = new ObservableCollection<DaySchedule>(schedules);         
        }
        public ObservableCollection<DaySchedule> WeekSchedule { get; set; }
        public ObservableCollection<DateTime> Dates { get; set; }
        public ObservableCollection<Group> Groups { get; set; }

    }

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

    public class Lesson
    {
        private List<LessonItem> _pairOptions;
        private Discipline[] _disciplines;
        public int PairNumber { get; set; }
        public ObservableCollection<LessonItem> PairOptions { get; set; }
        public bool IsNotWhole { get; set; }
        public Lesson(Discipline[] disciplines, int pairNumber)
        {
            _disciplines = disciplines;
            _pairOptions = new List<LessonItem>() { new LessonItem(disciplines, "") };
            PairOptions = new ObservableCollection<LessonItem>(_pairOptions);
            PairNumber = pairNumber;
            ChangeCountLessonOptionsCommand = new RelayCommand(null, _ => ChangePairOptions());
        }

        public RelayCommand ChangeCountLessonOptionsCommand { get; set; }

        private void ChangePairOptions()
        {
            if (IsNotWhole)
            {
                PairOptions[0] = new LessonItem(_disciplines, "Ч");
                PairOptions.Add(new LessonItem(_disciplines, "З"));
            }
            else
            {
                PairOptions.RemoveAt(PairOptions.Count - 1);
                PairOptions[0] = new LessonItem(_disciplines, "");
            }
        }
    }

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