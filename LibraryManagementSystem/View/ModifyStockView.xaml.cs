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
        }

        void secondView() 
        {
            cancelButton.Visibility = Visibility.Visible;
            confirmButton.Visibility = Visibility.Visible;
            writeStock.Visibility = Visibility.Visible;
            currentStockText.Visibility = Visibility.Visible;
        }
        private void selectComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            secondView();
            //pune in combobox : [<Titlu>, by <Autor>]
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            firstView();
            //si deselecteaza din combobox optiunea selectata
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            //doar modifica stock la valoarea din writeStock, nu adauga sau altceva
        }
    }
}
