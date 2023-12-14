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
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
namespace LibraryManagementSystem.View
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : UserControl
    {
        public RegisterView()
        {
            InitializeComponent();
        }

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


            string key = "b14ca5898a4e4133bbce2ea2315a1916";
            password = AESCrypt.EncryptString(key, password);

            var newAccount = new Account
            {
                UserID = newUser.UserID,
                Username = username,
                Password = password,
                Email = email
            };
            
            dbContext.Accounts.InsertOnSubmit(newAccount);
            dbContext.SubmitChanges();

            MessageBox.Show($"Utilizator adăugat cu succes! Username: {username}");
        }

        private void butonRegister_Click(object sender, RoutedEventArgs e)
        {
            string username = this.registerUsername.Text;
            string password = this.password.Password;
            string secondPassword = this.passwordConfirm.Password;
            string email = this.registerEmail.Text;

            if (password != secondPassword)
            {
                MessageBox.Show("Parolele nu sunt egale!");
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
                            MessageBox.Show($"A fost introdus utilizatorul {username}!");
                        }
                        else
                        {
                            MessageBox.Show($"Datele acestui utilizator se află deja în aplicație!");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Eroare înregistrare (conexiune, bază de date, etc.): {ex.Message}");
                    }
                }
            }
        }
    }
}