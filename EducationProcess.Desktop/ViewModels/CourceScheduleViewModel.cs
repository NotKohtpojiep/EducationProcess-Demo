using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.DataAccess.Entities;
using EducationProcess.Desktop.Helpers;

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

    public class CourceScheduleViewModel
    {
        private string[] weekdays = new string[] { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница" };
        public CourceScheduleViewModel()
        {
            Discipline[] disciplines = new EducationProcessContext().Disciplines.ToArray();
            int maxCourse = new EducationProcessContext().Groups.Max(x => x.CourseNumber);
            CourseGroups = new ObservableCollection<CourseGroups>();
            for (int i = 1; i <= maxCourse; i++)
            {
                CourseGroups.Add(new CourseGroups(new EducationProcessContext().Groups.Where(x => x.CourseNumber == i).ToArray(), i));
            }

            List<GroupSchedule> groupSchedules = new List<GroupSchedule>();
            foreach (var group in new EducationProcessContext().Groups.ToArray())
            {
                List<DaySchedule> schedules = new List<DaySchedule>();
                for (int i = 0; i < 5; i++)
                {
                    string weekday = weekdays[i];
                    DaySchedule daySchedule = new DaySchedule(disciplines, weekday);
                    schedules.Add(daySchedule);
                }
                groupSchedules.Add(new GroupSchedule(group, schedules.ToArray()));
            }

            GroupSchedules = new ObservableCollection<GroupSchedule>(groupSchedules);
        }
        public byte SelectedCource { get; set; }
        public ObservableCollection<GroupSchedule> GroupSchedules { get; set; }
        public ObservableCollection<CourseGroups> CourseGroups { get; set; }
    }
}
