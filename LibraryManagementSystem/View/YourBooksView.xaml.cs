using LibraryManagementSystem.Model;
using LibraryManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Interaction logic for YourBooksView.xaml
    /// </summary>
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
                      .Where(account => account.Username != StudentWindow.username)
                      .Select(account => account.Username)
                      .ToList();

                    foreach (var username in usernames)
                    {
                        selectComboBox.Items.Add(username);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Eroare la preluarea datelor din baza de date: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
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
            //todo check if one row is selected
            dataGrid.Visibility = Visibility.Collapsed;
            borrowToButton.Visibility = Visibility.Collapsed;
            returnBookButton.Visibility = Visibility.Collapsed;
            selectComboBox.Visibility = Visibility.Visible;
            selectText.Visibility = Visibility.Visible;
            cancelBorrowTo.Visibility = Visibility.Visible;
            //    confirmBorrowTo.Visibility = Visibility.Visible; idee: sa fie afisat butonul de confirmare doar dupa selectia unui util.


        }

        private void returnBookButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedBookID = ((YourBooksModel)dataGrid.SelectedItem).BookID;
            int ID = findUserID(StudentWindow.username) ?? 0;
            int LIBRARY_ID = 999;


            using (var context = new UncensoredLibraryDataContext())
            {
                var lastTransaction = context.Transactions
                    .Where(t => t.BookID == selectedBookID && t.UserID_from == ID && t.UserID_to == ID)
                    .OrderByDescending(t => t.Date_transaction)
                    .FirstOrDefault();

                if (lastTransaction != null)
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

                    var returnedBook = context.BooksOwneds
                        .Where(b => b.UserID == ID && b.BookID == selectedBookID && b.TransactionID == lastTransaction.TransactionID)
                        .FirstOrDefault();

                    if (returnedBook != null)
                    {
                        context.BooksOwneds.DeleteOnSubmit(returnedBook);
                        context.SubmitChanges();
                    }

                    MessageBox.Show("Book returned successfully.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("No active transaction found for the selected book.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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

            // functioneaza pentru cazuri normale
            int idFrom = findUserID(StudentWindow.username) ?? 0;
            int idTo = findUserID(this.selectComboBox.Text) ?? 0;
            int bookID = ((YourBooksModel)dataGrid.SelectedItem).BookID;

            using (var context = new UncensoredLibraryDataContext())
            {
                var lastTransaction = context.Transactions
                                    .Where(t => t.BookID == bookID)
                                    .OrderByDescending(t => t.Date_transaction)
                                    .FirstOrDefault();

             
                 DateTime datePenalty = lastTransaction.Date_penalty ?? DateTime.Now ;

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
