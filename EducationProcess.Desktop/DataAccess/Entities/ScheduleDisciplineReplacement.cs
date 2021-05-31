using System;
using System.Collections.Generic;

#nullable disable

namespace EducationProcess.Desktop.DataAccess.Entities
{
    public partial class ScheduleDisciplineReplacement
    {
        public ScheduleDisciplineReplacement()
        {
            ConductedPairs = new HashSet<ConductedPair>();
        }

        public int ScheduleDisciplineReplacementId { get; set; }
        public int? ScheduleDisciplineId { get; set; }
        public int FixedDisciplineId { get; set; }
        public DateTime Date { get; set; }
        public int PairNumber { get; set; }
        public int? AudienceId { get; set; }
        public bool? IsFirstSubgroup { get; set; }

        public virtual Audience Audience { get; set; }
        public virtual FixedDiscipline FixedDiscipline { get; set; }
        public virtual ScheduleDiscipline ScheduleDiscipline { get; set; }
        public virtual ICollection<ConductedPair> ConductedPairs { get; set; }
    }
}
