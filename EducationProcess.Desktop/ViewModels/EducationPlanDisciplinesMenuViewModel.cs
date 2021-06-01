using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Mvvm;
using EducationProcess.Desktop.Core;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.DataAccess.Entities;
using EducationProcess.Desktop.Helpers.Excel;
using EducationProcess.Desktop.Windows;
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

            SemesterDiscipline[] semesterDisciplines = new EducationProcessContext()
                .EducationPlanSemesterDisciplines
                .Where(x => x.EducationPlanId == educationPlan.EducationPlanId)
                .Include(x => x.SemesterDiscipline)
                .ThenInclude(x => x.Discipline)
                .Include(x => x.SemesterDiscipline)
                .ThenInclude(x => x.Semester)
                .Select(x => x.SemesterDiscipline)
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
            int semestreMax = semesterDisciplines.Length != 0 ? semesterDisciplines.Max(x => x.Semester.Number) : 0;

            SemesterDisciplines = new ObservableCollection<SemesterDiscipline>(semesterDisciplines);
            EducationPlanInfo = $"Всего часов: {semesterDisciplineAmountHours}\tВремя обучения (семестров): {semestreMax}";

            ConvertToExcelCommand = new RelayCommand(null, _ => ConvertDataToExcel());
            PageBackCommand = new RelayCommand(null, _ => PageBack());
            AddSemesterDisciplineCommand = new RelayCommand(null, _ => AddSemesterDiscipline(_educationPlan));
            EditSemesterDisciplineCommand = new RelayCommand(null,
                _ => EditSemesterDiscipline(_educationPlan, SelectedSemesterDiscipline));
            RemoveSemesterDisciplineCommand = new RelayCommand(null,
                _ => DeleteSemesterDiscipline(_educationPlan, SelectedSemesterDiscipline));
        }

        public ObservableCollection<SemesterDiscipline> SemesterDisciplines { get; set; }
        public SemesterDiscipline SelectedSemesterDiscipline { get; set; }
        public string EducationPlanInfo { get; set; }

        public RelayCommand ConvertToExcelCommand { get; set; }
        public RelayCommand AddSemesterDisciplineCommand { get; set; }
        public RelayCommand EditSemesterDisciplineCommand { get; set; }
        public RelayCommand RemoveSemesterDisciplineCommand { get; set; }
        public RelayCommand PageBackCommand { get; set; }

        private void PageBack()
        {
            _navigationManager.Back();
        }

        private void ConvertDataToExcel()
        {
            Discipline[] disciplines = new EducationProcessContext().Disciplines.ToArray();

            ExcelEducationPlanWriter.ToExcelFile(disciplines, null);
            _dialogCoordinator.ShowMessageAsync(this, "Конвертация в таблицу", "Операция завершена успешо.");
        }

        private bool IsSelectedSemesterDiscipline(SemesterDiscipline semesterDiscipline)
        {
            if (semesterDiscipline == null)
            {
                _dialogCoordinator.ShowMessageAsync(this, "Внимание", "Выберите дисциплину");
                return false;
            }
            return true;
        }

        private void DeleteSemesterDiscipline(EducationPlan educationPlan, SemesterDiscipline semesterDiscipline)
        {
            if (IsSelectedSemesterDiscipline(semesterDiscipline))
            {
                EducationProcessContext context = new EducationProcessContext();
                EducationPlanSemesterDiscipline educationPlanSemesterDiscipline =
                    context.EducationPlanSemesterDisciplines
                        .First(x => x.EducationPlanId == educationPlan.EducationPlanId &&
                                    x.SemesterDisciplineId == semesterDiscipline.SemesterDisciplineId);

                context.EducationPlanSemesterDisciplines.Remove(educationPlanSemesterDiscipline);

                EducationPlanSemesterDiscipline? otherEducationPlanSemesterDiscipline =
                    context.EducationPlanSemesterDisciplines
                        .FirstOrDefault(x => x.SemesterDisciplineId == semesterDiscipline.SemesterDisciplineId
                                             && x.EducationPlanId != educationPlan.EducationPlanId);

                if (otherEducationPlanSemesterDiscipline == null)
                    context.EducationPlans.Remove(educationPlan);

                context.SaveChanges();
                RefreshSemesterDisciplines(educationPlan);
            }
        }

        private void AddSemesterDiscipline(EducationPlan educationPlan)
        {
            ShowSemesterDisciplineEditor(educationPlan, null);
            RefreshSemesterDisciplines(educationPlan);
        }

        private void EditSemesterDiscipline(EducationPlan educationPlan, SemesterDiscipline semesterDiscipline)
        {
            if (IsSelectedSemesterDiscipline(semesterDiscipline))
            {
                ShowSemesterDisciplineEditor(educationPlan, semesterDiscipline);
                RefreshSemesterDisciplines(educationPlan);
            }
        }

        private void ShowSemesterDisciplineEditor(EducationPlan educationPlan, SemesterDiscipline semesterDiscipline)
        {
            SemesterDisciplineEditViewModel viewModel = new SemesterDisciplineEditViewModel(educationPlan, semesterDiscipline);
            new SemesterDisciplineEditorWindow(viewModel).ShowDialog();
            RefreshSemesterDisciplines(educationPlan);
        }

        private void RefreshSemesterDisciplines(EducationPlan educationPlan)
        {
            SemesterDiscipline[] semesterDisciplines = new EducationProcessContext()
                .EducationPlanSemesterDisciplines
                .Where(x => x.EducationPlanId == educationPlan.EducationPlanId)
                .Include(x => x.SemesterDiscipline)
                .ThenInclude(x => x.Discipline)
                .Include(x => x.SemesterDiscipline)
                .ThenInclude(x => x.Semester)
                .Select(x => x.SemesterDiscipline)
                .ToArray();
            SemesterDisciplines = new ObservableCollection<SemesterDiscipline>(semesterDisciplines);
        }
    }
}
