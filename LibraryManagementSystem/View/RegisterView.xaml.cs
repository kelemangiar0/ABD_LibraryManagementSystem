using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
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
    public partial class RegisterView : UserControl
    {
        public RegisterView()
        {
            InitializeComponent();
        }
        public static string key = "b14ca5898a4e4133bbce2ea2315a1916";
        private bool TestUserExistence(string username, string email, UncensoredLibraryDataContext dbContext)
        {
            return dbContext.Accounts.Any(u => u.Username == username || u.Email == email);
        }

        private void InsertUserAndAccount(string username, string password, string email, UncensoredLibraryDataContext dbContext)
        {
            string userRole = "User";
            
            var newUser = new User { Role = userRole };
            
            dbContext.Users.InsertOnSubmit(newUser);
            dbContext.SubmitChanges();


            
            password = AESCrypt.EncryptString(RegisterView.key, password);

            var newAccount = new Account
            {
                UserID = newUser.UserID,
                Username = username,
                Password = password,
                Email = email
            };
            
            dbContext.Accounts.InsertOnSubmit(newAccount);
            dbContext.SubmitChanges();

            MessageBox.Show($"User added succesfully! Username: {username}");
        }

        private void butonRegister_Click(object sender, RoutedEventArgs e)
        {
            string username = this.registerUsername.Text;
            string password = this.password.Password;
            string secondPassword = this.passwordConfirm.Password;
            string email = this.registerEmail.Text;

            if (password != secondPassword)
            {
                MessageBox.Show("The passwords do not match!");
            }
            else
            {
                using (var dbContext = new UncensoredLibraryDataContext())
                {
                    try
                    {
                        if (!TestUserExistence(username, email, dbContext))
                        {
                            InsertUserAndAccount(username, password, email, dbContext);
                            MessageBox.Show($"New user introduced : {username}!");
                        }
                        else
                        {
                            MessageBox.Show($"This user's data is already in the app!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Registration error (connection, database, etc.):  {ex.Message}");
                    }
                }
            }
        }
    }
}