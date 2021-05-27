using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using EducationProcess.Desktop.Core;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.DataAccess.Entities;
using EducationProcess.Desktop.Helpers;
using MahApps.Metro.Controls.Dialogs;

namespace EducationProcess.Desktop.ViewModels
{
    public class EducationWeek
    {
        public string Name { get; set; }
        public DateTime StartDay { get; set; }

        public EducationWeek(DateTime start, string name)
        {
            StartDay = start;
            Name = name;
        }

        public string WeekInfo
        {
            get
            {
                int firstDayOfYear = (int)new DateTime(StartDay.Year, 1, 1).DayOfWeek;
                int weekNumber = (StartDay.DayOfYear + firstDayOfYear) / 7;
                return $"({weekNumber}) {StartDay.ToShortDateString()} - {StartDay.AddDays(6).ToShortDateString()}";
            }
        }
    }

    public class CourseGroups
    {
        public int Course { get; set; }
        public string CourseInfo
        {
            get => $"Курс: {Course}";
        }
        public Group[] CourseGroupsCollection { get; set; }
        public CourseGroups(Group[] courseGroups, int semestreNumber)
        {
            Course = semestreNumber;
            CourseGroupsCollection = courseGroups;
        }
    }

    public class GroupsScheduleViewModel : BindableBase
    {
        private string[] weekdays = new string[] { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница" };
        public GroupsScheduleViewModel()
        {
            Discipline[] disciplines = new EducationProcessContext().Disciplines.ToArray();
            int maxCourse = new EducationProcessContext().Groups.Max(x => x.CourseNumber);
            CourseGroups = new ObservableCollection<CourseGroups>();
            for (int i = 1; i <= maxCourse; i++)
            {
                CourseGroups.Add(new CourseGroups(new EducationProcessContext().Groups.Where(x => x.CourseNumber == i).ToArray(), i));
            }
            Weeks = new ObservableCollection<EducationWeek>();
            for (int i = -5; i < 2; i++)
            {
                Weeks.Add(new EducationWeek(DateTime.Now.AddDays(i * 7).StartOfWeek(DayOfWeek.Monday), "Неделя"));
            }
            List<DaySchedule> schedules = new List<DaySchedule>();
            SelectedWeek = Weeks[5];
            for (int i = 0; i < 5; i++)
            {
                string weekday = weekdays[i] + " - " + SelectedWeek.StartDay.AddDays(i).ToShortDateString();
                DaySchedule daySchedule = new DaySchedule(disciplines, weekday);
                schedules.Add(daySchedule);
            }

            WeekSchedule = new ObservableCollection<DaySchedule>(schedules);
        }
        public EducationWeek SelectedWeek { get; set; }
        public ObservableCollection<DaySchedule> WeekSchedule { get; set; }
        public ObservableCollection<EducationWeek> Weeks { get; set; }
        public ObservableCollection<CourseGroups> CourseGroups { get; set; }

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
                PairOptions[0] = new LessonItem(_disciplines, "Числитель");
                PairOptions.Add(new LessonItem(_disciplines, "Знаменатель"));
            }
            else
            {
                PairOptions.RemoveAt(PairOptions.Count - 1);
                PairOptions[0] = new LessonItem(_disciplines, "Обычный");
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