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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryManagementSystem.View
{
    public partial class LibraryStatisticsView : UserControl
    {
        public LibraryStatisticsView()
        {
            InitializeComponent();
            showNumberBooksLibrary();
            showBooksCurrentlyBorrowed();
            showBooksAllTime();
            showUniqueAuthors();
        }

        private void showNumberBooksLibrary()
        {
            using (var context = new UncensoredLibraryEntities())
            {
                int? numberOfBooks = context.Books.Count(b => b.Stock > 0);
                this.totalLabel.Content = $"Number of Books in Library: {numberOfBooks}";
            }
        }
        private void showBooksCurrentlyBorrowed()
        {
            using (var context = new UncensoredLibraryEntities())
            {
                int? currentlyBorrowed = context.Transactions
                    .Count(t => t.Date_transaction <= DateTime.Now && t.Date_penalty >= DateTime.Now);
                this.currentLabel.Content = $"Number of Books Currently Borrowed: {currentlyBorrowed}";
            }
        }

        private void showBooksAllTime()
        {
            using (var context = new UncensoredLibraryEntities())
            {
                int? allTimeBorrowed = context.Transactions.Count();
                this.firstLabel.Content = $"Number of Books Borrowed All-Time: {allTimeBorrowed}";
            }
        }
        private void showUniqueAuthors()
        {
            using (var context = new UncensoredLibraryEntities())
            {
                int? uniqueAuthors = context.Books.Select(b => b.Author).Distinct().Count();
                this.lastLabel.Content = $"Number of Unique Authors Library: {uniqueAuthors}";
            }
        }
    }
}
