using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationProcess.Desktop.ViewModels
{
    public class DisciplineStatisticInfo
    {
        private readonly FixedDiscipline _fixedDiscipline;
        private readonly EducationProcessContext _context;

        public DisciplineStatisticInfo(FixedDiscipline fixedDiscipline)
        {
            _fixedDiscipline = fixedDiscipline;
            _context = new EducationProcessContext();

            Discipline = _context.Disciplines
                .First(x => x.DisciplineId == _fixedDiscipline.SemesterDiscipline.DisciplineId).Name;
            Employee employee = _context.Employees
                .First(x => x.EmployeeId == _fixedDiscipline.EmployeeId);
            Teacher = $"{employee.Lastname} {employee.Firstname.First()}.{employee.Firstname.First()}.";
        }
        public string Discipline { get; set; }
        public string Teacher { get; set; }

        public int TotalHours
        {
            get
            {
                return _fixedDiscipline.SemesterDiscipline.ControlWorkHours
                        + _fixedDiscipline.SemesterDiscipline.EducationalPracticeHours
                        + _fixedDiscipline.SemesterDiscipline.IndependentWorkHours
                        + _fixedDiscipline.SemesterDiscipline.LaboratoryWorkHours
                        + _fixedDiscipline.SemesterDiscipline.PracticeWorkHours
                        + _fixedDiscipline.SemesterDiscipline.ProductionPracticeHours
                        + _fixedDiscipline.SemesterDiscipline.TheoryLessonHours;
            }
        }

        public int ConductedPairs
        {
            get
            {
                return _context.ConductedPairs.Count(x => x.ScheduleDiscipline.FixedDisciplineId == _fixedDiscipline.FixedDisciplineId)
                       + _context.ConductedPairs.Count(x => x.ScheduleDiscipline.FixedDisciplineId == _fixedDiscipline.FixedDisciplineId);
            }
        }

        public int HoursPeerWeek
        {
            get
            {
                return TotalHours / 22;
            }
        }

        public double Percent
        {
            get
            {
                return (double)ConductedPairs / (double)TotalHours * 100;
            }
        }
    }
    public class DisciplineStatisticViewModel : BindableBase
    {
        private readonly Group _group;

        public DisciplineStatisticViewModel(Group group, int semesterNumber)
        {
            _group = group;
            FixedDiscipline[] fixedDisciplines = new EducationProcessContext()
                .FixedDisciplines
                .Where(x => x.SemesterDiscipline.Semester.Number == semesterNumber && x.GroupId == group.GroupId)
                .Include(x => x.SemesterDiscipline)
                .ToArray();
            GroupFixedDisciplines = new ObservableCollection<DisciplineStatisticInfo>();
            foreach (var fixedDiscipline in fixedDisciplines)
            {
                GroupFixedDisciplines.Add(new DisciplineStatisticInfo(fixedDiscipline));
            }

            HeaderText = $"Сводка дисциплин группы: {group.Name}";
        }

        public ObservableCollection<DisciplineStatisticInfo> GroupFixedDisciplines { get; set; }
        public string HeaderText { get; set; }
    }
}
