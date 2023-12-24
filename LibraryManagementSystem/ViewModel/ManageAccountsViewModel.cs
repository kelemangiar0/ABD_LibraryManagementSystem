using LibraryManagementSystem.Model;
using LibraryManagementSystem.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.ViewModel
{
    public class ManageAccountsViewModel : ViewModelBase
    {
        // ... existing properties and methods
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
        public ManageAccountsViewModel()
        {
            List<AccountsModel> accountsList = new List<AccountsModel>();

            using (var context = new UncensoredLibraryDataContext())
            {
                var query = from accounts in context.Accounts
                            join users in context.Users on accounts.UserID equals users.UserID
                            select new AccountsModel
                            {
                                AccountID = accounts.AccountID,
                                UserID = accounts.UserID ?? 0,
                                Username = accounts.Username,
                                Password = accounts.Password, // Be cautious with password data
                                Email = accounts.Email,
                                Role = users.Role // Assigning the Role from the Users table
                            };

                foreach (var account in query)
                {
                    accountsList.Add(account);
                }
            }

            Accounts = new ObservableCollection<AccountsModel>(accountsList);
        }

        // ... additional functions as needed
    }
}
