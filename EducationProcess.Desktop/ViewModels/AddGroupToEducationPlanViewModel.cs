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
        private readonly EducationProcessContext _context;
        private readonly EducationPlan _educationPlan;

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

        public bool ChainGroup(Group group)
        {
            if (group == null)
            {
                _dialogCoordinator.ShowMessageAsync(this, "Внимание", "Выберите закрепляемую группу");
                return false;
            }
            if (IsValidGroup(group))
            {
                _dialogCoordinator.ShowMessageAsync(this, "Внимание", "Данная группа является неккоректной");
                return false;
            }
            group.EducationPlanId = _educationPlan.EducationPlanId;
            _context.SaveChanges();
            _dialogCoordinator.ShowMessageAsync(this, null, "Закреплено");
            Groups = new ObservableCollection<Group>(GetRelevantGroupsByEducationPlan(_educationPlan));
            return true;
        }

        public bool IsGroupChained(Group group)
        {
            if (group.EducationPlanId == null)
                return false;
            return true;
        }

        public bool IsValidGroup(Group group)
        {
            if (string.IsNullOrWhiteSpace(group.Name))
                return false;
            if (group.CourseNumber <= 0)
                return false;
            if (group.ReceiptYear <= 2000 || group.ReceiptYear >= DateTime.Now.AddYears(2).Year)
                return false;
            return true;
        }

        public Group[] GetRelevantGroupsByEducationPlan(EducationPlan educationPlan)
        {
            Group[] groups = _context.Groups
                .Where(x => x.ReceivedEducation.ReceivedSpecialty.SpecialtieId == educationPlan.SpecialtieId &&
                            x.EducationPlanId == null)
                .Include(x => x.ReceivedEducation)
                .ThenInclude(x => x.ReceivedSpecialty)
                .ToArray();
            return groups;
        }
    }
}