using System;
using System.Collections.Generic;

#nullable disable

namespace EducationProcess.Desktop.DataAccess.Entities
{
    public partial class ReceivedSpecialty
    {
        public ReceivedSpecialty()
        {
            ReceivedEducations = new HashSet<ReceivedEducation>();
        }

        public int ReceivedSpecialtyId { get; set; }
        public int SpecialtieId { get; set; }
        public string Qualification { get; set; }

        public virtual Specialty Specialtie { get; set; }
        public virtual ICollection<ReceivedEducation> ReceivedEducations { get; set; }
    }
}
