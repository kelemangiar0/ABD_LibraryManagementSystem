using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for ModifyStockView.xaml
    /// </summary>
    public partial class ModifyStockView : UserControl
    {
        public ModifyStockView()
        {
            InitializeComponent();

            firstView();
        }

        void firstView()
        {
            cancelButton.Visibility = Visibility.Collapsed;
            confirmButton.Visibility = Visibility.Collapsed;
            writeStock.Visibility = Visibility.Collapsed;
            currentStockText.Visibility = Visibility.Collapsed;
            selectComboBox.Items.Clear();
            addItemsToComboBox();
        }

        void secondView() 
        {
            cancelButton.Visibility = Visibility.Visible;
            confirmButton.Visibility = Visibility.Visible;
            writeStock.Visibility = Visibility.Visible;
            currentStockText.Visibility = Visibility.Visible;
        }

        private void addItemsToComboBox()
        {
            using (var context = new UncensoredLibraryEntities())
            {
                try
                {
                    var booksInfo = context.Books
                        .Select(books => $"{books.Name} (ID:{books.BookID}) by {books.Author}")
                        .ToList();

                    foreach (var info in booksInfo)
                    {
                        selectComboBox.Items.Add(info);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error retrieving data from the database: {ex.Message}");
                }
            }
        }

        private void selectComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectComboBox.SelectedItem != null)
            {
                secondView();
                var selectedText = selectComboBox.SelectedItem.ToString();
                int startIndex = selectedText.IndexOf("ID:") + "ID:".Length;
                int endIndex = selectedText.IndexOf(")", startIndex);

                string bookID = selectedText.Substring(startIndex, endIndex - startIndex);
                int integerCastID = int.Parse(bookID);
                using (var context = new UncensoredLibraryEntities())
                {
                    var query = from books in context.Books
                                where books.BookID == integerCastID
                                select books.Stock;

                    var queryResult = query.SingleOrDefault();

                    string stockstring = "Current stock: ";

                    currentStockText.Content = stockstring + queryResult;
                }
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            firstView();
            writeStock.Text = string.Empty;

        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedText = selectComboBox.SelectedItem.ToString();
            int startIndex = selectedText.IndexOf("ID:") + "ID:".Length;
            int endIndex = selectedText.IndexOf(")", startIndex);

            string bookID = selectedText.Substring(startIndex, endIndex - startIndex);
            int integerCastID = int.Parse(bookID);

            using (var context = new UncensoredLibraryEntities())
            {
                var bookToUpdate = context.Books.SingleOrDefault(book => book.BookID == integerCastID);
                if (bookToUpdate != null)
                {
                    bookToUpdate.Stock = int.Parse(writeStock.Text);
                    context.SaveChanges();
                }
            }

            firstView();
            selectComboBox.SelectedItem = null;
            writeStock.Clear();
        }
    }
}
