using System;
using System.Collections.Generic;

#nullable disable

namespace EducationProcess.Desktop.DataAccess.Entities
{
    public partial class ConductedPair
    {
        public int ConductedPairId { get; set; }
        public int? ScheduleDisciplineId { get; set; }
        public int? ScheduleDisciplineReplacementId { get; set; }
        public int LessonType { get; set; }

        public virtual LessonType LessonTypeNavigation { get; set; }
        public virtual ScheduleDiscipline ScheduleDiscipline { get; set; }
        public virtual ScheduleDisciplineReplacement ScheduleDisciplineReplacement { get; set; }
    }
}
