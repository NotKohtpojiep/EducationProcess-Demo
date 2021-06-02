using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows.Documents;
using DevExpress.Mvvm;
using EducationProcess.Desktop.Core;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.DataAccess.Entities;
using EducationProcess.Desktop.Helpers.Identity;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;

namespace EducationProcess.Desktop.ViewModels
{
    public class CheckDisciplineSuggestionsViewModel : BindableBase
    {
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly int _employeeId;
        public CheckDisciplineSuggestionsViewModel()
        {
            _dialogCoordinator = DialogCoordinator.Instance;

            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            if (customPrincipal == null)
                throw new ArgumentException("The application's default thread principal must be set to a CustomPrincipal object on startup.");
            _employeeId = customPrincipal.Identity.EmployeeId;

            SuggestingDisciplines = new ObservableCollection<SuggestingDiscipline>(GetSuggestingDisciplines(_employeeId));
            AcceptDisciplineCommand = new RelayCommand(null, o => AcceptDiscipline((SuggestingDiscipline)o));
            DeclineDisciplineCommand = new RelayCommand(null, o => DecineDiscipline((SuggestingDiscipline)o));
        }

        public ObservableCollection<SuggestingDiscipline> SuggestingDisciplines { get; set; }

        public RelayCommand AcceptDisciplineCommand { get; set; }
        public RelayCommand DeclineDisciplineCommand { get; set; }

        private async void AcceptDiscipline(SuggestingDiscipline suggestingDiscipline)
        {
            MessageDialogResult mgs = await _dialogCoordinator.ShowMessageAsync(this, null,
                $"Вы уверены?", MessageDialogStyle.AffirmativeAndNegative);
            if (mgs == MessageDialogResult.Affirmative)
            {
                EducationProcessContext context = new EducationProcessContext();
                foreach (var fixedDiscipline in suggestingDiscipline.FixedDisciplines)
                {
                    fixedDiscipline.IsAgreed = true;
                    context.FixedDisciplines.Update(fixedDiscipline);
                }
                context.SaveChanges();
                await _dialogCoordinator.ShowMessageAsync(this, null, "Подтверждено");
            }
            SuggestingDisciplines =
                new ObservableCollection<SuggestingDiscipline>(GetSuggestingDisciplines(_employeeId));
        }

        private async void DecineDiscipline(SuggestingDiscipline suggestingDiscipline)
        {
            MessageDialogResult mgs = await _dialogCoordinator.ShowMessageAsync(this, null,
                $"Вы уверены?", MessageDialogStyle.AffirmativeAndNegative);
            if (mgs == MessageDialogResult.Affirmative)
            {
                EducationProcessContext context = new EducationProcessContext();
                foreach (var fixedDiscipline in suggestingDiscipline.FixedDisciplines)
                {
                    fixedDiscipline.IsAgreed = false;
                    context.FixedDisciplines.Update(fixedDiscipline);
                }
                context.SaveChanges();
                await _dialogCoordinator.ShowMessageAsync(this, null, "Отменено");
            }
            SuggestingDisciplines =
                new ObservableCollection<SuggestingDiscipline>(GetSuggestingDisciplines(_employeeId));
        }

        private SuggestingDiscipline[] GetSuggestingDisciplines(int employeeId)
        {
            var disciplines = new EducationProcessContext().FixedDisciplines
                .Where(x => x.IsAgreed == null && x.EmployeeId == employeeId)
                .Include(x => x.Group)
                .Include(x => x.SemesterDiscipline)
                .ThenInclude(x => x.Discipline)
                .Include(x => x.SemesterDiscipline.Semester)
                .ToArray()
                .GroupBy(x => new
                {
                    x.SemesterDiscipline.DisciplineId,
                    x.GroupId
                });

            List<SuggestingDiscipline> suggestingDisciplines = new List<SuggestingDiscipline>();
            foreach (var discipline in disciplines)
            {
                SuggestingDiscipline suggestingDiscipline =
                    new SuggestingDiscipline(discipline.ToArray());
                suggestingDisciplines.Add(suggestingDiscipline);
            }

            return suggestingDisciplines.ToArray();
        }

        public class SuggestingDiscipline
        {
            private FixedDiscipline[] _fixedDisciplines;
            public SuggestingDiscipline(FixedDiscipline[] fixedDisciplines)
            {
                _fixedDisciplines = fixedDisciplines;
                Discipline = fixedDisciplines.First().SemesterDiscipline.Discipline.Name;
                Group = fixedDisciplines.First().Group.Name;
                SemestreNumber = $"{string.Join(",", fixedDisciplines.Select(x => x.SemesterDiscipline.Semester.Number))}";
            }
            public FixedDiscipline[] FixedDisciplines { get => _fixedDisciplines; }
            public string Discipline { get; set; }
            public string Group { get; set; }
            public string SemestreNumber { get; set; }
        }
    }
}