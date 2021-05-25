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
    public class EducationPlanDisciplinesMenuViewModel : BindableBase
    {
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly INavigationManager _navigationManager;
        public ObservableCollection<SemesterDiscipline> SemesterDisciplines { get; set; }

        public EducationPlanDisciplinesMenuViewModel(INavigationManager navigationManager)
        {
            _dialogCoordinator = DialogCoordinator.Instance;
            _navigationManager = navigationManager;

            SemesterDiscipline[] semesterDisciplines = new EducationProcessContext().SemesterDisciplines
                .Include(x => x.Discipline)
                .ToArray();
            SemesterDisciplines = new ObservableCollection<SemesterDiscipline>(semesterDisciplines);
            ConvertToExcelCommand = new RelayCommand(null, _ => ConvertDataToExcel());
            PageBackCommand = new RelayCommand(null, _ => PageBack());
        }
        public RelayCommand ConvertToExcelCommand { get; set; }

        private void ConvertDataToExcel()
        {
            Discipline[] disciplines = new EducationProcessContext().Disciplines.ToArray();

            ExcelEducationPlanWriter.ToExcelFile(disciplines, null);
            _dialogCoordinator.ShowMessageAsync(this, "Конвертация в таблицу", "Операция завершена успешо.");
        }

        public RelayCommand PageBackCommand { get; set; }

        private void PageBack()
        {
            _navigationManager.Back();
        }
    }
}
