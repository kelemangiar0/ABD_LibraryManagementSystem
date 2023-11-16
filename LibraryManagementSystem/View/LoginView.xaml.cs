using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }
        private bool passedLogin(string username, string password)
        {
     

            string connectionString = "Data Source=.;Initial Catalog=UncensoredLibrary;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("The connection has just been opened.");
                    string userQuery = "SELECT Password FROM Accounts WHERE Username = @Username";

                    using (SqlCommand checkUserCommand = new SqlCommand(userQuery, connection))
                    {
                        checkUserCommand.Parameters.AddWithValue("@Username", username);

                        using (SqlDataReader reader = checkUserCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedPassword = reader["Password"].ToString();
                                string key = "b14ca5898a4e4133bbce2ea2315a1916";
                                string decryptedPassword = AESCrypt.DecryptString(key, storedPassword);

                                
                                return decryptedPassword == password;
                            }
                        }
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
            if (passedLogin(username, password))
            {
                StudentWindow studentWindow = new StudentWindow();
                studentWindow.Show();
                //sa inchida fereastra de login/register
                var myWindow = Window.GetWindow(this);
                myWindow.Close();

                
            }
            else
            {
                MessageBox.Show("Eroare logare!");
            }
        }
    }
}
