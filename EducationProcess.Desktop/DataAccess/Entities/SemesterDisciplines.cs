

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EducationProcess.Desktop.DataAccess.Entities
{
    public partial class SemesterDisciplines
    {
        public int SemesterDisciplineId { get; set; }
        public int SemesterId { get; set; }
        public int DisciplineId { get; set; }
        public int EmployeeId { get; set; }
        public int GroupId { get; set; }
        public bool IsAccepted { get; set; }
        public string GroupName { get; set; }

        public virtual Disciplines Discipline { get; set; }
        public virtual Employees Employee { get; set; }
        public virtual Groups Group { get; set; }
        public virtual Semesters Semester { get; set; }
    }
}
