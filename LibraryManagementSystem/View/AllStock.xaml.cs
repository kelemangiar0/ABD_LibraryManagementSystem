﻿using LibraryManagementSystem.ViewModel;
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
    /// Interaction logic for AllStock.xaml
    /// </summary>
    public partial class AllStock : UserControl
    {
        public AllStock()
        {
            InitializeComponent();
        }

        private void filterButton_Click(object sender, RoutedEventArgs e)
        {
            //ia din textbox si filtreaza pe baza la ala
            //daca nu exista nu afiseaza nimic pur si simplu
            //filterBox.Text

            // this.Visibility = Visibility.Collapsed;
            string filterText = this.filterBox.Text;
            (DataContext as AllStockViewModel)?.ApplyFilter(filterText);


        }
    }
}
