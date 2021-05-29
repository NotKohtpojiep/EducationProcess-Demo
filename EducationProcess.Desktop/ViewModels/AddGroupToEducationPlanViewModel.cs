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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;

namespace EducationProcess.Desktop.ViewModels
{
    public class AddGroupToEducationPlanViewModel : BindableBase
    {
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly EducationPlan _educationPlan;
        private readonly EducationProcessContext _context;

        public AddGroupToEducationPlanViewModel(EducationPlan educationPlan)
        {
            _dialogCoordinator = DialogCoordinator.Instance;
            _educationPlan = educationPlan;
            _context = new EducationProcessContext();

            Groups = new ObservableCollection<Group>(GetRelevantGroupsByEducationPlan(educationPlan));
            ChainGroupCommand = new RelayCommand(null, _ => ChainGroup(SelectedGroup));
            CancelCommand = new RelayCommand(null, o => ((MetroWindow)o).Close());
        }

        public ObservableCollection<Group> Groups { get; set; }
        public Group SelectedGroup { get; set; }

        public RelayCommand ChainGroupCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        private void ChainGroup(Group group)
        {
            if (group == null)
            {
                _dialogCoordinator.ShowMessageAsync(this, "Внимание", "Выберите закрепляемую группу");
                return;
            }
            group.EducationPlanId = _educationPlan.EducationPlanId;
            _context.SaveChanges();
        }

        private Group[] GetRelevantGroupsByEducationPlan(EducationPlan educationPlan)
        {
            Group[] groups = _context.Groups
                .Where(x => x.ReceivedEducation.ReceivedSpecialty.SpecialtieId == educationPlan.SpecialtieId)
                .Include(x => x.ReceivedEducation)
                .ThenInclude(x => x.ReceivedSpecialty)
                .ToArray();
            return groups;
        }
    }
}
