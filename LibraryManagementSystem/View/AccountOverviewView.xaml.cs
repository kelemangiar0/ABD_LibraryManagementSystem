using Microsoft.Win32;
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
using System.IO; 

namespace LibraryManagementSystem.View
{
    public partial class AccountOverviewView : UserControl
    {

        private string ProfilePictureSource;
        private string selection;


        int? findUserID(string username)
        {
            using (var context = new UncensoredLibraryEntities())
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

            using (var context = new UncensoredLibraryEntities())
            {
                var result = (from users in context.Users
                              join accounts in context.Accounts on users.UserID equals accounts.UserID
                              where users.UserID == userID
                              select new
                              {
                                  Name = users.Name,
                                  Email = accounts.Email,
                                  Age = users.Age,
                                  Password = accounts.Password,
                                  ProfileImagePath = users.ProfilePicture 
                              }).FirstOrDefault();

                if (result != null)
                {
                    string decryptedPassword = AESCrypt.DecryptString(RegisterView.key, result.Password);
                    int size = decryptedPassword.Length;
                    string starPassword = new string('*', size);

                    nameLabel.Content = $"Name: {result.Name}";
                    ageLabel.Content = $"Age: {result.Age}";
                    emailLabel.Content = $"Email: {result.Email}";
                    passwordLabel.Content = $"Password: {starPassword}";

                    LoadAndDisplayProfileImage(result.ProfileImagePath);
                }
                
                edit.Text = "";
            }
        }

        void LoadAndDisplayProfileImage(string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                BitmapImage profileImage = new BitmapImage();
                profileImage.BeginInit();
                profileImage.UriSource = new Uri(imagePath);
                profileImage.CacheOption = BitmapCacheOption.OnLoad;
                profileImage.EndInit();

                profilePicture.Source = profileImage;
            }
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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            openFileDialog.Title = "Select an image file";

            if (openFileDialog.ShowDialog() == true)
            {
                ProfilePictureSource = openFileDialog.FileName;
            
                int userID = findUserID(StudentWindow.username) ?? 0;

                using (var context = new UncensoredLibraryEntities())
                {
                    try
                    {
                        var userToUpdate = context.Users.Single(u => u.UserID == userID);
                        userToUpdate.ProfilePicture = ProfilePictureSource;
                        context.SaveChanges();
                        setDetailsOverview();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error on updating data: {ex.Message}");
                    }
                }
            }

        }

        void editName(string newName)
        {
            int userID = findUserID(StudentWindow.username) ?? 0;

            using (var context = new UncensoredLibraryEntities())
            {
                try
                {
                    var userToUpdate = context.Users.Single(u => u.UserID == userID);
                    userToUpdate.Name = newName;
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error on updating data: {ex.Message}");
                }
            }
        }

        void editAge(int NewAge)
        {
            int userID = findUserID(StudentWindow.username) ?? 0;

            using (var context = new UncensoredLibraryEntities())
            {
                try
                {
                    var userToUpdate = context.Users.Single(u => u.UserID == userID);
                    userToUpdate.Age = NewAge;
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error on updating data: {ex.Message}", "Eroare");
                }
            }
        }

        void editEmail(string NewEmail)
        {
            int userID = findUserID(StudentWindow.username) ?? 0;

            using (var context = new UncensoredLibraryEntities())
            {
                try
                {
                    var userToUpdate = context.Accounts.Single(a => a.UserID == userID);
                    userToUpdate.Email = NewEmail;
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error on updating data: {ex.Message}");
                }
            }
        }

        void editPassword(string password)
        {
            int userID = findUserID(StudentWindow.username) ?? 0;

            using (var context = new UncensoredLibraryEntities())
            {
                try
                {
                    var userToUpdate = context.Accounts.Single(a => a.UserID == userID);
                    userToUpdate.Password = AESCrypt.EncryptString(RegisterView.key,password);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error on updating data: {ex.Message}");
                }
            }
        }

        private void confirmEdit_Click(object sender, RoutedEventArgs e)
        {
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
