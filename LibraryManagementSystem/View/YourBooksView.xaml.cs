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
            confirmBorrowTo.Visibility = Visibility.Visible;
        }

        private void returnBookButton_Click(object sender, RoutedEventArgs e)
        {
            //todo check if one row is selected
            //todo confirm prompt
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
            //todo DB
        }

        private void userSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //todo DB
        }
    }


}
