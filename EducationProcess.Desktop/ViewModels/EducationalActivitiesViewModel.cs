using System.Collections.Generic;
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
    public class SemestreInfo
    {
        public string DisciplineName { get; set; }
        public string GroupName { get; set; }
        public short LectionLessonHours { get; set; }
        public short PracticalLessonHours { get; set; }
        public short LaboratoryLessonHours { get; set; }
        public short ConsultationHours { get; set; }
        public short TestHours { get; set; }
        public short ExamHours { get; set; }
        public short CourseworkProjectHours { get; set; }
        public short DiplomaProjectHours { get; set; }
        public short SecHours { get; set; }
        public short PracticeHeadHours { get; set; }
        public short ControlWorkVerificationHours { get; set; }
        public string Note { get; set; }

        public SemestreInfo(Discipline discipline, string groupName)
        {
            DisciplineName = discipline.Name;
            GroupName = groupName;
            LectionLessonHours = discipline.LectionLessonHours;
            PracticalLessonHours = discipline.PracticalLessonHours;
            LaboratoryLessonHours = discipline.LaboratoryLessonHours;
            ConsultationHours = discipline.ConsultationHours;
            TestHours = discipline.TestHours;
            ExamHours = discipline.ExamHours;
            CourseworkProjectHours = discipline.CourseworkProjectHours;
            DiplomaProjectHours = discipline.DiplomaProjectHours;
            SecHours = discipline.SecHours;
            PracticeHeadHours = discipline.PracticeHeadHours;
            ControlWorkVerificationHours = discipline.ControlWorkVerificationHours;
            Note = discipline.Note;
        }
    }

    public class EducationalActivitiesViewModel : BindableBase
    {
        private readonly IDialogCoordinator _dialogCoordinator;
        public ObservableCollection<SemestreInfo> SemesterDisciplines { get; set; }
        public ObservableCollection<SemestreInfo> SemesterResult { get; set; }
        public ObservableCollection<string> AcademicYears { get; set; }

        public EducationalActivitiesViewModel(IDialogCoordinator dialogCoordinator)
        {
            _dialogCoordinator = dialogCoordinator;
            SemesterDiscipline[] disciplines = new EducationProcessContext().SemesterDisciplines
                .Where(x => x.Semester.Number == 1)
                .Include(x => x.Group)
                .Include(x => x.Discipline)
                .Include(x => x.Employee)
                .ToArray();
            List<SemestreInfo> semestreInfos = new List<SemestreInfo>();
            foreach (var discipline in disciplines)
            {
                semestreInfos.Add(new SemestreInfo(discipline.Discipline, discipline.GroupName));
            }

            Discipline resultDiscipline = new Discipline() { Name = "Итого: " };
            foreach (var semestreInfo in semestreInfos)
            {
                resultDiscipline.ConsultationHours += semestreInfo.ConsultationHours;
                resultDiscipline.ControlWorkVerificationHours += semestreInfo.ControlWorkVerificationHours;
                resultDiscipline.CourseworkProjectHours += semestreInfo.CourseworkProjectHours;
                resultDiscipline.DiplomaProjectHours += semestreInfo.DiplomaProjectHours;
                resultDiscipline.ExamHours += semestreInfo.ExamHours;
                resultDiscipline.LaboratoryLessonHours += semestreInfo.LaboratoryLessonHours;
                resultDiscipline.LectionLessonHours += semestreInfo.LectionLessonHours;
                resultDiscipline.PracticalLessonHours += semestreInfo.PracticalLessonHours;
                resultDiscipline.PracticeHeadHours += semestreInfo.PracticeHeadHours;
                resultDiscipline.SecHours += semestreInfo.SecHours;
                resultDiscipline.TestHours += semestreInfo.TestHours;
            }

            semestreInfos.Add((new SemestreInfo(resultDiscipline, null)));
            string[] academicYears = new string[] { "2020-2021", "2019-2020", "2018-2019" };

            SemesterDisciplines = new ObservableCollection<SemestreInfo>(semestreInfos);
            AcademicYears = new ObservableCollection<string>(academicYears);
            ConvertToExcelCommand = new RelayCommand(null, _ => ConvertDataToExcel());

        }

        public RelayCommand ConvertToExcelCommand { get; set; }

        private void ConvertDataToExcel()
        {
            Discipline[] disciplines = new EducationProcessContext().Disciplines.ToArray();

            ExcelEducationPlanWriter.ToExcelFile(disciplines, null);
            _dialogCoordinator.ShowMessageAsync(this, "Конвертация в таблицу", "Операция завершена успешо.");
        }
    }
}