using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EducationProcess.Desktop.DataAccess.Entities
{
    public partial class Employees
    {
        public Employees()
        {
            Account = new HashSet<Account>();
            Groups = new HashSet<Groups>();
            SemesterDisciplines = new HashSet<SemesterDisciplines>();
        }

        public int EmployeeId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Middlename { get; set; }
        public int PostId { get; set; }

        public virtual Posts Post { get; set; }
        public virtual ICollection<Account> Account { get; set; }
        public virtual ICollection<Groups> Groups { get; set; }
        public virtual ICollection<SemesterDisciplines> SemesterDisciplines { get; set; }
    }
}
