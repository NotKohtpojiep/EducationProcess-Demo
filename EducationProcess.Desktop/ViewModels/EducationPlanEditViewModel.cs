using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DevExpress.Mvvm;
using EducationProcess.Desktop.Core;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.DataAccess.Entities;
using MahApps.Metro.Controls.Dialogs;

namespace EducationProcess.Desktop.ViewModels
{
    public class EducationPlanEditViewModel : BindableBase
    {
        public string TitleInfo { get; set; }
        public string HeaderText { get; set; }
        public string EducationPlanName { get; set; }
        public ObservableCollection<AcademicYear> AcademicYears { get; set; }
        public ObservableCollection<Specialty> Specialties { get; set; }
        private IDialogCoordinator _dialogCoordinator;

        public ICommand CancelCommand { get; set; }
        public ICommand SaveCommand { get; set; }


        public AcademicYear SelectedAcademicYear { get; set; }
        public Specialty SelectedSpecialty { get; set; }

        public EducationPlanEditViewModel(EducationPlan educationPlan)
        {
            _dialogCoordinator = DialogCoordinator.Instance;
            EducationProcessContext context = new EducationProcessContext();

            if (educationPlan != null)
                HeaderText = $"Редактирование уч. плана";
            else
                HeaderText = $"Добавление уч. плана";

            AcademicYears = new ObservableCollection<AcademicYear>(context.AcademicYears.ToArray());
            Specialties = new ObservableCollection<Specialty>(context.Specialties.ToArray());

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

            CancelCommand = new RelayCommand(null,
                _ => { _dialogCoordinator.ShowMessageAsync(this, "Отмена", "Вы действительно хотите выйти? Внесенные изменения не будут сохранены."); });
            SaveCommand = new RelayCommand(null,
                _ => { _dialogCoordinator.ShowMessageAsync(this, "Подтверждение", "Вы действительно хотите применить данные изменения?"); });
        }
    }
}
