using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EducationProcess.Desktop.DataAccess.Entities
{
    public partial class Groups
    {
        public Groups()
        {
            SemesterDisciplines = new HashSet<SemesterDisciplines>();
        }

        public int GroupId { get; set; }
        public string Name { get; set; }
        public byte CourseNumber { get; set; }
        public int CuratorId { get; set; }
        public int SpecialtieId { get; set; }
        public short ReceiptYear { get; set; }

        public virtual Employees Curator { get; set; }
        public virtual Specialties Specialtie { get; set; }
        public virtual ICollection<SemesterDisciplines> SemesterDisciplines { get; set; }
    }
}
