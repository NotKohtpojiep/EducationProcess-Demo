﻿using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EducationProcess.Desktop.DataAccess.Entities
{
    public partial class Posts
    {
        public Posts()
        {
            Employees = new HashSet<Employees>();
        }

        public int PostId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }
    }
}