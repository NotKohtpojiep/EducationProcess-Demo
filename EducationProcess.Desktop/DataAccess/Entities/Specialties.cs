using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EducationProcess.Desktop.DataAccess.Entities
{
    public partial class Specialties
    {
        public Specialties()
        {
            Groups = new HashSet<Groups>();
        }

        public int SpecialtieId { get; set; }
        public int CathedraId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }

        public virtual Cathedras Cathedra { get; set; }
        public virtual ICollection<Groups> Groups { get; set; }
    }
}
