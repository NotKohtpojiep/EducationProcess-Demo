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
            Audiences = new HashSet<Audience>();
            FixedDisciplines = new HashSet<FixedDiscipline>();
            Groups = new HashSet<Group>();
        }

        public int EmployeeId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Middlename { get; set; }
        public int PostId { get; set; }

        public virtual Post Post { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Audience> Audiences { get; set; }
        public virtual ICollection<FixedDiscipline> FixedDisciplines { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }
}
