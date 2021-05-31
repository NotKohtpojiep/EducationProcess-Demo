using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EducationProcess.Desktop.DataAccess.Entities;

namespace EducationProcess.Desktop.Models
{
    public class CourseGroups
    {
        public int Course { get; set; }
        public string CourseInfo
        {
            get => $"Курс: {Course}";
        }
        public Group[] CourseGroupsCollection { get; set; }
        public CourseGroups(Group[] courseGroups, int semestreNumber)
        {
            Course = semestreNumber;
            CourseGroupsCollection = courseGroups;
        }
    }
}
