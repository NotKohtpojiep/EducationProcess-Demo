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
using EducationProcess.Desktop.Windows;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;

namespace EducationProcess.Desktop.ViewModels
{
    public class EducationPlansMenuViewModel : BindableBase
    {
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly INavigationManager _navigationManager;

        public EducationPlan SelectedEducationPlan { get; set; }
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

            ViewEducationPlanCommand = new RelayCommand(null, _ =>
            {
                if (IsSelectedEducationPlan(SelectedEducationPlan))
                    Navigate(new EducationPlanDisciplinesMenuViewModel(_navigationManager, SelectedEducationPlan));
            });
            AddEducationPlanCommand = new RelayCommand(null, _ => ShowEducationPlanEditor(null));
            EditEducationPlanCommand = new RelayCommand(null, _ => EditEducationPlan(SelectedEducationPlan));
            DeleteEducationPlanCommand = new RelayCommand(null, _ => DeleteEducationPlan(SelectedEducationPlan));
            AttachGroupToEducationPlanCommand = new RelayCommand(null, _ =>
            {
                if (IsSelectedEducationPlan(SelectedEducationPlan))
                    Navigate(new EducationPlanGroupsMenuViewModel(_navigationManager, SelectedEducationPlan));
            });
        }

        public RelayCommand ViewEducationPlanCommand { get; set; }
        public RelayCommand AddEducationPlanCommand { get; set; }
        public RelayCommand EditEducationPlanCommand { get; set; }
        public RelayCommand DeleteEducationPlanCommand { get; set; }
        public RelayCommand AttachGroupToEducationPlanCommand { get; set; }

        private bool IsSelectedEducationPlan(EducationPlan educationPlan)
        {
            if (educationPlan == null)
            {
                _dialogCoordinator.ShowMessageAsync(this, "Внимание", "Выберите учебный план");
                return false;
            }
            return true;
        }

        private void Navigate(BindableBase viewModel)
        {
            _navigationManager.Next(viewModel);
        }

        private void DeleteEducationPlan(EducationPlan educationPlan)
        {
            if (IsSelectedEducationPlan(educationPlan))
            {
                EducationProcessContext context = new EducationProcessContext();
                if (context.Groups.FirstOrDefault(x => x.EducationPlanId == educationPlan.EducationPlanId) != null)
                {
                    _dialogCoordinator.ShowMessageAsync(this, "Ошибка", "Данный учебный план закреплен за группой");
                    return;
                }
                RefreshEducationPlans();
            }
        }

        private void EditEducationPlan(EducationPlan educationPlan)
        {
            if (IsSelectedEducationPlan(educationPlan))
            {
                ShowEducationPlanEditor(educationPlan);
            }
        }

        private void ShowEducationPlanEditor(EducationPlan educationPlan)
        {
            EducationPlanEditViewModel viewModel = new EducationPlanEditViewModel(educationPlan);
            new EducationPlanEditorWindow(viewModel).ShowDialog();
            RefreshEducationPlans();
        }

        private void RefreshEducationPlans()
        {
            EducationPlan[] educationPlans = new EducationProcessContext()
                .EducationPlans
                .Include(x => x.Specialtie)
                .Include(x => x.AcademicYear)
                .ToArray();;
            EducationPlans = new ObservableCollection<EducationPlan>(educationPlans);
        }
    }
}