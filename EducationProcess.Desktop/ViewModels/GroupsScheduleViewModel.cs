using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Mvvm;
using EducationProcess.Desktop.Core;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.DataAccess.Entities;
using EducationProcess.Desktop.Helpers;
using EducationProcess.Desktop.Models;
using EducationProcess.Desktop.Windows;
using MahApps.Metro.Controls.Dialogs;

namespace EducationProcess.Desktop.ViewModels
{
    public class GroupsScheduleViewModel : BindableBase
    {
        private readonly INavigationManager _navigationManager;
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly EducationProcessContext _context;

        private string[] weekdays = new string[] { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница" };
        public GroupsScheduleViewModel(INavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            _dialogCoordinator = DialogCoordinator.Instance;
            _context = new EducationProcessContext();

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
            PageBackCommand = new RelayCommand(null, _ => _navigationManager.Back());
            ShowDisciplinesStatisticCommand =
                new RelayCommand(null, _ => ShowDisciplinesStatisticWindow(SelectedGroup));
            SelectGroupCommand = new RelayCommand(null, o => SelectGroup((Group) o));
        }

        public EducationWeek SelectedWeek { get; set; }
        public ObservableCollection<DaySchedule> WeekSchedule { get; set; }
        public ObservableCollection<EducationWeek> Weeks { get; set; }
        public ObservableCollection<CourseGroups> CourseGroups { get; set; }
        public Group SelectedGroup { get; set; }

        public RelayCommand PageBackCommand { get; set; }
        public RelayCommand ShowDisciplinesStatisticCommand { get; set; }
        public RelayCommand SelectGroupCommand { get; set; }

        private void SelectGroup(Group group)
        {
            SelectedGroup = group;
        }

        private void ShowDisciplinesStatisticWindow(Group group)
        {
            if (group == null)
            {
                _dialogCoordinator.ShowMessageAsync(this, "Внимание", "Выберите группу");
                return;
            }

            int currentSemestre = GetSemestreByDate(DateTime.Now, group.ReceiptYear);
            DisciplineStatisticViewModel viewModel = new DisciplineStatisticViewModel(group, currentSemestre);
            DisciplinesStatisticWindow window = new DisciplinesStatisticWindow(viewModel);
            window.Show();
        }

        private FixedDiscipline[] GetGroupFixedDisciplinesByDate(Group group, DateTime date)
        {
            int currentSemestre = GetSemestreByDate(date, group.ReceiptYear);
            FixedDiscipline[] fixedDisciplines = _context.FixedDisciplines
                .Where(x => x.GroupId == group.GroupId && x.SemesterDiscipline.Semester.Number == currentSemestre)
                .ToArray();
            return fixedDisciplines;
        }

        private int GetSemestreByDate(DateTime currentDate, short receiptYear)
        {
            bool isFirstHalfYear = currentDate.Month / 7.0 < 1;
            int currentCource = currentDate.Year - receiptYear + (isFirstHalfYear ? 0 : 1);
            return currentCource * 2 - (isFirstHalfYear ? 0 : 1);
        }
    }
}