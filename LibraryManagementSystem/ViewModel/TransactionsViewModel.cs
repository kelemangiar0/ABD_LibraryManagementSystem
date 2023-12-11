using LibraryManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.ViewModel
{
    // TransactionViewModel.cs
    public class TransactionViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TransactionsModel> _transactions;

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

        public TransactionViewModel()
        {
            // Initialize and populate Transactions collection with data from the database
            // For demonstration purposes, I'll add some sample data here. In a real scenario,
            // you would fetch data from the database.
            Transactions = new ObservableCollection<TransactionsModel>
            {
            new TransactionsModel { TransactionID = 1, BookID = 101, UserID_from = 201, UserID_to = 301, Date_transaction = DateTime.Now, Date_penalty = DateTime.Now.AddDays(7) },
            new TransactionsModel { TransactionID = 2, BookID = 102, UserID_from = 202, UserID_to = 302, Date_transaction = DateTime.Now.AddDays(-5), Date_penalty = DateTime.Now.AddDays(2) },
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
