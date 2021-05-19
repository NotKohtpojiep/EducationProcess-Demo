using System;
using System.Collections.Generic;

#nullable disable

namespace EducationProcess.Desktop.DataAccess.Entities
{
    public partial class Specialty
    {
        public Specialty()
        {
            Groups = new HashSet<Group>();
        }

        public int SpecialtieId { get; set; }
        public int CathedraId { get; set; }
        public string SpecialtieIndex { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }

        public virtual Cathedra Cathedra { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }
}
