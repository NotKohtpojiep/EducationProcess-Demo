using System;
using System.Collections.Generic;

#nullable disable

namespace EducationProcess.Desktop.DataAccess.Entities
{
    public partial class DisciplineGroup
    {
        public DisciplineGroup()
        {
            Disciplines = new HashSet<Discipline>();
            InverseDisciplineGroupHead = new HashSet<DisciplineGroup>();
        }

        public int DisciplineGroupId { get; set; }
        public string Name { get; set; }
        public int? DisciplineGroupHeadId { get; set; }

        public virtual DisciplineGroup DisciplineGroupHead { get; set; }
        public virtual ICollection<Discipline> Disciplines { get; set; }
        public virtual ICollection<DisciplineGroup> InverseDisciplineGroupHead { get; set; }
    }
}
