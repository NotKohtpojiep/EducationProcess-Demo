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
        private readonly EducationPlan _educationPlan;

        public EducationPlanDisciplinesMenuViewModel(INavigationManager navigationManager, EducationPlan educationPlan)
        {
            _dialogCoordinator = DialogCoordinator.Instance;
            _navigationManager = navigationManager;
            _educationPlan = educationPlan;

            int[] semesterDisciplinesEducPlan = new EducationProcessContext()
                .EducationPlanSemesterDisciplines
                .Where(x => x.EducationPlanId == educationPlan.EducationPlanId)
                .Select(x => x.SemesterDisciplineId)
                .ToArray();

            SemesterDiscipline[] semesterDisciplines = new EducationProcessContext()
                .SemesterDisciplines
                .Where(x => semesterDisciplinesEducPlan.Contains(x.SemesterDisciplineId))
                .Include(x => x.Discipline)
                .Include(x => x.Semester)
                .ToArray();

            int semesterDisciplineAmountHours = semesterDisciplines
                .Sum(x => x.EducationalPracticeHours +
                          x.ConsultationHours +
                          x.ControlWorkHours +
                          x.ExamHours +
                          x.IndependentWorkHours +
                          x.LaboratoryWorkHours +
                          x.PracticeWorkHours +
                          x.ProductionPracticeHours +
                          x.TheoryLessonHours);

            SemesterDisciplines = new ObservableCollection<SemesterDiscipline>(semesterDisciplines);
            EducationPlanInfo = $"Всего часов: {semesterDisciplineAmountHours}\tВремя обучения (семестров): {semesterDisciplines.Max(x => x.Semester.Number)}";

            ConvertToExcelCommand = new RelayCommand(null, _ => ConvertDataToExcel());
            PageBackCommand = new RelayCommand(null, _ => PageBack());
        }

        public ObservableCollection<SemesterDiscipline> SemesterDisciplines { get; set; }
        public string EducationPlanInfo { get; set; }

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
