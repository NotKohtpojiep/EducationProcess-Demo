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
using MahApps.Metro.Controls.Dialogs;

namespace EducationProcess.Desktop.ViewModels
{
    public class DisciplineEditViewModel : BindableBase
    {
        private IDialogCoordinator _dialogCoordinator;
        private Discipline _discipline;

        public DisciplineEditViewModel(Discipline discipline)
        {
            _dialogCoordinator = DialogCoordinator.Instance;
            _discipline = discipline;
            EducationProcessContext context = new EducationProcessContext();

            if (discipline != null)
                HeaderText = $"Редактирование дисциплины";
            else
                HeaderText = $"Добавление дисциплины";

            CyclesAndModules = new ObservableCollection<EducationCyclesAndModule>(context.EducationCyclesAndModules.ToArray());
            Cathedras = new ObservableCollection<Cathedra>(context.Cathedras.ToArray());

            if (discipline != null)
            {
                TitleInfo = "Редактирование дисциплины";
                DisciplineName = discipline.Name;
                DisciplineIndex = discipline.DisciplineIndex;
                SelectedCathedra = Cathedras.First(x => x.CathedraId == discipline.CathedraId);
                SelectedCyclesAndModule = CyclesAndModules.First(x => x.EducationCycleId == discipline.EducationCycleId);
            }
            else
            {
                TitleInfo = "Добавление дисциплины";
            }

            CancelCommand = new RelayCommand(null,
                _ => { });
            SaveCommand = new RelayCommand(null,
                _ => Save(_discipline));
        }

        public string TitleInfo { get; set; }
        public string HeaderText { get; set; }
        public string DisciplineName { get; set; }
        public string DisciplineIndex { get; set; }
        public ObservableCollection<EducationCyclesAndModule> CyclesAndModules { get; set; }
        public ObservableCollection<Cathedra> Cathedras { get; set; }
        public EducationCyclesAndModule SelectedCyclesAndModule { get; set; }
        public Cathedra SelectedCathedra { get; set; }

        public RelayCommand CancelCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }

        private void Save(Discipline discipline)
        {
            EducationProcessContext context = new EducationProcessContext();

            if (context.Disciplines.FirstOrDefault(x => x.DisciplineIndex.ToLower() == DisciplineIndex.ToLower()) != null)
            {
                _dialogCoordinator.ShowMessageAsync(this, "Внимание", "Данный индекс уже существует");
                return;
            }

            if (discipline == null)
            {
                Discipline newDiscipline = new Discipline()
                {
                    CathedraId = SelectedCathedra.CathedraId,
                    DisciplineIndex = DisciplineIndex,
                    Name = DisciplineName,
                    EducationCycleId = SelectedCyclesAndModule.EducationCycleId
                };
                context.Disciplines.Add(newDiscipline);
                context.SaveChanges();
            }
            else
            {
                _discipline.Name = DisciplineName;
                _discipline.DisciplineIndex = DisciplineIndex;
                _discipline.CathedraId = SelectedCathedra.CathedraId;
                _discipline.EducationCycleId = SelectedCyclesAndModule.EducationCycleId;
                context.Disciplines.Update(_discipline);
                context.SaveChanges();
            }

            _dialogCoordinator.ShowMessageAsync(this, "Дисциплина сохранена", null);
        }
    }
}