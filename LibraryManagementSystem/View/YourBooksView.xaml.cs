using LibraryManagementSystem.Model;
using LibraryManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    public partial class YourBooksView : UserControl
    {
        public YourBooksView()
        {
            InitializeComponent();

            cancelBorrowTo.Visibility = Visibility.Collapsed;
            confirmBorrowTo.Visibility = Visibility.Collapsed;
            selectComboBox.Visibility = Visibility.Collapsed;
            selectText.Visibility = Visibility.Collapsed;

            borrowToButton.Visibility = Visibility.Collapsed;
            returnBookButton.Visibility = Visibility.Collapsed;

            addItemsToComboBox();
        }



        private void addItemsToComboBox()
        {
            using (var context = new UncensoredLibraryDataContext())
            {
                try
                {
                    var usernames = context.Accounts
                              .Where(account =>
                                  account.Username != StudentWindow.username &&
                                  account.Username != "Library" &&
                                  !context.Users.Any(user => user.UserID == account.UserID && user.Role == "Librarian"))
                              .Select(account => account.Username)
                              .ToList();

                    foreach (var username in usernames)
                    {
                        selectComboBox.Items.Add(username);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error retrieving data from the database: {ex.Message}");
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

        private void borrowToButton_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.Visibility = Visibility.Collapsed;
            borrowToButton.Visibility = Visibility.Collapsed;
            returnBookButton.Visibility = Visibility.Collapsed;
            selectComboBox.Visibility = Visibility.Visible;

            selectText.Visibility = Visibility.Visible;
            cancelBorrowTo.Visibility = Visibility.Visible;
        }

        private void returnBookButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedBookID = ((YourBooksModel)dataGrid.SelectedItem).BookID;
            int ID = findUserID(StudentWindow.username) ?? 0;

            using (var context = new UncensoredLibraryDataContext())
            {
                var returnTransaction = new Transaction
                {
                    BookID = selectedBookID,
                    UserID_from = ID,
                    UserID_to = StudentWindow.LIBRARY_ID,
                    Date_transaction = DateTime.Now,
                    Date_penalty = null
                };

                context.Transactions.InsertOnSubmit(returnTransaction);
                context.SubmitChanges();

                var bookToUpdate = context.Books.Single(b => b.BookID == selectedBookID);
                bookToUpdate.Stock += 1;
                context.SubmitChanges();

                var returnedBook = context.BooksOwneds
                        .Where(b => b.UserID == ID && b.BookID == selectedBookID)
                        .FirstOrDefault();

                if (returnedBook != null)
                {
                    context.BooksOwneds.DeleteOnSubmit(returnedBook);
                    context.SubmitChanges();
                }

                MessageBox.Show("Book returned successfully.");

                (DataContext as YourBooksViewModel)?.RefreshBooks();

            }
        }

        private void cancelBorrowTo_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.Visibility = Visibility.Visible;
            borrowToButton.Visibility = Visibility.Visible;
            returnBookButton.Visibility = Visibility.Visible;
            selectComboBox.Visibility = Visibility.Collapsed;
            selectText.Visibility = Visibility.Collapsed;
            cancelBorrowTo.Visibility = Visibility.Collapsed;
            confirmBorrowTo.Visibility = Visibility.Collapsed;
        }
        private void confirmBorrowTo_Click(object sender, RoutedEventArgs e)
        {
            //procedeu imprumut:
            //preluam ID-ul userului conectat, al userului selectat si al cartii selectate
            //cautam ultima tranzactie cu cartea asta pentru a prelua data limita (m-am gandit ca ramane la fel, asa mi s ar parea logic)
            //inseram o noua intrare in tab. tranzactii cu acest schimb
            //inseram in tabela cu carti detinute intrarea pentru utilizatorul selectat
            //stergem din tabela cu carti deitnute intrarea pentru utilizatorul curent (ne dispare cartea pe care am imprumutat-o, logic)

            int idFrom = findUserID(StudentWindow.username) ?? 0;
            int idTo = findUserID(this.selectComboBox.Text) ?? 0;
            int bookID = ((YourBooksModel)dataGrid.SelectedItem).BookID;


            int minAgeRequired = getMinAge(bookID);
            int ageTo = getAge(idTo);

            if (ageTo < minAgeRequired)
            {
                MessageBox.Show("The selected user is not of the right age to borrow this book.");
                return;
            }

                using (var context = new UncensoredLibraryDataContext())
                {
                    var lastTransaction = context.Transactions
                                        .Where(t => t.BookID == bookID)
                                        .OrderByDescending(t => t.Date_transaction)
                                        .FirstOrDefault();

                    DateTime datePenalty = lastTransaction.Date_penalty ?? DateTime.Now;

                    var newTransaction = new Transaction
                    {
                        BookID = bookID,
                        UserID_from = idFrom,
                        UserID_to = idTo,
                        Date_transaction = DateTime.Now,
                        Date_penalty = datePenalty
                    };
                    context.Transactions.InsertOnSubmit(newTransaction);
                    context.SubmitChanges();

                    var newBooksOwned = new BooksOwned
                    {
                        UserID = idTo,
                        TransactionID = newTransaction.TransactionID,
                        BookID = bookID
                    };
                    context.BooksOwneds.InsertOnSubmit(newBooksOwned);
                    context.SubmitChanges();

                    var oldBooksOwned = context.BooksOwneds
                                        .Where(bo => bo.UserID == idFrom && bo.BookID == bookID)
                                        .FirstOrDefault();
                    if (oldBooksOwned != null)
                    {
                        context.BooksOwneds.DeleteOnSubmit(oldBooksOwned);
                        context.SubmitChanges();
                    }
                }

                (DataContext as YourBooksViewModel)?.RefreshBooks();
                cancelBorrowTo.Visibility = Visibility.Collapsed;

                dataGrid.Visibility = Visibility.Visible;
                confirmBorrowTo.Visibility = Visibility.Collapsed;
                selectComboBox.Visibility = Visibility.Collapsed;
                selectText.Visibility = Visibility.Collapsed;

                borrowToButton.Visibility = Visibility.Collapsed;
                returnBookButton.Visibility = Visibility.Collapsed;
            
        }


        private int getAge(int userID)
        {
            using (var context = new UncensoredLibraryDataContext())
            {
                var ageQuery = from users in context.Users
                               where users.UserID == userID
                               select users.Age;

                return ageQuery.SingleOrDefault() ?? 0;
            }
        }

        private int getMinAge(int bookID)
        {
            using (var context = new UncensoredLibraryDataContext())
            {
                var minAgeQuery = from books in context.Books
                                  where books.BookID == bookID
                                  select books.MinAge;

                return minAgeQuery.SingleOrDefault() ?? 0;
            }
        }

        private void userSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectComboBox.SelectedItem != null)
            {
                confirmBorrowTo.Visibility = Visibility.Visible;
            }
            else
            {
                confirmBorrowTo.Visibility = Visibility.Collapsed;
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                this.borrowToButton.Visibility = Visibility.Visible;
                this.returnBookButton.Visibility = Visibility.Visible;
              
            }
            else
            {
                this.borrowToButton.Visibility = Visibility.Collapsed;
                this.returnBookButton.Visibility = Visibility.Collapsed;
            }
        }
    }


}
