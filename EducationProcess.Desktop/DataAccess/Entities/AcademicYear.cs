using System;
using System.Collections.Generic;

#nullable disable

namespace EducationProcess.Desktop.DataAccess.Entities
{
    public partial class AcademicYear
    {
        public AcademicYear()
        {
            Semesters = new HashSet<Semester>();
        }

        public int AcademicYearId { get; set; }
        public short BeginingYear { get; set; }
        public short EndingYear { get; set; }

        public virtual ICollection<Semester> Semesters { get; set; }
    }
}
