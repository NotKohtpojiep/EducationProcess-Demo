using System;
using System.Collections.Generic;

#nullable disable

namespace EducationProcess.Desktop.DataAccess.Entities
{
    public partial class Semester
    {
        public Semester()
        {
            SemesterDisciplines = new HashSet<SemesterDiscipline>();
        }

        public int SemesterId { get; set; }
        public int Number { get; set; }
        public int AcademicYearId { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }
        public virtual ICollection<SemesterDiscipline> SemesterDisciplines { get; set; }
    }
}
