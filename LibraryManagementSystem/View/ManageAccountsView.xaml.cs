using LibraryManagementSystem.Model;
using LibraryManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public partial class ManageAccountsView : UserControl
    {
        public ManageAccountsView()
        {
            InitializeComponent();
            deleteAccount.Visibility = Visibility.Collapsed;
            returnAllBooks.Visibility = Visibility.Collapsed;
            toggleRole.Visibility = Visibility.Collapsed;   
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            deleteAccount.Visibility = Visibility.Visible;
            returnAllBooks.Visibility = Visibility.Visible;
            toggleRole.Visibility = Visibility.Visible;
        }



        private void deleteAccount_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null && dataGrid.SelectedItem is AccountsModel selectedAccount)
            {
                int userIDtoDelete = selectedAccount.UserID;
                string username = selectedAccount.Username;

                using (var context = new UncensoredLibraryDataContext())
                {
                        try
                        {              
                  
                        var accountToDelete = context.Accounts
                             .Where(ac => ac.UserID == userIDtoDelete)
                             .FirstOrDefault();
                        if (accountToDelete != null)
                        {
                            context.Accounts.DeleteOnSubmit(accountToDelete);
                            context.SubmitChanges();
                        }
                        context.SubmitChanges();
                            
                            MessageBox.Show($"The user {username} was deleted succesfully!");
                        }
                        catch (Exception obj)
                        {
                            MessageBox.Show($"Error in deleting {username}! Message: {obj.Message}");
                        }
                    }
                

                RefreshData();
            }
        }
        private void RefreshData()
        {

            ((ManageAccountsViewModel)DataContext).RefreshData();
            deleteAccount.Visibility = Visibility.Collapsed;
            returnAllBooks.Visibility = Visibility.Collapsed;
            toggleRole.Visibility = Visibility.Collapsed;
        }
        private void returnAllBooks_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null && dataGrid.SelectedItem is AccountsModel selectedAccount)
            {
                int userID = selectedAccount.UserID;
                using (var context = new UncensoredLibraryDataContext())
                {
                    try
                    {
                        var getBookIDs = (from ownbooks in context.BooksOwneds
                                          where ownbooks.UserID == userID
                                          select ownbooks.BookID).ToList();


                        var booksOwnedToDelete = context.BooksOwneds
                            .Where(bo => bo.UserID == userID)
                            .ToList();
                        context.BooksOwneds.DeleteAllOnSubmit(booksOwnedToDelete);

                        foreach (var bookID in getBookIDs)
                        {
                            var book = context.Books.SingleOrDefault(b => b.BookID == bookID);
                            if (book != null)
                            {
                                book.Stock += 1;
                            }
                        }

                        context.SubmitChanges();
                        MessageBox.Show("Books returned succesfully!");
                        RefreshData();
                    }
                    catch(Exception obj)
                    {
                        MessageBox.Show($"Error in returning the books: {obj.Message}");
                    }
                    }
            }
        }
        private void toggleRole_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null && dataGrid.SelectedItem is AccountsModel selectedAccount)
            {
                try
                {
                    int userID = selectedAccount.UserID;
                    using (var context = new UncensoredLibraryDataContext())
                    {
                        var query = context.Users.SingleOrDefault(b => b.UserID == userID);

                        if (query != null)
                        {
                            if (query.Role == "User")
                            {
                                query.Role = "Library";
                            }
                            else
                            {
                                query.Role = "User";
                            }
                            context.SubmitChanges();
                        }
                        RefreshData();
                    }
                }
                catch(Exception obj)
                {
                    MessageBox.Show($"Error in setting the role: {obj.Message}");

                }
           }
        }
    }
}
