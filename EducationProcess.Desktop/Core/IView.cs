using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EducationProcess.Desktop.ViewModels;

namespace EducationProcess.Desktop.Core
{
    public interface IView
    {
        IViewModel ViewModel
        {
            get;
            set;
        }

        void Show();
        void Close();
    }
    public interface IViewModel { }
}
