using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

        private string selection;
        int? findUserID(string username)
        {
            using (var context = new UncensoredLibraryDataContext())
            {
                var query = from accounts in context.Accounts
                            where accounts.Username == username
                            select accounts.UserID;


                return query.SingleOrDefault();
            }

        }
        void setDetailsOverview()
        {
            int userID = findUserID(StudentWindow.username) ?? 0;

            using (var context = new UncensoredLibraryDataContext())
            {
                var result = (from users in context.Users
                              join accounts in context.Accounts on users.UserID equals accounts.UserID
                              select new
                              {
                                  Name = users.Name,
                                  Email = accounts.Email,
                                  Age = users.Age,
                                  Password = accounts.Password
                              }).FirstOrDefault();
               string decryptedPassword = AESCrypt.DecryptString(RegisterView.key, result.Password);
               int size = decryptedPassword.Length;
               string starPassword = "";
        

                for (int i = 0; i<size; i++)
                {
                    starPassword += "*";
                }

                nameLabel.Content = $"Name: {result.Name}";
                ageLabel.Content = $"Age: {result.Age}";
                emailLabel.Content = $"Email: {result.Email}";
                passwordLabel.Content = $"Password: {starPassword}";
            }
            edit.Text = "";



        }
        public AccountOverviewView()
        {
            InitializeComponent();

            firstView();
            setDetailsOverview();
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
            selection = "Name";
        }

        private void editAgeButton_Click(object sender, RoutedEventArgs e)
        {
            secondView();
            selection = "Age";
        }

        private void editEmailButton_Click(object sender, RoutedEventArgs e)
        {
            secondView();
            selection = "Email";
        }

        private void editPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            secondView();
            selection = "Password";
        }

        private void uploadPictureButton_Click(object sender, RoutedEventArgs e)
        {
            //sa deschida file explorer?
        }

        void editName(string newName)
        {
            int userID = findUserID(StudentWindow.username) ?? 0;

            using (var context = new UncensoredLibraryDataContext())
            {
                try
                {
                    var userToUpdate = context.Users.Single(u => u.UserID == userID);
                    userToUpdate.Name = newName;
                    context.SubmitChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Eroare la actualizarea datelor: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        void editAge(int NewAge)
        {
            int userID = findUserID(StudentWindow.username) ?? 0;

            using (var context = new UncensoredLibraryDataContext())
            {
                try
                {
                    var userToUpdate = context.Users.Single(u => u.UserID == userID);
                    userToUpdate.Age = NewAge;
                    context.SubmitChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Eroare la actualizarea datelor: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        void editEmail(string NewEmail)
        {
            int userID = findUserID(StudentWindow.username) ?? 0;

            using (var context = new UncensoredLibraryDataContext())
            {
                try
                {
                    var userToUpdate = context.Accounts.Single(a => a.UserID == userID);
                    userToUpdate.Email = NewEmail;
                    context.SubmitChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Eroare la actualizarea datelor: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        void editPassword(string password)
        {
            int userID = findUserID(StudentWindow.username) ?? 0;

            using (var context = new UncensoredLibraryDataContext())
            {
                try
                {
                    var userToUpdate = context.Accounts.Single(a => a.UserID == userID);
                    userToUpdate.Password = AESCrypt.EncryptString(RegisterView.key,password);
                    context.SubmitChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Eroare la actualizarea datelor: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void confirmEdit_Click(object sender, RoutedEventArgs e)
        {
            //vezi care dintre cele patru butoane sunt apasate? si in coloana respectiva sa intre modificarile
            firstView();

            string introducedValue = edit.Text;
            if (selection == "Name")
            {
                editName(introducedValue);
               
            }
            else if (selection == "Age")
            {
                editAge(int.Parse(introducedValue));
               
            }
            else if(selection == "Email")
            {
                editEmail(introducedValue);
                
            }
            else if(selection == "Password")
            {
                editPassword(introducedValue);
            }

            firstView();
            setDetailsOverview();

              
        }

        private void cancelEdit_Click(object sender, RoutedEventArgs e)
        {
            firstView();
        }
    }
}
