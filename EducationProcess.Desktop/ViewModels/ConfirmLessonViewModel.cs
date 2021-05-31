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
using Microsoft.EntityFrameworkCore;

namespace EducationProcess.Desktop.ViewModels
{
    public class ConfirmingLesson
    {
        public int ScheduleDisciplineId { get; set; }
        public ConfirmingLesson(int scheduleDisciplineId, bool isReplacement)
        {
            ScheduleDisciplineId = scheduleDisciplineId;
            IsReplacement = isReplacement;
            if (isReplacement)
            {
                ScheduleDisciplineReplacement replacement =
                    new EducationProcessContext().ScheduleDisciplineReplacements
                        .Include(x => x.FixedDiscipline)
                        .ThenInclude(x => x.SemesterDiscipline)
                        .ThenInclude(x => x.Discipline)
                        .First(x => x.ScheduleDisciplineReplacementId == scheduleDisciplineId);
                PairNumber = replacement.PairNumber;
                Discipline = replacement.FixedDiscipline.SemesterDiscipline.Discipline.Name;
                Group = new EducationProcessContext().Groups
                    .First(x => x.GroupId == replacement.FixedDiscipline.GroupId).Name;
            }
            else
            {
                ScheduleDiscipline discipline =
                    new EducationProcessContext().ScheduleDisciplines
                        .Include(x => x.FixedDiscipline)
                        .ThenInclude(x => x.SemesterDiscipline)
                        .ThenInclude(x => x.Discipline)
                        .First(x => x.ScheduleDisciplineId == scheduleDisciplineId);
                PairNumber = discipline.PairNumber;
                Discipline = discipline.FixedDiscipline.SemesterDiscipline.Discipline.Name;
                Group = new EducationProcessContext().Groups
                    .First(x => x.GroupId == discipline.FixedDiscipline.GroupId).Name;
            }
        }
        public int PairNumber { get; set; }
        public bool IsReplacement { get; set; }
        public string Discipline { get; set; }
        public string Group { get; set; }
    }
    public class ConfirmLessonViewModel : BindableBase
    {
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly Employee _employee;
        private readonly DateTime _date;

        public ConfirmLessonViewModel(Employee employee, DateTime date)
        {
            _employee = employee;
            _date = date;

            ConfirmingLessons = new ObservableCollection<ConfirmingLesson>(GetConfirmingLessons(employee, date));
            ConfirmLessonCommand = new RelayCommand(null, o => ConfirmLesson((ConfirmingLesson)o));
        }

        public ObservableCollection<ConfirmingLesson> ConfirmingLessons { get; set; }

        public RelayCommand ConfirmLessonCommand { get; set; }

        private void ConfirmLesson(ConfirmingLesson confirmingLesson)
        {
            EducationProcessContext context = new EducationProcessContext();
            ConductedPair conductedPair;
            if (confirmingLesson.IsReplacement)
                conductedPair = new ConductedPair() { ScheduleDisciplineReplacementId = confirmingLesson.ScheduleDisciplineId, LessonTypeId = 1 };
            else
                conductedPair = new ConductedPair() { ScheduleDisciplineId = confirmingLesson.ScheduleDisciplineId, LessonTypeId = 1 };

            context.ConductedPairs.Add(conductedPair);
            context.SaveChanges();

            ConfirmingLessons = new ObservableCollection<ConfirmingLesson>(GetConfirmingLessons(_employee, _date));
        }

        private ConfirmingLesson[] GetConfirmingLessons(Employee employee, DateTime date)
        {
            ScheduleDisciplineReplacement[] replacements = new EducationProcessContext()
                .ScheduleDisciplineReplacements
                .Where(x => x.FixedDiscipline.EmployeeId == employee.EmployeeId &&
                            x.Date == date.Date).ToArray();

            ScheduleDiscipline[] scheduleDisciplines = new EducationProcessContext()
                .ScheduleDisciplines
                .Where(x => x.FixedDiscipline.EmployeeId == employee.EmployeeId &&
                            x.Date == date.Date).ToArray();

            List<ConfirmingLesson> confirmingLessons = new List<ConfirmingLesson>();
            foreach (var replacement in replacements)
            {
                if (new EducationProcessContext().ConductedPairs.FirstOrDefault(x => x.ScheduleDisciplineReplacementId == replacement.ScheduleDisciplineReplacementId) == null)
                    confirmingLessons.Add(new ConfirmingLesson(replacement.ScheduleDisciplineReplacementId, true));
            }

            foreach (var scheduleDiscipline in scheduleDisciplines)
            {
                if (replacements.FirstOrDefault(x => x.ScheduleDisciplineId == scheduleDiscipline.ScheduleDisciplineId) == null &&
                    new EducationProcessContext().ConductedPairs.FirstOrDefault(x => x.ScheduleDisciplineId == scheduleDiscipline.ScheduleDisciplineId) == null)
                    confirmingLessons.Add(new ConfirmingLesson(scheduleDiscipline.ScheduleDisciplineId, false));
            }

            return confirmingLessons.ToArray();
        }
    }
}