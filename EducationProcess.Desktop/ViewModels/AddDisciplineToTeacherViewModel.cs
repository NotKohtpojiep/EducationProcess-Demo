using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using EducationProcess.Desktop.Core;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.DataAccess.Entities;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;

namespace EducationProcess.Desktop.ViewModels
{
    public class AddDisciplineToTeacherViewModel : BindableBase
    {
        private readonly IDialogCoordinator _dialogCoordinator;
        private SemesterDiscipline[] _semesterDisciplines;
        private Group _selectedGroup;
        private Discipline _selectedDiscipline;
        private Employee _selectedTeacher;

        public AddDisciplineToTeacherViewModel()
        {
            _dialogCoordinator = DialogCoordinator.Instance;
            EducationProcessContext context = new EducationProcessContext();

            Groups = new ObservableCollection<Group>(context.Groups
                .Where(x => x.EducationPlanId != null)
                .Include(x => x.EducationPlan)
                .ThenInclude(x => x.Specialtie).ToArray());

            SendSuggestionCommand = new RelayCommand(null, _ => SendSuggestion());
        }

        public ObservableCollection<Discipline> Disciplines { get; set; }
        public ObservableCollection<Employee> Teachers { get; set; }
        public ObservableCollection<Group> Groups { get; set; }

        public Discipline SelectedDiscipline
        {
            get => _selectedDiscipline;
            set
            {
                _selectedDiscipline = value;
                if (_selectedDiscipline != null)
                {
                    Employee[] employees = new EducationProcessContext().EmployeeCathedras
                        .Where(x => x.CathedraId == _selectedDiscipline.CathedraId)
                        .Include(x => x.Employee)
                        .Select(x => x.Employee)
                        .ToArray();
                    Teachers = new ObservableCollection<Employee>(employees);
                }
                SelectionsInfo = GetSelectionsInfo();
            }
        }

        public Employee SelectedTeacher
        {
            get => _selectedTeacher;
            set
            {
                _selectedTeacher = value;
                SelectionsInfo = GetSelectionsInfo();
            }
        }
        public Group SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                if (_selectedGroup != null)
                {
                    _semesterDisciplines = GetRelevantSemesterDisciplines(_selectedGroup);
                    Discipline[] disciplines = _semesterDisciplines.Select(x => x.Discipline).Distinct().ToArray();
                    Disciplines = new ObservableCollection<Discipline>(disciplines);
                }
                SelectionsInfo = GetSelectionsInfo();
            }
        }
        public string SelectionsInfo { get; set; }

        public RelayCommand SendSuggestionCommand { get; set; }

        private void SendSuggestion()
        {
            if (SelectedGroup == null)
            {
                _dialogCoordinator.ShowMessageAsync(this, null, "Выберите группу");
                return;
            }
            if (SelectedDiscipline == null)
            {
                _dialogCoordinator.ShowMessageAsync(this, null, "Выберите дисциплину");
                return;
            }
            if (SelectedTeacher == null)
            {
                _dialogCoordinator.ShowMessageAsync(this, null, "Выберите преподавателя");
                return;
            }

            SemesterDiscipline[] semesterDisciplines = _semesterDisciplines
                .Where(x => x.DisciplineId == SelectedDiscipline.DisciplineId).ToArray();
            List<FixedDiscipline> fixedDisciplines = new List<FixedDiscipline>();
            foreach (var semesterDiscipline in semesterDisciplines)
            {
                FixedDiscipline fixedDiscipline = new FixedDiscipline()
                {
                    EmployeeId = SelectedTeacher.EmployeeId,
                    GroupId = SelectedGroup.GroupId,
                    SemesterDisciplineId = semesterDiscipline.SemesterDisciplineId
                };
                fixedDisciplines.Add(fixedDiscipline);
            }

            EducationProcessContext context = new EducationProcessContext();
            context.FixedDisciplines.AddRange(fixedDisciplines);
            context.SaveChanges();
            _dialogCoordinator.ShowMessageAsync(this, null, "Предложение отправлено");
        }

        private string GetSelectionsInfo()
        {
            string info = string.Empty;
            if (_selectedGroup != null)
                info += $"Специальность: {_selectedGroup.EducationPlan.Specialtie.SpecialtieCode}\n";
            if (_selectedDiscipline != null)
                info += $"Семестр(ы): {string.Join(",", _semesterDisciplines.Where(x => x.DisciplineId == _selectedDiscipline.DisciplineId).Select(x => x.Semester.Number))}\n";
            if (_selectedTeacher != null)
                info += $"Кол-во часов на след. год: {GetTeacherAcademicHours(_selectedTeacher)}";
            return info;
        }

        private int GetTeacherAcademicHours(Employee employee)
        {
            FixedDiscipline[] fixedDisciplines = new EducationProcessContext()
                .FixedDisciplines
                .Include(x => x.Group)
                .Include(x => x.SemesterDiscipline)
                .ThenInclude(x => x.Semester)
                .Where(x => x.EmployeeId == employee.EmployeeId && x.IsAgreed == true)
                .ToArray();
            List<SemesterDiscipline> relevantSelectedDisciplines = new List<SemesterDiscipline>();
            foreach (var fixedDiscipline in fixedDisciplines)
            {
                int groupCource = GetCourceByDate(DateTime.Now, fixedDiscipline.Group.ReceiptYear) + 1;
                if (fixedDiscipline.SemesterDiscipline.Semester.Number == groupCource * 2 ||
                    fixedDiscipline.SemesterDiscipline.Semester.Number == groupCource * 2 - 1)
                    relevantSelectedDisciplines.Add(fixedDiscipline.SemesterDiscipline);
            }

            int hoursSum = relevantSelectedDisciplines.Sum(x => x.ConsultationHours +
                                                        x.EducationalPracticeHours +
                                                        x.ControlWorkHours +
                                                        x.ExamHours +
                                                        x.IndependentWorkHours +
                                                        x.LaboratoryWorkHours +
                                                        x.PracticeWorkHours +
                                                        x.ProductionPracticeHours +
                                                        x.TheoryLessonHours);
            return hoursSum;
        }

        private SemesterDiscipline[] GetRelevantSemesterDisciplines(Group group)
        {
            if (group == null)
                return new SemesterDiscipline[] { };

            int nextCource = GetCourceByDate(DateTime.Now, group.ReceiptYear) + 1;
            SemesterDiscipline[] semesterDisciplines = new EducationProcessContext()
                .EducationPlanSemesterDisciplines
                .Where(x => x.EducationPlanId == group.EducationPlanId &&
                            (x.SemesterDiscipline.Semester.Number == nextCource * 2 || x.SemesterDiscipline.Semester.Number == nextCource * 2 - 1))
                .Include(x => x.SemesterDiscipline)
                .ThenInclude(x => x.Discipline)
                .Include(x => x.SemesterDiscipline)
                .ThenInclude(x => x.Semester)
                .Select(x => x.SemesterDiscipline)
                .ToArray();
            return semesterDisciplines;
        }

        private int GetCourceByDate(DateTime currentDate, short receiptYear)
        {
            bool isFirstHalfYear = currentDate.Month / 7.0 < 1;
            return currentDate.Year - receiptYear + (isFirstHalfYear ? 0 : 1);
        }
    }
}