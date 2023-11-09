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
    /// Interaction logic for LoginRegisterView.xaml
    /// </summary>
    public partial class LoginRegisterView : UserControl
    {
        //private LoginRegisterWindowViewModel viewModel;
        public LoginRegisterView()
        {
            InitializeComponent();
            //viewModel = new LoginRegisterWindowViewModel();
            //DataContext = viewModel;
        }

        private void butonLoginMain_Click(object sender, RoutedEventArgs e)
        {
            //viewModel.ShowLoginViewCommand.Execute(null);

            //StudentWindow studentWindow = new StudentWindow();
            //studentWindow.Show();
            //this.butonLoginMain.Visibility = Visibility.Collapsed;
        }
    }
}
