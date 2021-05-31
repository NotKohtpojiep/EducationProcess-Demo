using System.Collections.Generic;
using System.Linq;
using DevExpress.Mvvm;

namespace EducationProcess.Desktop.ViewModels
{
    public class ScheduleActivitiesViewModel : BindableBase, INavigationManager
    {
        private List<BindableBase> _selectedViewModels;
        public BindableBase SelectedViewModel { get; set; }

        public ScheduleActivitiesViewModel()
        {
            _selectedViewModels = new List<BindableBase>(new[] { new CourceScheduleViewModel(this) });
            SelectedViewModel = _selectedViewModels[0];
        }

        public void Next(BindableBase viewModel)
        {
            _selectedViewModels.Add(viewModel);
            SelectedViewModel = viewModel;
        }

        public void Back()
        {
            _selectedViewModels.RemoveAt(_selectedViewModels.Count - 1);
            SelectedViewModel = _selectedViewModels.Last();
        }
    }
}