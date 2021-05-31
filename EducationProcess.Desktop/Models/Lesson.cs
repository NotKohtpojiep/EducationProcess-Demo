using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EducationProcess.Desktop.Core;
using EducationProcess.Desktop.DataAccess.Entities;
using EducationProcess.Desktop.ViewModels;

namespace EducationProcess.Desktop.Models
{
    public class Lesson
    {
        private List<LessonItem> _pairOptions;
        private Discipline[] _disciplines;
        public int PairNumber { get; set; }
        public ObservableCollection<LessonItem> PairOptions { get; set; }
        public bool IsNotWhole { get; set; }
        public Lesson(Discipline[] disciplines, int pairNumber)
        {
            _disciplines = disciplines;
            _pairOptions = new List<LessonItem>() { new LessonItem(disciplines, "") };
            PairOptions = new ObservableCollection<LessonItem>(_pairOptions);
            PairNumber = pairNumber;
            ChangeCountLessonOptionsCommand = new RelayCommand(null, _ => ChangePairOptions());
        }

        public RelayCommand ChangeCountLessonOptionsCommand { get; set; }

        private void ChangePairOptions()
        {
            if (IsNotWhole)
            {
                PairOptions[0] = new LessonItem(_disciplines, "Числитель");
                PairOptions.Add(new LessonItem(_disciplines, "Знаменатель"));
            }
            else
            {
                PairOptions.RemoveAt(PairOptions.Count - 1);
                PairOptions[0] = new LessonItem(_disciplines, "Обычный");
            }
        }
    }
}
