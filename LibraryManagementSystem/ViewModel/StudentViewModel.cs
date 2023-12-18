using LibraryManagementSystem.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibraryManagementSystem.ViewModel
{
    public class StudentViewModel : ViewModelBase
    {
        private ViewModelBase _currentChildView;

        public ICommand ShowYourBooksViewCommand { get; }
        public ICommand ShowAvailableBooksViewCommand { get; }
        public ICommand ShowAccountOverviewViewCommand { get; }
        public ICommand ShowTransactionHistoryViewCommand { get; }
        public ICommand ShowLogoutViewCommand { get; }
        public ICommand ShowStatisticsViewCommand { get; }
        public ViewModelBase CurrentChildView
        {
            get { return _currentChildView; }
            set { _currentChildView = value; OnPropertyChanged(nameof(CurrentChildView)); }
        }
        public StudentViewModel()
        {
            //trebuie initializate toate paginile in memorie inainte de hide/show
            ShowYourBooksViewCommand = new ViewModelCommand(ExecuteShowYourBooksViewCommand);
            ShowAvailableBooksViewCommand = new ViewModelCommand(ExecuteShowAvailableBooksViewCommand);
            ShowTransactionHistoryViewCommand = new ViewModelCommand(ExecuteShowTransactionHistoryViewCommand);
            ShowAccountOverviewViewCommand = new ViewModelCommand(ExecuteShowAccountOverviewViewCommand);
            ShowLogoutViewCommand = new ViewModelCommand(ExecuteShowLogoutViewCommand);
            ShowStatisticsViewCommand = new ViewModelCommand(ExecuteShowStatisticsViewCommand);
            //default view       
            ExecuteShowYourBooksViewCommand(null);
        }

        private void ExecuteShowStatisticsViewCommand(object obj)
        {
            CurrentChildView = new AccountStatisticsViewModel();
        }

        private void ExecuteShowAccountOverviewViewCommand(object obj)
        {
            CurrentChildView = new AccountOverviewViewModel();
        }

        private void ExecuteShowLogoutViewCommand(object obj)
        {
            CurrentChildView = new LogoutViewModel(); 
        }

        private void ExecuteShowTransactionHistoryViewCommand(object obj)
        {
            CurrentChildView = new TransactionHistoryViewModel();
        }

        private void ExecuteShowAvailableBooksViewCommand(object obj)
        {
            CurrentChildView = new AvailableBooksViewModel();
        }

        private void ExecuteShowYourBooksViewCommand(object obj)
        {
           CurrentChildView = new YourBooksViewModel();
        }

    }
}
