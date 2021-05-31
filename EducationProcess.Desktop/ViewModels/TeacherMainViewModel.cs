using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.DataAccess.Entities;
using EducationProcess.Desktop.Helpers.Identity;
using MahApps.Metro.Controls.Dialogs;

namespace EducationProcess.Desktop.ViewModels
{
    public class TeacherMainViewModel : MainWindowViewModel
    {
        public TeacherMainViewModel(IDialogCoordinator dialogCoordinator) : base(dialogCoordinator)
        {
            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            if (customPrincipal == null)
                throw new ArgumentException("The application's default thread principal must be set to a CustomPrincipal object on startup.");
            int employeeId = customPrincipal.Identity.EmployeeId;

            Employee currentEmployee = new EducationProcessContext().Employees.First(x => x.EmployeeId == employeeId);

            ConfirmingLessonsCount = GetConfirmingLessonsCount(currentEmployee, DateTime.Now);
            SuggestionDisciplinesCount = new EducationProcessContext().FixedDisciplines
                .Count(x => x.EmployeeId == currentEmployee.EmployeeId && x.IsAgreed == null);
        }

        public int SuggestionDisciplinesCount { get; set; }
        public int ConfirmingLessonsCount { get; set; }

        private int GetConfirmingLessonsCount(Employee employee, DateTime date)
        {
            ScheduleDisciplineReplacement[] replacements = new EducationProcessContext()
                .ScheduleDisciplineReplacements
                .Where(x => x.FixedDiscipline.EmployeeId == employee.EmployeeId &&
                            x.Date == date.Date).ToArray();

            ScheduleDiscipline[] scheduleDisciplines = new EducationProcessContext()
                .ScheduleDisciplines
                .Where(x => x.FixedDiscipline.EmployeeId == employee.EmployeeId &&
                            x.Date == date.Date).ToArray();

            List<ConfirmingLesson> confirmingLessons = new List<ConfirmingLesson>();
            foreach (var replacement in replacements)
            {
                if (new EducationProcessContext().ConductedPairs.FirstOrDefault(x => x.ScheduleDisciplineReplacementId == replacement.ScheduleDisciplineReplacementId) == null)
                    confirmingLessons.Add(new ConfirmingLesson(replacement.ScheduleDisciplineReplacementId, true));
            }

            foreach (var scheduleDiscipline in scheduleDisciplines)
            {
                if (replacements.FirstOrDefault(x => x.ScheduleDisciplineId == scheduleDiscipline.ScheduleDisciplineId) == null &&
                    new EducationProcessContext().ConductedPairs.FirstOrDefault(x => x.ScheduleDisciplineId == scheduleDiscipline.ScheduleDisciplineId) == null)
                    confirmingLessons.Add(new ConfirmingLesson(scheduleDiscipline.ScheduleDisciplineId, false));
            }

            return confirmingLessons.Count();
        }
    }
}
