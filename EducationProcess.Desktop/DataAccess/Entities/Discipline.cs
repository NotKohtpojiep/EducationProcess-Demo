using System;
using System.Collections.Generic;

#nullable disable

namespace EducationProcess.Desktop.DataAccess.Entities
{
    public partial class Discipline
    {
        public Discipline()
        {
            SemesterDisciplines = new HashSet<SemesterDiscipline>();
        }

        public int DisciplineId { get; set; }
        public string DisciplineIndex { get; set; }
        public string Name { get; set; }
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

        public virtual ICollection<SemesterDiscipline> SemesterDisciplines { get; set; }
    }
}
