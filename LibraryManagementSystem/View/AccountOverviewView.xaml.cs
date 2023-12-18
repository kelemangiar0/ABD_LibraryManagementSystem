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
    /// Interaction logic for AccountOverviewView.xaml
    /// </summary>
    public partial class AccountOverviewView : UserControl
    {
        public AccountOverviewView()
        {
            InitializeComponent();

            firstView();
        }

        void firstView()
        {
            editAgeButton.Visibility = Visibility.Visible;
            editEmailButton.Visibility = Visibility.Visible;
            editNameButton.Visibility = Visibility.Visible;
            editPasswordButton.Visibility = Visibility.Visible;
            uploadPictureButton.Visibility = Visibility.Visible;
            nameLabel.Visibility = Visibility.Visible;
            emailLabel.Visibility = Visibility.Visible;
            passwordLabel.Visibility = Visibility.Visible;
            ageLabel.Visibility = Visibility.Visible;
            profilePicture.Visibility = Visibility.Visible;
            confirmEdit.Visibility = Visibility.Collapsed;
            cancelEdit.Visibility = Visibility.Collapsed;
            newValueLabel.Visibility = Visibility.Collapsed;
            edit.Visibility = Visibility.Collapsed;
        }
        void secondView() 
        {
            editAgeButton.Visibility = Visibility.Collapsed;
            editEmailButton.Visibility = Visibility.Collapsed;
            editNameButton.Visibility = Visibility.Collapsed;
            editPasswordButton.Visibility = Visibility.Collapsed;
            uploadPictureButton.Visibility = Visibility.Collapsed;
            nameLabel.Visibility = Visibility.Collapsed;
            emailLabel.Visibility = Visibility.Collapsed;
            passwordLabel.Visibility = Visibility.Collapsed;
            ageLabel.Visibility = Visibility.Collapsed;
            profilePicture.Visibility = Visibility.Collapsed;
            confirmEdit.Visibility = Visibility.Visible;
            cancelEdit.Visibility = Visibility.Visible;
            newValueLabel.Visibility = Visibility.Visible;
            edit.Visibility = Visibility.Visible;
        }
        private void editNameButton_Click(object sender, RoutedEventArgs e)
        {
            secondView();
        }

        private void editAgeButton_Click(object sender, RoutedEventArgs e)
        {
            secondView();
        }

        private void editEmailButton_Click(object sender, RoutedEventArgs e)
        {
            secondView();
        }

        private void editPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            secondView();
        }

        private void uploadPictureButton_Click(object sender, RoutedEventArgs e)
        {
            //sa deschida file explorer?
        }

        private void confirmEdit_Click(object sender, RoutedEventArgs e)
        {
            //vezi care dintre cele patru butoane sunt apasate? si in coloana respectiva sa intre modificarile
            firstView();
        }

        private void cancelEdit_Click(object sender, RoutedEventArgs e)
        {
            firstView();
        }
    }
}
