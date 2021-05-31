using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Mvvm;
using EducationProcess.Desktop.Core;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.DataAccess.Entities;
using EducationProcess.Desktop.Models;
using Microsoft.EntityFrameworkCore;

namespace EducationProcess.Desktop.ViewModels
{
    public class GroupSchedule
    {
        public DaySchedule[] DaySchedules { get; set; }
        public Group Group { get; }

        public GroupSchedule(Group group, DaySchedule[] daySchedules)
        {
            DaySchedules = daySchedules;
            Group = group;
        }
    }

    public class CourceScheduleViewModel : BindableBase
    {
        private readonly INavigationManager _navigationManager;
        private readonly EducationProcessContext _context;
        private string[] weekdays = new string[] { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница" };

        public CourceScheduleViewModel(INavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            _context = new EducationProcessContext();

            int maxCourse = new EducationProcessContext().Groups.Max(x => x.CourseNumber);
            CourseGroups = new ObservableCollection<CourseGroups>();
            CourceNumbers = new ObservableCollection<int>();
            for (int i = 1; i <= maxCourse; i++)
            {
                CourseGroups.Add(new CourseGroups(new EducationProcessContext().Groups.Where(x => x.CourseNumber == i).ToArray(), i));
                CourceNumbers.Add(i);
            }

            List<GroupSchedule> groupSchedules = new List<GroupSchedule>();
            foreach (var group in new EducationProcessContext().Groups.ToArray())
            {
                List<DaySchedule> schedules = new List<DaySchedule>();
                for (int i = 0; i < 5; i++)
                {
                    string weekday = weekdays[i];
                    DaySchedule daySchedule = new DaySchedule(new Discipline[]{}, weekday);
                    schedules.Add(daySchedule);
                }
                groupSchedules.Add(new GroupSchedule(group, schedules.ToArray()));
            }

            GroupSchedules = new ObservableCollection<GroupSchedule>(groupSchedules);
            EditScheduleCommand = new RelayCommand(null, _ => _navigationManager.Next(new GroupsScheduleViewModel(_navigationManager)));
            SelectedCourceNumber = CourceNumbers.FirstOrDefault();
        }
        public byte SelectedCource { get; set; }
        public ObservableCollection<GroupSchedule> GroupSchedules { get; set; }
        public ObservableCollection<CourseGroups> CourseGroups { get; set; }
        public ObservableCollection<int> CourceNumbers { get; set; }
        public int SelectedCourceNumber { get; set; }
        public RelayCommand EditScheduleCommand { get; set; }

        private FixedDiscipline[] GetGroupFixedDisciplinesByDate(Group group, DateTime date)
        {
            int currentSemestre = GetSemestreByDate(date, group.ReceiptYear);
            FixedDiscipline[] fixedDisciplines = _context.FixedDisciplines
                .Where(x => x.GroupId == group.GroupId && x.SemesterDiscipline.Semester.Number == currentSemestre)
                .Include(x => x.SemesterDiscipline)
                .ThenInclude(x => x.Discipline)
                .ToArray();
            return fixedDisciplines;
        }

        private int GetSemestreByDate(DateTime currentDate, short receiptYear)
        {
            bool isFirstHalfYear = currentDate.Month / 7.0 < 1;
            int currentCource = currentDate.Year - receiptYear + (isFirstHalfYear ? 0 : 1);
            return currentCource * 2 - (isFirstHalfYear ? 1 : 0);
        }
    }
}