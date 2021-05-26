using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;
using DevExpress.Mvvm;
using EducationProcess.Desktop.Core;
using EducationProcess.Desktop.DataAccess;
using EducationProcess.Desktop.DataAccess.Entities;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.EntityFrameworkCore;

namespace EducationProcess.Desktop.ViewModels
{
    public class CheckDisciplineSuggestionsViewModel : BindableBase
    {
        private readonly IDialogCoordinator _dialogCoordinator;
        public ObservableCollection<SuggestingDiscipline> SuggestingDisciplines { get; set; }

        public CheckDisciplineSuggestionsViewModel()
        {
            _dialogCoordinator = DialogCoordinator.Instance;
            FixedDiscipline[] disciplines = new EducationProcessContext().FixedDisciplines
                .Include(x => x.Group)
                .Include(x => x.SemesterDiscipline)
                .ThenInclude(x => x.Discipline)
                .Include(x => x.SemesterDiscipline.Semester)
                .ToArray();

            List<SuggestingDiscipline> suggestingDisciplines = new List<SuggestingDiscipline>();
            foreach (var discipline in disciplines)
            {
                SuggestingDiscipline suggestingDiscipline =
                    new SuggestingDiscipline(discipline.FixedDisciplineId,
                        discipline.SemesterDiscipline.Discipline.Name, discipline.Group.Name,
                        discipline.SemesterDiscipline.Semester.Number,
                        new RelayCommand(null, _ => AcceptDiscipline(discipline.FixedDisciplineId)),
                         new RelayCommand(null, _ => DecineDiscipline(discipline.FixedDisciplineId)));
                suggestingDisciplines.Add(suggestingDiscipline);
            }
            SuggestingDisciplines = new ObservableCollection<SuggestingDiscipline>(suggestingDisciplines);
        }

        private async void AcceptDiscipline(int fixedDisciplineId)
        {
            await _dialogCoordinator.ShowMessageAsync(this, "Подтверждение",
                $"Вы действительно хотите фистинг {fixedDisciplineId}?", MessageDialogStyle.AffirmativeAndNegative);
        }

        private async void DecineDiscipline(int fixedDisciplineId)
        {
            MessageDialogResult mgs = await _dialogCoordinator.ShowMessageAsync(this, "Отмена",
                $"Вы действительно чувствуете {fixedDisciplineId} bucks?", MessageDialogStyle.AffirmativeAndNegative);
            if (mgs == MessageDialogResult.Affirmative)
            {
                await _dialogCoordinator.ShowMessageAsync(this, "Подтвердилось", "Ой, cum...");
            }
        }

        public class SuggestingDiscipline
        {
            private int _fixedDisciplineId;
            public SuggestingDiscipline(int fixedDisciplineId, string disciplineName, string groupName, int semestreNumber,
                RelayCommand acceptDisciplineCommand, RelayCommand cancelDisciplineCommand)
            {
                _fixedDisciplineId = fixedDisciplineId;
                Discipline = disciplineName;
                Group = groupName;
                SemestreNumber = semestreNumber;
                AcceptDisciplineCommand = acceptDisciplineCommand;
                CancelDisciplineCommand = cancelDisciplineCommand;
            }
            public string Discipline { get; set; }
            public string Group { get; set; }
            public int SemestreNumber { get; set; }
            public RelayCommand AcceptDisciplineCommand { get; set; }
            public RelayCommand CancelDisciplineCommand { get; set; }
        }
    }
}
