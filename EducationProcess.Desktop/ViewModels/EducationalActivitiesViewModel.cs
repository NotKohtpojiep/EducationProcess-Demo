using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Mvvm;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.DataAccess.Entities;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;

using Range = System.Range;

namespace EducationProcess.Desktop.ViewModels
{
    public class EducationalActivitiesViewModel : BindableBase
    {
        private readonly IDialogCoordinator _dialogCoordinator;
        public ObservableCollection<SemesterDisciplines> SemesterDisciplines { get; set; }
        public EducationalActivitiesViewModel(IDialogCoordinator dialogCoordinator)
        {
            _dialogCoordinator = dialogCoordinator;
            SemesterDisciplines[] disciplines = new EducationProcessContext().SemesterDisciplines
                .Where(x => x.Semester.Number == 1)
                .Include(x => x.Group)
                .Include(x => x.Discipline)
                .Include(x => x.Employee)
                .ToArray();
            SemesterDisciplines = new ObservableCollection<SemesterDisciplines>(disciplines);
        }

    }
}
