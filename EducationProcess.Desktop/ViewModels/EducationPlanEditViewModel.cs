using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DevExpress.Mvvm;
using EducationProcess.Desktop.Core;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.DataAccess.Entities;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;

namespace EducationProcess.Desktop.ViewModels
{
    public class EducationPlanEditViewModel : BindableBase
    {
        private IDialogCoordinator _dialogCoordinator;
        private EducationPlan _educationPlan;

        public EducationPlanEditViewModel(EducationPlan educationPlan)
        {
            _dialogCoordinator = DialogCoordinator.Instance;
            EducationProcessContext context = new EducationProcessContext();
            _educationPlan = educationPlan;

            if (educationPlan != null)
                HeaderText = $"Редактирование уч. плана";
            else
            {
                HeaderText = $"Добавление уч. плана";
                IsEnabledCopyEducationPlan = true;
            }
               

            AcademicYears = new ObservableCollection<AcademicYear>(context.AcademicYears.ToArray());
            Specialties = new ObservableCollection<Specialty>(context.Specialties.ToArray());
            EducationPlans = new ObservableCollection<EducationPlan>(context.EducationPlans.ToArray());

            if (educationPlan != null)
            {
                TitleInfo = "Редактирование уч. плана";
                EducationPlanName = educationPlan.Name;
                SelectedSpecialty = Specialties.First(x => x.SpecialtieId == educationPlan.SpecialtieId);
                SelectedAcademicYear = AcademicYears.First(x => x.AcademicYearId == educationPlan.AcademicYearId);
            }
            else
            {
                TitleInfo = "Добавление уч. плана";
            }

            SaveCommand = new RelayCommand(null, _ => SaveEducationPlan(_educationPlan));
        }

        public string TitleInfo { get; set; }
        public string HeaderText { get; set; }
        public string EducationPlanName { get; set; }
        public bool IsEnabledCopyEducationPlan { get; set; }
        public ObservableCollection<AcademicYear> AcademicYears { get; set; }
        public ObservableCollection<Specialty> Specialties { get; set; }
        public ObservableCollection<EducationPlan> EducationPlans { get; set; }
        public AcademicYear SelectedAcademicYear { get; set; }
        public Specialty SelectedSpecialty { get; set; }
        public EducationPlan SelectedEducationPlan { get; set; }

        public RelayCommand SaveCommand { get; set; }

        private void SaveEducationPlan(EducationPlan educationPlan)
        {
            if (SelectedAcademicYear == null)
            {
                _dialogCoordinator.ShowMessageAsync(this, null, "Выберите учебный год");
                return;
            }
            if (SelectedSpecialty == null)
            {
                _dialogCoordinator.ShowMessageAsync(this, null, "Выберите специальность");
                return;
            }

            if (string.IsNullOrWhiteSpace(EducationPlanName))
            {
                _dialogCoordinator.ShowMessageAsync(this, null, "Назовите учебный план");
                return;
            }

            EducationProcessContext context = new EducationProcessContext();
            if (educationPlan == null)
            {
                educationPlan = new EducationPlan()
                {
                    Name = EducationPlanName,
                    AcademicYearId = SelectedAcademicYear.AcademicYearId,
                    SpecialtieId = SelectedSpecialty.SpecialtieId
                };
                context.EducationPlans.Add(educationPlan);
            }
            else
            {
                educationPlan.Name = EducationPlanName;
                educationPlan.AcademicYearId = SelectedAcademicYear.AcademicYearId;
                educationPlan.SpecialtieId = SelectedSpecialty.SpecialtieId;
                context.EducationPlans.Update(educationPlan);
            }
            context.SaveChanges();

            if (SelectedEducationPlan != null)
            {
                CopySemesterDisciplines(educationPlan, SelectedEducationPlan);
            }

            _dialogCoordinator.ShowMessageAsync(this, "Сохранено", null);
        }


        private void CopySemesterDisciplines(EducationPlan educationPlan, EducationPlan copyingEducationPlan)
        {
            EducationProcessContext context = new EducationProcessContext();
            EducationPlanSemesterDiscipline[] copyingSemesterDisciplines = context.EducationPlanSemesterDisciplines
                .Where(x => x.EducationPlanId == copyingEducationPlan.EducationPlanId)
                .AsNoTracking()
                .ToArray();
            foreach (var copyingSemesterDiscipline in copyingSemesterDisciplines)
            {
                EducationPlanSemesterDiscipline educationPlanSemesterDiscipline =
                    new EducationPlanSemesterDiscipline()
                    {
                        EducationPlanId = educationPlan.EducationPlanId,
                        SemesterDisciplineId = copyingSemesterDiscipline.SemesterDisciplineId
                    };
                context.EducationPlanSemesterDisciplines.Add(educationPlanSemesterDiscipline);
            }
            context.SaveChanges();
        }
    }
}