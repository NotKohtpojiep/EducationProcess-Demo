using System;
using System.Collections.Generic;

#nullable disable

namespace EducationProcess.Desktop.DataAccess.Entities
{
    public partial class Employee
    {
        public Employee()
        {
            Accounts = new HashSet<Account>();
            Groups = new HashSet<Group>();
            SemesterDisciplines = new HashSet<SemesterDiscipline>();
        }

        public int EmployeeId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Middlename { get; set; }
        public int PostId { get; set; }

        public virtual Post Post { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<SemesterDiscipline> SemesterDisciplines { get; set; }
    }
}
