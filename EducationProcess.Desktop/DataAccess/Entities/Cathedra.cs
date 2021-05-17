using System;
using System.Collections.Generic;

#nullable disable

namespace EducationProcess.Desktop.DataAccess.Entities
{
    public partial class Cathedra
    {
        public Cathedra()
        {
            Specialties = new HashSet<Specialty>();
        }

        public int CathedraId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }

        public virtual ICollection<Specialty> Specialties { get; set; }
    }
}
