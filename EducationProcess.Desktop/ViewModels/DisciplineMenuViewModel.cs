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
    public class DisciplineMenuViewModel : BindableBase
    {
        private readonly IDialogCoordinator _dialogCoordinator;

        public DisciplineMenuViewModel()
        {
            _dialogCoordinator = DialogCoordinator.Instance;
            Discipline[] disciplines = new EducationProcessContext()
                .Disciplines
                .Include(x => x.Cathedra)
                .Include(x => x.EducationCycle)
                .ToArray();


            Disciplines = new ObservableCollection<Discipline>(disciplines);
            ViewDisciplineInfoCommand = new RelayCommand(null, o => ViewDisciplineInfo());
            AddDisciplineCommand = new RelayCommand(null, _ => AddDiscipline());
            EditDisciplineCommand = new RelayCommand(null, _ => EditDiscipline(SelectedDiscipline));
            DeleteDisciplineCommand = new RelayCommand(null, _ => DeleteDiscipline(SelectedDiscipline));
        }

        public ObservableCollection<Discipline> Disciplines { get; set; }
        public Discipline SelectedDiscipline { get; set; }

        public RelayCommand ViewDisciplineInfoCommand { get; set; }
        public RelayCommand AddDisciplineCommand { get; set; }
        public RelayCommand EditDisciplineCommand { get; set; }
        public RelayCommand DeleteDisciplineCommand { get; set; }

        private void ViewDisciplineInfo()
        {

        }

        private void AddDiscipline()
        {
            DisciplineEditViewModel viewModel = new DisciplineEditViewModel(null);
            new DisciplineEditorWindow(viewModel).ShowDialog();
            RefreshDisciplines();
        }
        private void EditDiscipline(Discipline discipline)
        {
            if (discipline == null)
            {
                _dialogCoordinator.ShowMessageAsync(this, "Внимание", "Выберите дисциплину");
                return;
            }
            DisciplineEditViewModel viewModel = new DisciplineEditViewModel(discipline);
            new DisciplineEditorWindow(viewModel).ShowDialog();
            RefreshDisciplines();
        }

        private void DeleteDiscipline(Discipline discipline)
        {
            if (discipline == null)
            {
                _dialogCoordinator.ShowMessageAsync(this, "Внимание", "Выберите дисциплину");
                return;
            }

            EducationProcessContext context = new EducationProcessContext();
            if (context.SemesterDisciplines.FirstOrDefault(x =>
                x.DisciplineId == discipline.DisciplineId) != null)
            {
                _dialogCoordinator.ShowMessageAsync(this, "Внимание", "Открепите дисциплину от учебных планов");
                return;
            }
            context.Disciplines.Remove(discipline);
            context.SaveChanges();
            _dialogCoordinator.ShowMessageAsync(this, "Внимание", "Дисциплина успешно удалена");
            RefreshDisciplines();
        }

        private void RefreshDisciplines()
        {
            Discipline[] disciplines = new EducationProcessContext()
                .Disciplines
                .Include(x => x.Cathedra)
                .Include(x => x.EducationCycle)
                .ToArray();
            Disciplines = new ObservableCollection<Discipline>(disciplines);
        }
    }
}
