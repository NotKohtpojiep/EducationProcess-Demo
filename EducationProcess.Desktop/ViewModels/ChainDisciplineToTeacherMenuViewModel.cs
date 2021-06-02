using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EducationProcess.Desktop.Core;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.DataAccess.Entities;
using EducationProcess.Desktop.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace EducationProcess.Desktop.ViewModels
{
    public class ChainDisciplineToTeacherMenuViewModel
    {
        public ChainDisciplineToTeacherMenuViewModel()
        {
            SuggestingDisciplines = new ObservableCollection<SuggestingDiscipline>(LoadSuggestingDisciplines());
            SuggestDisciplineCommand = new RelayCommand(null, _ => ShowSuggestionWindow());
        }
        public RelayCommand SuggestDisciplineCommand { get; set; }
        public ObservableCollection<SuggestingDiscipline> SuggestingDisciplines { get; set; }

        private void ShowSuggestionWindow()
        {
            AddDisciplineToTeacherViewModel viewModel = new AddDisciplineToTeacherViewModel();
            AddDisciplineToTeacherWindow window = new AddDisciplineToTeacherWindow(viewModel);
            window.ShowDialog();
            SuggestingDisciplines = new ObservableCollection<SuggestingDiscipline>(LoadSuggestingDisciplines());
        }

        private SuggestingDiscipline[] LoadSuggestingDisciplines()
        {
            var groupedFixedDisciplines = new EducationProcessContext()
                .FixedDisciplines
                .Include(x => x.Employee)
                .Include(x => x.SemesterDiscipline)
                .ThenInclude(x => x.Discipline)
                .Include(x => x.SemesterDiscipline)
                .ThenInclude(x => x.Semester)
                .Include(x => x.Group)
                .ToArray()
                .GroupBy(x =>
                    new
                    {
                        x.SemesterDiscipline.DisciplineId,
                        x.GroupId,
                        x.IsAgreed
                    });

            List<SuggestingDiscipline> suggestingDisciplines = new List<SuggestingDiscipline>();
            foreach (var fixedDiscipline in groupedFixedDisciplines)
            {
                suggestingDisciplines.Add(new SuggestingDiscipline(fixedDiscipline.ToArray()));
            }

            return suggestingDisciplines.ToArray();
        }
    }

    public class SuggestingDiscipline
    {
        private FixedDiscipline[] _fixedDisciplines;
        public SuggestingDiscipline(FixedDiscipline[] fixedDisciplines)
        {
            _fixedDisciplines = fixedDisciplines;
            string fio =
                $"{fixedDisciplines.First().Employee.Firstname} {fixedDisciplines.First().Employee.Lastname} {fixedDisciplines.First().Employee.Middlename}";
            Teacher = fio;
            Discipline = fixedDisciplines.First().SemesterDiscipline.Discipline.Name;
            Group = fixedDisciplines.First().Group.Name;
            SemestreNumber = $"{string.Join(",", fixedDisciplines.Select(x => x.SemesterDiscipline.Semester.Number))}";

            if (fixedDisciplines.First().IsAgreed == null)
            {
                StatusIconBackground = "Orange";
                StatusIconKind = "StickerAlert";
            }

            if (fixedDisciplines.First().IsAgreed == true)
            {
                StatusIconBackground = "Green";
                StatusIconKind = "StickerCheck";
            }

            if (fixedDisciplines.First().IsAgreed == false)
            {
                StatusIconBackground = "Tomato";
                StatusIconKind = "StickerRemove";
            }
        }
        public string Teacher { get; set; }
        public string Discipline { get; set; }
        public string Group { get; set; }
        public string SemestreNumber { get; set; }
        public string StatusIconBackground { get; set; }
        public string StatusIconKind { get; set; }
    }
}