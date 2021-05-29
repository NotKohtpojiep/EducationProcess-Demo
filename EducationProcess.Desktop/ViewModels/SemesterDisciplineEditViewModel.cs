using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Input;
using DevExpress.Mvvm;
using EducationProcess.Desktop.Core;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.DataAccess.Entities;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;

namespace EducationProcess.Desktop.ViewModels
{
    public class SemesterDisciplineEditViewModel : BindableBase
    {
        public string TitleInfo { get; set; }
        public string HeaderText { get; set; }
        public bool IsActiveDeleteButton { get; set; }
        public SemesterDiscipline SemesterDiscipline { get; set; }
        public ObservableCollection<Discipline> Disciplines { get; set; }
        public ObservableCollection<Semester> Semesters { get; set; }
        public ObservableCollection<IntermediateCertificationForm> CertificationForms { get; set; }
        private IDialogCoordinator _dialogCoordinator;

        public ICommand CancelCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public Discipline SelectedDiscipline { get; set; }
        public Semester SelectedSemester { get; set; }
        public IntermediateCertificationForm SelectedCertificationForm { get; set; }

        public SemesterDisciplineEditViewModel(IDialogCoordinator dialogCoordinator, EducationPlan educationPlan, SemesterDiscipline? semesterDiscipline = null)
        {
            _dialogCoordinator = dialogCoordinator;
            EducationProcessContext context = new EducationProcessContext();

            string specialtieName = context.Specialties.First(x => x.SpecialtieId == educationPlan.SpecialtieId)
                .ImplementedSpecialtyName;
            HeaderText = $"Дисциплина специальности:\n{specialtieName}";

            Cathedra[] diciplineCathedras = context.CathedraSpecialties
                .Where(x => x.SpecialtieId == educationPlan.SpecialtieId)
                .Include(x => x.Cathedra)
                .Select(x => x.Cathedra)
                .ToArray();
            List<Discipline> disciplines = new List<Discipline>();
            foreach (var diciplineCathedra in diciplineCathedras)
            {
                disciplines.AddRange(context.Disciplines
                    .Where(x => x.CathedraId == diciplineCathedra.CathedraId)
                    .ToArray());
            }

            List<IntermediateCertificationForm> certificationForms =
                new EducationProcessContext().IntermediateCertificationForms.ToList();
            certificationForms.Insert(0, new IntermediateCertificationForm() { Name = "Нет" });

            CertificationForms = new ObservableCollection<IntermediateCertificationForm>(certificationForms);
            Semesters = new ObservableCollection<Semester>(context.Semesters.ToArray());
            Disciplines = new ObservableCollection<Discipline>(disciplines);

            if (semesterDiscipline != null)
            {
                TitleInfo = "Редактирование дисциплины";
                SemesterDiscipline = semesterDiscipline;
                IsActiveDeleteButton = true;
                SelectedDiscipline = Disciplines.First(x => x.DisciplineId == semesterDiscipline.DisciplineId);
                SelectedSemester = Semesters.First(x => x.SemesterId == semesterDiscipline.SemesterId);
                SelectedCertificationForm =
                    CertificationForms.First(x => x.CertificationFormId == semesterDiscipline.CertificationFormId);
            }
            else
            {
                TitleInfo = "Добавление дисциплины";
                SemesterDiscipline = new SemesterDiscipline();
            }

            CancelCommand = new RelayCommand(null,
                _ => { _dialogCoordinator.ShowMessageAsync(this, "ХЕЙ!", "Ты действительно хочешь шекели?"); });
            SaveCommand = new RelayCommand(null,
                _ => { _dialogCoordinator.ShowMessageAsync(this, "ООВАОАОАОАООО", "Стоять нахуй, ты арестован"); });
            DeleteCommand = new RelayCommand(null,
                _ => { _dialogCoordinator.ShowMessageAsync(this, "ХЕЙ!", "Ты чооооооооо, ты чо??"); });
        }
    }
}