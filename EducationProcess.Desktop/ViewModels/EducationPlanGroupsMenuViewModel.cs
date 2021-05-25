using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.DataAccess.Entities;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;

namespace EducationProcess.Desktop.ViewModels
{
    public class EducationPlanGroupsMenuViewModel : BindableBase
    {
        
        private readonly IDialogCoordinator _dialogCoordinator;
        public ObservableCollection<Group> Groups { get; set; }

        public EducationPlanGroupsMenuViewModel()
        {
            _dialogCoordinator = DialogCoordinator.Instance;
            Group[] groups = new EducationProcessContext().Groups
                .Include(x => x.ReceivedEducation)
                .ThenInclude(x => x.ReceivedSpecialty)
                .ToArray();
            Groups = new ObservableCollection<Group>(groups);
        }
    }
}
