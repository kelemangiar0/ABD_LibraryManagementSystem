using LibraryManagementSystem.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibraryManagementSystem.ViewModel
{
    public class LibrarianViewModel : ViewModelBase
    {
        private ViewModelBase _currentChildView;

        public ICommand ShowAllStockViewCommand { get; }
        public ICommand ShowModifyStockViewCommand { get; }
        public ICommand ShowAddBooksViewCommand { get; }
        public ICommand ShowManageAccountsViewCommand { get; }
        public ICommand ShowLibraryStatisticsViewCommand { get; }
        public ICommand ShowLogoutViewCommand { get; }

        public ViewModelBase CurrentChildView
        {
            get { return _currentChildView; }
            set { _currentChildView = value; OnPropertyChanged(nameof(CurrentChildView)); }
        }

        public LibrarianViewModel()
        {
            ShowAllStockViewCommand = new ViewModelCommand(ExecuteShowAllStockViewCommand);
            ShowModifyStockViewCommand = new ViewModelCommand(ExecuteShowModifyStockViewCommand);
            ShowAddBooksViewCommand = new ViewModelCommand(ExecuteShowAddBooksViewCommand);
            ShowManageAccountsViewCommand = new ViewModelCommand(ExecuteShowManageAccountsViewCommand);
            ShowLibraryStatisticsViewCommand = new ViewModelCommand(ExecuteShowLibraryStatisticsViewCommand);
            ShowLogoutViewCommand = new ViewModelCommand(ExecuteShowLogoutViewCommand);

            ExecuteShowAllStockViewCommand(null);
        }

        private void ExecuteShowLibraryStatisticsViewCommand(object obj)
        {
            CurrentChildView = new LibraryStatisticsViewModel();
        }

        private void ExecuteShowLogoutViewCommand(object obj)
        {
            CurrentChildView = new LogoutViewModel();
        }

        private void ExecuteShowManageAccountsViewCommand(object obj)
        {
            CurrentChildView = new ManageAccountsViewModel();
        }

        private void ExecuteShowAddBooksViewCommand(object obj)
        {
            CurrentChildView = new AddBooksViewModel();
        }

        private void ExecuteShowModifyStockViewCommand(object obj)
        {
            CurrentChildView = new ModifyStockViewModel();
        }

        private void ExecuteShowAllStockViewCommand(object obj)
        {
            CurrentChildView = new AllStockViewModel();
        }
    }
}
