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


        int? findUserID(string username)
        {
            using (var context = new UncensoredLibraryEntities())
            {
                var query = from accounts in context.Accounts
                            where accounts.Username == username
                            select accounts.UserID;


                return query.SingleOrDefault();
            }

        }
   

        public ManageAccountsViewModel()
        {
            List<AccountsModel> accountsList = new List<AccountsModel>();

            using (var context = new UncensoredLibraryEntities())
            {
                var query = from accounts in context.Accounts
                            join users in context.Users on accounts.UserID equals users.UserID
                            join booksOwned in context.BooksOwneds on accounts.UserID equals booksOwned.UserID into booksOwnedGroup
                            select new AccountsModel
                            {
                                AccountID = accounts.AccountID,
                                UserID = accounts.UserID ?? 0,
                                Username = accounts.Username,
                                Password = accounts.Password,
                                Email = accounts.Email,
                                BooksOwned = booksOwnedGroup.Count(),
                                Role = users.Role 
                            };
                
                var UserID = findUserID(LibrarianWindow.username);
                foreach (var account in query)
                {
                    if ((account.UserID != StudentWindow.LIBRARY_ID) && (UserID != account.UserID))
                        accountsList.Add(account);
                }
            }

            Accounts = new ObservableCollection<AccountsModel>(accountsList);
        }
        public void RefreshData()
        {
            List<AccountsModel> accountsList = new List<AccountsModel>();

            using (var context = new UncensoredLibraryEntities())
            {
                var query = from accounts in context.Accounts
                            join users in context.Users on accounts.UserID equals users.UserID
                            join booksOwned in context.BooksOwneds on accounts.UserID equals booksOwned.UserID into booksOwnedGroup
                            select new AccountsModel
                            {
                                AccountID = accounts.AccountID,
                                UserID = accounts.UserID ?? 0,
                                Username = accounts.Username,
                                Password = accounts.Password, 
                                Email = accounts.Email,
                                BooksOwned = booksOwnedGroup.Count(),
                                Role = users.Role 
                            };

                var userID = findUserID(LibrarianWindow.username);
                foreach (var account in query)
                {
                    if ((account.UserID != StudentWindow.LIBRARY_ID) && (userID != account.UserID))
                        accountsList.Add(account);
                }
            }

            Accounts = new ObservableCollection<AccountsModel>(accountsList);
        }

    }
}
