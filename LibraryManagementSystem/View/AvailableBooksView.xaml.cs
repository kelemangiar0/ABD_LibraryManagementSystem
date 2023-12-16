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
    /// Interaction logic for AvailableBooksView.xaml
    /// </summary>
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
        }

        private void borrowButton_Click(object sender, RoutedEventArgs e)
        {
            //todo check if one row is selected
            //todo confirm prompt
            confirmBorrow.Visibility = Visibility.Visible;
            cancelBorrow.Visibility = Visibility.Visible;
            borrowButton.Visibility = Visibility.Collapsed;
            dataGrid.Visibility = Visibility.Collapsed;
            calendar.Visibility = Visibility.Visible;
            fromDate.Visibility = Visibility.Visible;
            toDate.Visibility = Visibility.Visible;
            bookTitle.Visibility = Visibility.Visible;
        }

        private void cancelBorrow_Click(object sender, RoutedEventArgs e)
        {
            confirmBorrow.Visibility = Visibility.Collapsed;
            cancelBorrow.Visibility = Visibility.Collapsed;
            borrowButton.Visibility = Visibility.Visible;
            dataGrid.Visibility = Visibility.Visible;
            calendar.Visibility = Visibility.Collapsed;
            fromDate.Visibility = Visibility.Collapsed;
            toDate.Visibility = Visibility.Collapsed;
            bookTitle.Visibility = Visibility.Collapsed;
        }

        private void confirmBorrow_Click(object sender, RoutedEventArgs e)
        {
            //todo
        }
    }
}
