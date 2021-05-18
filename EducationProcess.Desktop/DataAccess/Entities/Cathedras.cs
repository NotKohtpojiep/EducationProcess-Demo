using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EducationProcess.Desktop.DataAccess.Entities
{
    public partial class Cathedras
    {
        public Cathedras()
        {
            Specialties = new HashSet<Specialties>();
        }

        public int CathedraId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }

        public virtual ICollection<Specialties> Specialties { get; set; }
    }
}
