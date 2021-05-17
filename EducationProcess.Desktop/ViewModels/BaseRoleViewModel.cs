using DevExpress.Mvvm;

namespace EducationProcess.Desktop.ViewModels
{
    public abstract class BaseRoleViewModel : BindableBase
    {
        public abstract string Name { get; }
    }
}
