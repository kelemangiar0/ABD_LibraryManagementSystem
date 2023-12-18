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

            //to change
            ExecuteShowLogoutViewCommand(null);
        }

        private void ExecuteShowLibraryStatisticsViewCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteShowLogoutViewCommand(object obj)
        {
            CurrentChildView = new LogoutViewModel();
        }

        private void ExecuteShowManageAccountsViewCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteShowAddBooksViewCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteShowModifyStockViewCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteShowAllStockViewCommand(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
