using System;
using System.Collections.Generic;

#nullable disable

namespace EducationProcess.Desktop.DataAccess.Entities
{
    public partial class Group
    {
        public Group()
        {
            SemesterDisciplines = new HashSet<SemesterDiscipline>();
        }

        public int GroupId { get; set; }
        public string Name { get; set; }
        public byte CourseNumber { get; set; }
        public int CuratorId { get; set; }
        public int SpecialtieId { get; set; }
        public short ReceiptYear { get; set; }
        public bool IsBudget { get; set; }

        public virtual Employee Curator { get; set; }
        public virtual Specialty Specialtie { get; set; }
        public virtual ICollection<SemesterDiscipline> SemesterDisciplines { get; set; }
    }
}
