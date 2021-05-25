using System;
using System.Collections.Generic;

#nullable disable

namespace EducationProcess.Desktop.DataAccess.Entities
{
    public partial class CathedraSpecialty
    {
        public int CathedraId { get; set; }
        public int SpecialtieId { get; set; }

        public virtual Cathedra Cathedra { get; set; }
        public virtual Specialty Specialtie { get; set; }
    }
}
