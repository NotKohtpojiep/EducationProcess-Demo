using System;
using System.Collections.Generic;

#nullable disable

namespace EducationProcess.Desktop.DataAccess.Entities
{
    public partial class FixedDiscipline
    {
        public FixedDiscipline()
        {
            ScheduleDisciplineReplacements = new HashSet<ScheduleDisciplineReplacement>();
            ScheduleDisciplines = new HashSet<ScheduleDiscipline>();
        }

        public int FixedDisciplineId { get; set; }
        public int EmployeeId { get; set; }
        public int SemesterDisciplineId { get; set; }
        public int GroupId { get; set; }
        public bool? IsAgreed { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Group Group { get; set; }
        public virtual SemesterDiscipline SemesterDiscipline { get; set; }
        public virtual ICollection<ScheduleDisciplineReplacement> ScheduleDisciplineReplacements { get; set; }
        public virtual ICollection<ScheduleDiscipline> ScheduleDisciplines { get; set; }
    }
}
