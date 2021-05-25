using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using DevExpress.Mvvm;
using EducationProcess.Desktop.Core;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.DataAccess.Entities;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;

namespace EducationProcess.Desktop.ViewModels
{
    public class EducationPlansMenuViewModel : BindableBase
    {
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly INavigationManager _navigationManager;

        public ObservableCollection<EducationPlan> EducationPlans { get; set; }

        public EducationPlansMenuViewModel(INavigationManager navigationManager)
        {
            _dialogCoordinator = DialogCoordinator.Instance;
            _navigationManager = navigationManager;

            EducationPlan[] educationPlans = new EducationProcessContext().EducationPlans
                .Include(x => x.Specialtie)
                .Include(x => x.AcademicYear)
                .ToArray();
            EducationPlans = new ObservableCollection<EducationPlan>(educationPlans);

            ViewEducationPlanCommand = new RelayCommand(null, _ => { ViewEducationPlan(); });
        }

        public RelayCommand ViewEducationPlanCommand { get; set; }

        private void ViewEducationPlan()
        {
            _navigationManager.Next(new EducationPlanDisciplinesMenuViewModel(_navigationManager));
        }
    }
}
