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
    public partial class AccountStatisticsView : UserControl
    {
        public AccountStatisticsView()
        {
            InitializeComponent();
            showCurrentBorrowed();
            showTotalBorrowed();
            showFirstAndLastDate();
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

        void showTotalBorrowed()
        {
            int userID = findUserID(StudentWindow.username) ?? 0;

            using (var context = new UncensoredLibraryDataContext())
            {
                int totalBorrowed = context.Transactions.Count(t => t.UserID_to == userID);
                totalLabel.Content += $" {totalBorrowed}";
            }
        }

        void showCurrentBorrowed()
        {
            int userID = findUserID(StudentWindow.username) ?? 0;

            using (var context = new UncensoredLibraryDataContext())
            {
                int currentBorrowed = context.BooksOwneds.Count(t => t.UserID == userID);
                currentLabel.Content += $" {currentBorrowed}";
            }
        }



        void showFirstAndLastDate()
        {
            int userID = findUserID(StudentWindow.username) ?? 0;
            using (var context = new UncensoredLibraryDataContext())
            {
                var query = from users in context.Users
                            where users.UserID == userID
                            select users.FirstBookDate;
                var firstDate = query.SingleOrDefault();

                firstLabel.Content += $" {firstDate?.ToShortDateString()}";

                query = from users in context.Users
                            where users.UserID == userID
                            select users.FirstBookDate;
                var Lastdate = query.SingleOrDefault();

                lastLabel.Content += $" {Lastdate?.ToShortDateString()}";
            }
        }
    }
}