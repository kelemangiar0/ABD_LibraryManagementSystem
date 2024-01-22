using LibraryManagementSystem.Model;
using LibraryManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryManagementSystem.View
{
    public partial class AvailableBooksView : UserControl
    {
     

        public AvailableBooksView()
        {

            InitializeComponent();

            calendar.Visibility = Visibility.Collapsed;
            confirmBorrow.Visibility = Visibility.Collapsed;
            cancelBorrow.Visibility = Visibility.Collapsed;
            fromDate.Visibility = Visibility.Collapsed;
            toDate.Visibility = Visibility.Collapsed;
            bookTitle.Visibility = Visibility.Collapsed;
            borrowButton.Visibility = Visibility.Collapsed;
            calendar.SelectedDatesChanged += (sender, e) =>
            {
                fromDate.Content = $"From: {DateTime.Now.ToShortDateString()}";
                DateTime selectedDate = calendar.SelectedDate ?? DateTime.Now;
                toDate.Content = $"To: {selectedDate.ToShortDateString()}";
                confirmBorrow.Visibility = Visibility.Visible;
            };
            dataGrid.SelectedCellsChanged += (sender, e) =>
            {
                borrowButton.Visibility = Visibility.Visible;
            };
        }
        private int getMyAge()
        {
            int userID = findUserID(StudentWindow.username) ?? 0;

            using (var context = new UncensoredLibraryEntities())
            {
                var ageQuery = from users in context.Users
                               where users.UserID == userID
                               select users.Age;

                return ageQuery.SingleOrDefault() ?? 0;
            }
        }
        private void borrowButton_Click(object sender, RoutedEventArgs e)
        {
            cancelBorrow.Visibility = Visibility.Visible;
            borrowButton.Visibility = Visibility.Collapsed;
            dataGrid.Visibility = Visibility.Collapsed;
            calendar.Visibility = Visibility.Visible;
            fromDate.Visibility = Visibility.Visible;
            toDate.Visibility = Visibility.Visible;
            bookTitle.Visibility = Visibility.Visible;

            AvailableBooksModel selectedBook = (AvailableBooksModel)dataGrid.SelectedItem;
            string selectedBookName = selectedBook.Name;
            int selectedBookMinAge = selectedBook.MinAge ?? 0;


            if (selectedBookMinAge > getMyAge())
            {
    
                MessageBox.Show("You are not old enough to borrow this book.");
                cancelBorrow_Click(sender, e); // TO BE TESTED WITHOUT THIS SHIT
            }
            else
            {
                bookTitle.Content = $"Book: {selectedBookName}";
            }
        }

        private void cancelBorrow_Click(object sender, RoutedEventArgs e)
        {
            confirmBorrow.Visibility = Visibility.Collapsed;
            cancelBorrow.Visibility = Visibility.Collapsed;
            borrowButton.Visibility = Visibility.Collapsed;
            dataGrid.Visibility = Visibility.Visible;
            calendar.Visibility = Visibility.Collapsed;
            fromDate.Visibility = Visibility.Collapsed;
            toDate.Visibility = Visibility.Collapsed;
            bookTitle.Visibility = Visibility.Collapsed;
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

        private void confirmBorrow_Click(object sender, RoutedEventArgs e)
        {
            int selectedBookID = ((AvailableBooksModel)dataGrid.SelectedItem).BookID;
            int ID = findUserID(StudentWindow.username) ?? 0;



            DateTime selectedDate = calendar.SelectedDate ?? DateTime.MinValue;
            DateTime currentDate = DateTime.Now;

            if (selectedDate > currentDate)
            {
                using (var context = new UncensoredLibraryEntities())
                {
                    var newTransaction = new Transaction
                    {
                        BookID = selectedBookID,
                        UserID_from = 999,
                        UserID_to = ID,
                        Date_transaction = currentDate,
                        Date_penalty = selectedDate
                    };

                    context.Transactions.Add(newTransaction);
                    context.SaveChanges();

                    var bookToUpdate = context.Books.Single(b => b.BookID == selectedBookID);
                    bookToUpdate.Stock -= 1;
                    context.SaveChanges();

                    var newBooksOwned = new BooksOwned
                    {
                        UserID = ID,
                        TransactionID = newTransaction.TransactionID,
                        BookID = selectedBookID
                    };

                    context.BooksOwneds.Add(newBooksOwned);
                    context.SaveChanges();
                }

                (DataContext as AvailableBooksViewModel)?.Refresh();

                confirmBorrow.Visibility = Visibility.Collapsed;
                cancelBorrow.Visibility = Visibility.Collapsed;
                borrowButton.Visibility = Visibility.Collapsed;
                dataGrid.Visibility = Visibility.Visible;
                calendar.Visibility = Visibility.Collapsed;
                fromDate.Visibility = Visibility.Collapsed;
                toDate.Visibility = Visibility.Collapsed;
                bookTitle.Visibility = Visibility.Collapsed;

            }
            else
            {
                MessageBox.Show("Please, select a valid date in the future.");
            }
        }
    }
}
