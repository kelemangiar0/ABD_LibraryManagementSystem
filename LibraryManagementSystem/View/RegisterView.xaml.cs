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
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : UserControl
    {
        public RegisterView()
        {
            InitializeComponent();
        }

        private bool testUserExistance(string username, string email, SqlConnection connection)
        {
            string checkUserQuery = "SELECT COUNT(*) FROM Accounts WHERE Username = @Username";

            using (SqlCommand checkUserCommand = new SqlCommand(checkUserQuery, connection))
            {
                checkUserCommand.Parameters.AddWithValue("@Username", username);
                int existingUserCount = (int)checkUserCommand.ExecuteScalar();

                if (existingUserCount > 0)
                    return true;
            }

            checkUserQuery = "SELECT COUNT(*) FROM Accounts WHERE Email = @Email";

            using (SqlCommand checkUserCommand = new SqlCommand(checkUserQuery, connection))
            {
                checkUserCommand.Parameters.AddWithValue("@Email", email);
                int existingUserCount = (int)checkUserCommand.ExecuteScalar();

                if(existingUserCount > 0)
                    return true;
            }
            return false;

        }



        private void InsertUserAndAccount(string username, string password, string email, SqlConnection connection)
        {
            
            string userRole = "User";
            string insertUserQuery = "INSERT INTO Users (Role) OUTPUT INSERTED.UserID VALUES (@Role);";
            int userID;

            using (SqlCommand command = new SqlCommand(insertUserQuery, connection))
            {
                command.Parameters.AddWithValue("@Role", userRole);
                object result = command.ExecuteScalar();
                userID = Convert.ToInt32(result);
                Console.WriteLine($"UserID asociat noii înregistrări în Users: {userID}");
            }

            string key = "b14ca5898a4e4133bbce2ea2315a1916";
            password = AESCrypt.EncryptString(key, password);

            string insertAccountsQuery = "INSERT INTO Accounts (UserID, Username, Password, Email)" +
                                        "VALUES (@UserID, @Username, @Password, @Email)";

            using (SqlCommand command = new SqlCommand(insertAccountsQuery, connection))
            {
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@Email", email);

                object result = command.ExecuteScalar();
            }

            MessageBox.Show($"User adăugat cu succes! Username: {username}");
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

            if (password == secondPassword)
            {
                string connectionString = "Data Source=.;Initial Catalog=UncensoredLibrary;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        Console.WriteLine("The connection has just been opened.");

                        if(testUserExistance(username,email,connection) == false)
                        {
                            InsertUserAndAccount(username,password,email,connection);
                            MessageBox.Show($"A fost introdus utilizatorul {username}!");
                        }
                        else
                        {
                            MessageBox.Show($"Datele acestui utilizator se afla deja in aplicatie!");
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
