using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EducationProcess.Desktop.DataAccess.Entities
{
    public partial class AcademicYears
    {
        public AcademicYears()
        {
            Semesters = new HashSet<Semesters>();
        }

        public int AcademicYearId { get; set; }
        public short BeginingYear { get; set; }
        public short EndingYear { get; set; }

        public virtual ICollection<Semesters> Semesters { get; set; }
    }
}
