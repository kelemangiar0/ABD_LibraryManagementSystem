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

        private bool PassedLogin(string username, string password, int userID)
        {
            using (var context = new UncensoredLibraryDataContext())
            {
                try
                {
                    var user = context.Accounts.SingleOrDefault(a => a.Username == username);

                    if (user != null)
                    {
                        string storedPassword = user.Password;
                        if(userID == StudentWindow.LIBRARY_ID) 
                        {
                            return password == storedPassword;
                        }
                      
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
        int verifyAccountType(int id) // 0 - user, 1 - librarian

        { 
            using (var context = new UncensoredLibraryDataContext())
            {
                var query = from users in context.Users
                            where users.UserID == id
                            select users.Role;

                string role = query.SingleOrDefault();
                if (role == "User")
                {
                    return 0; 
                }
                return 1; 

            }
        }
        private void butonLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = loginUsername.Text;
            string password = loginPassword.Password;
            int id = findUserID(username) ?? 0;
            string role;
            using (var context = new UncensoredLibraryDataContext())
            {
                    var query = from users in context.Users
                                where users.UserID == id
                                select users.Role;

                     role = query.SingleOrDefault();

            }
            

            
            if(PassedLogin(username, password, id) == true)
            {
                if (role == "User")
                {
                    StudentWindow studentWindow = new StudentWindow(username);
                    studentWindow.Show();
                    var myWindow = Window.GetWindow(this);
                    myWindow.Close();

                }
                if(role == "Library")
                {
                    LibrarianWindow librarianWindow = new LibrarianWindow(username);
                    librarianWindow.Show();
                    var myWindow = Window.GetWindow(this);
                    myWindow.Close();

                }

            }

            else
            {
                MessageBox.Show("Error on authentication!");
            }
        }
    }
}
