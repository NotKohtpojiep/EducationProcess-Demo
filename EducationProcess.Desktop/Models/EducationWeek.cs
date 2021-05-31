using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationProcess.Desktop.Models
{
    public class EducationWeek
    {
        public string Name { get; set; }
        public DateTime StartDay { get; set; }

        public EducationWeek(DateTime start, string name)
        {
            StartDay = start;
            Name = name;
        }

        public string WeekInfo
        {
            get
            {
                int firstDayOfYear = (int)new DateTime(StartDay.Year, 1, 1).DayOfWeek;
                int weekNumber = (StartDay.DayOfYear + firstDayOfYear) / 7;
                return $"({weekNumber}) {StartDay.ToShortDateString()} - {StartDay.AddDays(6).ToShortDateString()}";
            }
        }
    }
}
