using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LibraryManagementSystem.Model;

namespace LibraryManagementSystem.View
{
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private bool PassedLogin(string username, string password)
        {
            using (var context = new UncensoredLibraryDataContext())
            {
                try
                {
                    var user = context.Accounts.SingleOrDefault(a => a.Username == username);

                    if (user != null)
                    {
                        string storedPassword = user.Password;
                        string key = "b14ca5898a4e4133bbce2ea2315a1916";
                        string decryptedPassword = AESCrypt.DecryptString(key, storedPassword);

                        return decryptedPassword == password;
                    }

                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                    return false;
                }
            }
        }

        private void butonLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = loginUsername.Text;
            string password = loginPassword.Password;

            if (PassedLogin(username, password))
            {
                StudentWindow studentWindow = new StudentWindow(username);
                studentWindow.Show();
                var myWindow = Window.GetWindow(this);
                myWindow.Close();
            }
            else
            {
                MessageBox.Show("Eroare la autentificare!");
            }
        }
    }
}
