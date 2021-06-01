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
        private IDialogCoordinator _dialogCoordinator;
        private readonly EducationPlan _educationPlan;
        private readonly SemesterDiscipline? _semesterDiscipline;
        private readonly EducationProcessContext _context;

        public SemesterDisciplineEditViewModel(EducationPlan educationPlan, SemesterDiscipline? semesterDiscipline = null)
        {
            _educationPlan = educationPlan;
            _semesterDiscipline = semesterDiscipline;
            _dialogCoordinator = DialogCoordinator.Instance;
            _context = new EducationProcessContext();

            string specialtieName = _context.Specialties.First(x => x.SpecialtieId == educationPlan.SpecialtieId)
                .ImplementedSpecialtyName;
            HeaderText = $"Дисциплина специальности:\n{specialtieName}";

            Cathedra[] diciplineCathedras = _context.CathedraSpecialties
                .Where(x => x.SpecialtieId == educationPlan.SpecialtieId)
                .Include(x => x.Cathedra)
                .Select(x => x.Cathedra)
                .ToArray();
            List<Discipline> disciplines = new List<Discipline>();
            foreach (var diciplineCathedra in diciplineCathedras)
            {
                disciplines.AddRange(_context.Disciplines
                    .Where(x => x.CathedraId == diciplineCathedra.CathedraId)
                    .ToArray());
            }

            List<IntermediateCertificationForm> certificationForms =
                new EducationProcessContext().IntermediateCertificationForms.ToList();
            certificationForms.Insert(0, new IntermediateCertificationForm() { Name = "Нет" });

            CertificationForms = new ObservableCollection<IntermediateCertificationForm>(certificationForms);
            Semesters = new ObservableCollection<Semester>(_context.Semesters.ToArray());
            Disciplines = new ObservableCollection<Discipline>(disciplines);

            if (semesterDiscipline != null)
            {
                TitleInfo = "Редактирование дисциплины";
                SemesterDiscipline = semesterDiscipline;
                IsActiveDeleteButton = true;
                SelectedDiscipline = Disciplines.First(x => x.DisciplineId == semesterDiscipline.DisciplineId);
                SelectedSemester = Semesters.First(x => x.SemesterId == semesterDiscipline.SemesterId);
                SelectedCertificationForm =
                    CertificationForms.FirstOrDefault(x => x.CertificationFormId == semesterDiscipline.CertificationFormId);
                TheoryLessonHours = semesterDiscipline.TheoryLessonHours;
                PracticeWorkHours = semesterDiscipline.PracticeWorkHours;
                LaboratoryWorkHours = semesterDiscipline.LaboratoryWorkHours;
                ControlWorkHours = semesterDiscipline.ControlWorkHours;
                IndependentWorkHours = semesterDiscipline.IndependentWorkHours;
                ConsultationHours = semesterDiscipline.ConsultationHours;
                ExamHours = semesterDiscipline.ExamHours;
                EducationalPracticeHours = semesterDiscipline.EducationalPracticeHours;
                ProductionPracticeHours = semesterDiscipline.ProductionPracticeHours;
            }
            else
            {
                TitleInfo = "Добавление дисциплины";
                SemesterDiscipline = new SemesterDiscipline();
            }

            SaveCommand = new RelayCommand(null,
                _ => SaveSemesterDiscipline(_educationPlan, _semesterDiscipline));
        }

        public string TitleInfo { get; set; }
        public string HeaderText { get; set; }
        public short TheoryLessonHours { get; set; }
        public short PracticeWorkHours { get; set; }
        public short LaboratoryWorkHours { get; set; }
        public short ControlWorkHours { get; set; }
        public short IndependentWorkHours { get; set; }
        public short ConsultationHours { get; set; }
        public short ExamHours { get; set; }
        public short EducationalPracticeHours { get; set; }
        public short ProductionPracticeHours { get; set; }
        public bool IsActiveDeleteButton { get; set; }
        public SemesterDiscipline SemesterDiscipline { get; set; }
        public Discipline SelectedDiscipline { get; set; }
        public Semester SelectedSemester { get; set; }
        public IntermediateCertificationForm SelectedCertificationForm { get; set; }
        public ObservableCollection<Discipline> Disciplines { get; set; }
        public ObservableCollection<Semester> Semesters { get; set; }
        public ObservableCollection<IntermediateCertificationForm> CertificationForms { get; set; }

        public RelayCommand SaveCommand { get; set; }

        private void ShowDialog(string title, string message)
        {
            _dialogCoordinator.ShowMessageAsync(this, title, message);
        }

        private void SaveSemesterDiscipline(EducationPlan educationPlan, SemesterDiscipline semesterDiscipline)
        {
            if (SelectedDiscipline == null)
            {
                ShowDialog("Ошиюка", "Выберите дисциплину");
                return;
            }

            if (SelectedSemester == null)
            {
                ShowDialog("Ошиюка", "Выберите семестр");
                return;
            }

            if (IsThereDisciplineOnSemester(_educationPlan, semesterDiscipline))
            {
                ShowDialog("Ошибка", "На данный семестр уже имеется такая дисциплина");
                return;
            }

            bool isNewSemesterDiscipline = semesterDiscipline == null;

            if (isNewSemesterDiscipline)
                semesterDiscipline = new SemesterDiscipline();
            else
                semesterDiscipline.SemesterId = SelectedSemester.SemesterId;

            semesterDiscipline.DisciplineId = SelectedDiscipline.DisciplineId;
            semesterDiscipline.CertificationFormId = SelectedCertificationForm?.CertificationFormId;
            semesterDiscipline.TheoryLessonHours = TheoryLessonHours;
            semesterDiscipline.PracticeWorkHours = PracticeWorkHours;
            semesterDiscipline.LaboratoryWorkHours = LaboratoryWorkHours;
            semesterDiscipline.ControlWorkHours = ControlWorkHours;
            semesterDiscipline.IndependentWorkHours = IndependentWorkHours;
            semesterDiscipline.ConsultationHours = ConsultationHours;
            semesterDiscipline.ExamHours = ExamHours;
            semesterDiscipline.EducationalPracticeHours = EducationalPracticeHours;
            semesterDiscipline.ProductionPracticeHours = ProductionPracticeHours;

            if (isNewSemesterDiscipline)
                AddNewSemesterDiscipline(educationPlan, semesterDiscipline);
            else
                UpdateSemesterDiscipline(educationPlan, semesterDiscipline);

            _context.SaveChanges();
        }

        private bool IsThereDisciplineOnSemester(EducationPlan educationPlan, SemesterDiscipline semesterDiscipline)
        {
            int semesterDisciplineId = semesterDiscipline != null ? semesterDiscipline.SemesterDisciplineId : 0;
            if (_context.EducationPlanSemesterDisciplines
                .FirstOrDefault(x => x.SemesterDiscipline.Semester.Number == SelectedSemester.Number &&
                                     x.EducationPlanId == educationPlan.EducationPlanId &&
                                     x.SemesterDiscipline.DisciplineId == SelectedDiscipline.DisciplineId &&
                                     x.SemesterDisciplineId != semesterDisciplineId) != null)
            {
                return true;
            }
            return false;
        }


        private void AddNewSemesterDiscipline(EducationPlan educationPlan, SemesterDiscipline semesterDiscipline)
        {
            EducationProcessContext context = new EducationProcessContext();

            context.SemesterDisciplines.Add(semesterDiscipline);
            context.SaveChanges();
            EducationPlanSemesterDiscipline educationPlanSemesterDiscipline = new EducationPlanSemesterDiscipline()
            {
                EducationPlanId = educationPlan.EducationPlanId,
                SemesterDisciplineId = semesterDiscipline.SemesterDisciplineId
            };
            context.EducationPlanSemesterDisciplines.Add(educationPlanSemesterDiscipline);
            context.SaveChanges();
        }

        private void UpdateSemesterDiscipline(EducationPlan educationPlan, SemesterDiscipline semesterDiscipline)
        {
            EducationProcessContext context = new EducationProcessContext();
            bool isChainedForOtherDisciplines =
                context.EducationPlanSemesterDisciplines.FirstOrDefault(x =>
                    x.EducationPlanId != educationPlan.EducationPlanId &&
                    x.SemesterDisciplineId == semesterDiscipline.SemesterDisciplineId) != null;

            if (isChainedForOtherDisciplines)
            {
                AddNewSemesterDiscipline(educationPlan, semesterDiscipline);
                return;
            }
            context.SemesterDisciplines.Update(semesterDiscipline);
            context.SaveChanges();
        }
    }
}