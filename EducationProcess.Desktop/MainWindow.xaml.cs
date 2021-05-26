using EducationProcess.Desktop.Core;
using EducationProcess.Desktop.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace EducationProcess.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, IView
    {
        private readonly MainWindowViewModel _viewModel;
        public MainWindow()
        {
            this._viewModel = new MainWindowViewModel(DialogCoordinator.Instance);
            this.DataContext = this._viewModel;
            InitializeComponent();
        }

        private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            this.HamburgerMenuControl.Content = e.InvokedItem;
        }

        #region IView Members
        public IViewModel ViewModel
        {
            get { return DataContext as IViewModel; }
            set { DataContext = value; }
        }
        #endregion
    }
}
