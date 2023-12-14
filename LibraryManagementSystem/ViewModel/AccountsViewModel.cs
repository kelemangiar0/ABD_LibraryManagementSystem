using LibraryManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace LibraryManagementSystem.ViewModel
{
    public class AccountsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<AccountsModel> _accounts;

        public ObservableCollection<AccountsModel> Accounts
        {
            get { return _accounts; }
            set
            {
                if (_accounts != value)
                {
                    _accounts = value;
                    OnPropertyChanged(nameof(Accounts));
                }
            }
        }

     

        public AccountsViewModel()
        {
            // Initialize and populate Accounts collection with data from the database
            // For demonstration purposes, I'll add some sample data here. In a real scenario,
            // you would fetch data from the database.
            List<AccountsModel> list;
            

            Accounts = new ObservableCollection<AccountsModel>
        {
            new AccountsModel { AccountID = 1, UserID = 101, Password = "Pass123", Username = "User1", Email = "user1@example.com" },
            new AccountsModel { AccountID = 2, UserID = 102, Password = "Pass456", Username = "User2", Email = "user2@example.com" },
            // Add more data as needed
        };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
