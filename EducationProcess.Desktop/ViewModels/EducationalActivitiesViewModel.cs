using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Mvvm;
using EducationProcess.Desktop.Core;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.DataAccess.Entities;
using EducationProcess.Desktop.Helpers.Excel;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;

namespace EducationProcess.Desktop.ViewModels
{
    public interface INavigationManager
    {
        void Next(BindableBase viewModel);
        void Back();
    }

    public class EducationalActivitiesViewModel : BindableBase, INavigationManager
    {
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly INavigationManager _navigationManager;
        private List<BindableBase> _selectedViewModels;
        public BindableBase SelectedViewModel { get; set; }
        public ObservableCollection<SemesterDiscipline> SemesterDisciplines { get; set; }
        public ObservableCollection<SemesterDiscipline> SemesterResult { get; set; }
        public ObservableCollection<string> AcademicYears { get; set; }

        public EducationalActivitiesViewModel()
        {
            _dialogCoordinator = DialogCoordinator.Instance;
            _selectedViewModels = new List<BindableBase>(new[] { new EducationPlansMenuViewModel(this) });
            SelectedViewModel = _selectedViewModels[0];

            SemesterDiscipline[] disciplines = new EducationProcessContext().SemesterDisciplines
                .Include(x => x.Discipline)
                .ToArray();
            List<SemesterDiscipline> semestreInfos = new List<SemesterDiscipline>();

            string[] academicYears = new string[] { "2020-2021", "2019-2020", "2018-2019" };

            SemesterDisciplines = new ObservableCollection<SemesterDiscipline>(disciplines);
            AcademicYears = new ObservableCollection<string>(academicYears);
            ConvertToExcelCommand = new RelayCommand(null, _ => ConvertDataToExcel());
        }

        public RelayCommand ConvertToExcelCommand { get; set; }

        private void ConvertDataToExcel()
        {
            Discipline[] disciplines = new EducationProcessContext().Disciplines.ToArray();

            ExcelEducationPlanWriter.ToExcelFile(disciplines, null);
            _dialogCoordinator.ShowMessageAsync(this, "Конвертация в таблицу", "Операция завершена успешо.");
        }

        public void Next(BindableBase viewModel)
        {
            _selectedViewModels.Add(viewModel);
            SelectedViewModel = viewModel;
        }

        public void Back()
        {
            _selectedViewModels.RemoveAt(_selectedViewModels.Count - 1);
            SelectedViewModel = _selectedViewModels.Last();
        }
    }
}