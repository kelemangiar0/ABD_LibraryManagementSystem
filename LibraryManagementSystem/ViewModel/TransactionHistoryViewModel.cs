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
    public class TransactionHistoryViewModel : ViewModelBase
    {

        private ObservableCollection<TransactionsModel> _transactions;
        private ObservableCollection<TransactionsModel> allTransactions;
        public ObservableCollection<TransactionsModel> Transactions
        {
            get { return _transactions; }
            set
            {
                if (_transactions != value)
                {
                    _transactions = value;
                    OnPropertyChanged(nameof(Transactions));
                }
            }
        }

        int? findUserID(string username)
        {
            using (var context = new UncensoredLibraryDataContext())
            {
                var query = from accounts in context.Accounts
                            where accounts.Username == username
                            select accounts.UserID;
                return query.SingleOrDefault();
            }

        }

        string findUsername(int id)
        {
            using (var context = new UncensoredLibraryDataContext())
            {
                var query = from accounts in context.Accounts
                            join users in context.Users on accounts.UserID equals users.UserID
                            where users.UserID == id
                            select accounts.Username;
                return query.SingleOrDefault();
            }
        }
        public TransactionHistoryViewModel()
        {
            // Initialize and populate Transactions collection with data from the database
            // For demonstration purposes, I'll add some sample data here. In a real scenario,
            // you would fetch data from the database.
            
            List<TransactionsModel> transactionModels = new List<TransactionsModel>();

            using (var context = new UncensoredLibraryDataContext())
            {
                
                int userID = findUserID(StudentWindow.username) ?? 0;
                var query = from transaction in context.Transactions
                            join book in context.Books on transaction.BookID equals book.BookID
                            where transaction.UserID_from == userID || transaction.UserID_to == userID
                            select new
                            {
                                TransactionID = transaction.TransactionID,
                                BookID = transaction.BookID ?? 0,
                                UserID_from = transaction.UserID_from ?? 0,
                                UserID_to = transaction.UserID_to ?? 0,
                                Date_transaction = transaction.Date_transaction,
                                Date_penalty = transaction.Date_penalty,
                                BookName = book.Name
                            };

                foreach (var result in query)
                {
                    TransactionsModel transactionsModel = new TransactionsModel
                    {
                        TransactionID = result.TransactionID,
                        BookID = result.BookID,
                        UserID_from = result.UserID_from,
                        UserID_to = result.UserID_to,
                        Date_transaction = result.Date_transaction,
                        Date_penalty = result.Date_penalty,
                        Bookname = result.BookName,
                        Username_from = findUsername(result.UserID_from),
                        Username_to = findUsername(result.UserID_to)
                    };

                    transactionModels.Add(transactionsModel);
                }
            }

            Transactions = new ObservableCollection<TransactionsModel>(transactionModels);
            allTransactions = new ObservableCollection<TransactionsModel>(transactionModels);
        }

        // functie ce sterge intrarile ce nu contin filtrul pus
        public void ApplyFilter(string filter)
        {
            Transactions = allTransactions;
            if (string.IsNullOrEmpty(filter))
            {
                Transactions = _transactions;
                return;
            }
            var filteredTransactions = _transactions.Where(t =>
                t.Bookname.Contains(filter) ||
                t.Username_from.Contains(filter) ||
                t.Username_to.Contains(filter) ||
                t.Date_transaction.ToString().Contains(filter) ||
                t.Date_penalty.ToString().Contains(filter)
            ).ToList(); 

            Transactions = new ObservableCollection<TransactionsModel>(filteredTransactions);
        }
    }
}
