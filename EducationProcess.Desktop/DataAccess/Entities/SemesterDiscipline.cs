using System;
using System.Collections.Generic;

#nullable disable

namespace EducationProcess.Desktop.DataAccess.Entities
{
    public partial class SemesterDiscipline
    {
        public int SemesterDisciplineId { get; set; }
        public int SemesterId { get; set; }
        public int DisciplineId { get; set; }
        public int EmployeeId { get; set; }
        public int GroupId { get; set; }
        public bool IsAccepted { get; set; }
        public string GroupName { get; set; }

        public virtual Discipline Discipline { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Group Group { get; set; }
        public virtual Semester Semester { get; set; }
    }
}
