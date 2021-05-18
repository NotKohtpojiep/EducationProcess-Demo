using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EducationProcess.Desktop.DataAccess.Entities
{
    public partial class Semesters
    {
        public Semesters()
        {
            SemesterDisciplines = new HashSet<SemesterDisciplines>();
        }

        public int SemesterId { get; set; }
        public int Number { get; set; }
        public int AcademicYearId { get; set; }

        public virtual AcademicYears AcademicYear { get; set; }
        public virtual ICollection<SemesterDisciplines> SemesterDisciplines { get; set; }
    }
}
