﻿using System;
using System.Collections.Generic;

#nullable disable

namespace EducationProcess.Desktop.DataAccess.Entities
{
    public partial class IntermediateCertificationForm
    {
        public IntermediateCertificationForm()
        {
            SemesterDisciplines = new HashSet<SemesterDiscipline>();
        }

        public int CertificationFormId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SemesterDiscipline> SemesterDisciplines { get; set; }
    }
}
