using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using EducationProcess.Desktop.Core;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.DataAccess.Entities;
using EducationProcess.Desktop.Windows;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;

namespace EducationProcess.Desktop.ViewModels
{
    public class EducationPlanGroupsMenuViewModel : BindableBase
    {
        private readonly INavigationManager _navigationManager;
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly EducationPlan _educationPlan;
        private readonly EducationProcessContext _context;

        public EducationPlanGroupsMenuViewModel(INavigationManager navigationManager, EducationPlan educationPlan)
        {
            _navigationManager = navigationManager;
            _educationPlan = educationPlan;
            _dialogCoordinator = DialogCoordinator.Instance;
            _context = new EducationProcessContext();

            PageBackCommand = new RelayCommand(null, _ => _navigationManager.Back());
            ChainGroupCommand = new RelayCommand(null, _ => ChainGroup());
            UnchainGroupCommand = new RelayCommand(null, _ => UnchainGroup(SelectedGroup));

            Group[] groups = GetGroupsByEducationPlanId(educationPlan.EducationPlanId);
            Groups = new ObservableCollection<Group>(groups);
        }

        public ObservableCollection<Group> Groups { get; set; }
        public Group SelectedGroup { get; set; }

        public RelayCommand PageBackCommand { get; set; }
        public RelayCommand ChainGroupCommand { get; set; }
        public RelayCommand UnchainGroupCommand { get; set; }

        private void ChainGroup()
        {
            AddGroupToEducationPlanViewModel viewModel = new AddGroupToEducationPlanViewModel(_educationPlan);
            AddGroupToEducationPlanWindow window = new AddGroupToEducationPlanWindow(viewModel);
            window.ShowDialog();
            Groups = new ObservableCollection<Group>(GetGroupsByEducationPlanId(_educationPlan.EducationPlanId));
        }

        private void UnchainGroup(Group group)
        {
            if (group == null)
            {
                _dialogCoordinator.ShowMessageAsync(this, "Внимание", "Выберите группу");
                return;
            }

            MessageDialogResult dialogResult = _dialogCoordinator.ShowModalMessageExternal(this, "Подтверждение",
                "Вы действительно хотите открепить данную группу из учебного плана?", MessageDialogStyle.AffirmativeAndNegative);
            if (dialogResult == MessageDialogResult.Affirmative)
            {
                UnchainGroupFromEducationPlan(group);
                Groups = new ObservableCollection<Group>(GetGroupsByEducationPlanId(_educationPlan.EducationPlanId));
            }
        }

        private Group[] GetGroupsByEducationPlanId(int educationPlanId)
        {
            Group[] groups = _context.Groups
                .Where(x => x.EducationPlanId == educationPlanId)
                .Include(x => x.ReceivedEducation)
                .ThenInclude(x => x.ReceivedSpecialty)
                .ToArray();
            return groups;
        }

        private void UnchainGroupFromEducationPlan(Group group)
        {
            group.EducationPlanId = null;
            _context.SaveChanges();
        }
    }
}