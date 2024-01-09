using LibraryManagementSystem.Model;
using LibraryManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class TransactionHistoryView : UserControl
    {
        public TransactionHistoryView()
        {
            InitializeComponent();
        }

        private void filterButton_Click(object sender, RoutedEventArgs e)
        {
            string filterText = this.filterBox.Text;
            (DataContext as TransactionHistoryViewModel)?.ApplyFilter(filterText);


        }


            
    }
}
